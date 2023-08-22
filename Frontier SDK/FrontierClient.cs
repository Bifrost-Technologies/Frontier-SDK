using Frontiers.Accounts;
using Frontiers.Errors;
using Frontiers.Program;
using Frontiers.Types;
using Frontiers.Utilities;
using Frontiers.Wallet;
using Microsoft.AspNetCore.DataProtection;
using Solnet.Extensions;
using Solnet.Programs;
using Solnet.Programs.Abstract;
using Solnet.Programs.Models;
using Solnet.Rpc;
using Solnet.Rpc.Builders;
using Solnet.Rpc.Core.Http;
using Solnet.Rpc.Core.Sockets;
using Solnet.Rpc.Messages;
using Solnet.Rpc.Models;
using Solnet.Rpc.Types;
using Solnet.Wallet;
using System;
using UnrealEngine.Framework;
using Player = Frontiers.Accounts.Player;

namespace Frontiers
{
    public partial class FrontierChainClient : BaseClient
    {
        public FrontierGameWallet AirlockWallet { get; set; }

        public Dictionary<string, PublicKey> PlayerPDAs { get; set; }
        public Sequencer PacketChamber { get; set; }

        private LevelScript levelScript { get; set; }

        public string programAddress { get; set; }
        public FrontierChainClient(IRpcClient rpcClient, IStreamingRpcClient streamingRpcClient, PublicKey programId) : base(rpcClient, streamingRpcClient, programId)
        {
            programAddress = programId.Key.ToString();
        }
        public void InitializeChainClient(string password)
        {

            PacketChamber = new Sequencer();
            try
            {
                AirlockWallet = new FrontierGameWallet(password);
                string eventMessage = "Successful!";
                int eventID = (int)EventID.Initialization;
                if (AirlockWallet.isLoaded())
                {

                    bool successfulLogin = AirlockWallet.validCredentials();
                    if (successfulLogin)
                    {
                        
                        levelScript = World.GetActor<LevelScript>();
                        PacketChamber = new Sequencer();
                        AirlockWallet.tokenWallet = TokenWallet.Load(this.RpcClient, AirlockWallet.tokenMintDatabase, AirlockWallet.playerAddress);
                        PlayerPDAs = new Dictionary<string, PublicKey>
                        {
                            { "player", PDALookup.FindPlayerPDA(AirlockWallet.playerAddress) },
                            { "base", PDALookup.FindPlayerPDA(AirlockWallet.playerAddress) },
                            { "army", PDALookup.FindPlayerPDA(AirlockWallet.playerAddress) }
                        };
                        AirlockWallet.Balance = AirlockWallet.tokenWallet.Sol;
                        Debug.Log(LogLevel.Warning, AirlockWallet.playerAddress.Key);
                        Debug.Log(LogLevel.Warning, AirlockWallet.Balance.ToString());
                        SendInitPlayerAccounts(FactionType.Orc, ProgramIdKey);
                        levelScript.Invoke($"Initresponse\"{eventMessage}: \" {eventID}");
                        Debug.Log(LogLevel.Warning, "Game Chain Client is initialized!");
                    }
                    else
                    {
                        eventMessage = "Failed login attempt!";
                        levelScript.Invoke($"Initresponse\"{eventMessage}: \" {eventID}");
                        Debug.Log(LogLevel.Warning, "Game Chain Client is initialization failed! Bad Login!");
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.Log(LogLevel.Warning, ex.Message);

            }

        }
        public async Task<ProgramAccountsResultWrapper<List<Army>>> GetArmysAsync(string programAddress, Commitment commitment = Commitment.Finalized)
        {
            var list = new List<MemCmp> { new MemCmp { Bytes = Army.EncodedAccID, Offset = 0 } };
            var res = await RpcClient.GetProgramAccountsAsync(programAddress, commitment, memCmpList: list);
            if (!res.WasSuccessful || !(res.Result?.Count > 0))
                return new ProgramAccountsResultWrapper<List<Army>>(res);
            List<Army> resultingAccounts = new List<Army>(res.Result.Count);
            resultingAccounts.AddRange(res.Result.Select(result => Army.Deserialize(Convert.FromBase64String(result.Account.Data[0]))));
            return new ProgramAccountsResultWrapper<List<Army>>(res, resultingAccounts);
        }

        public async Task<ProgramAccountsResultWrapper<List<GameMatch>>> GetGameMatchsAsync(string programAddress, Commitment commitment = Commitment.Finalized)
        {
            var list = new List<MemCmp> { new MemCmp { Bytes = GameMatch.EncodedAccID, Offset = 0 } };
            var res = await RpcClient.GetProgramAccountsAsync(programAddress, commitment, memCmpList: list);
            if (!res.WasSuccessful || !(res.Result?.Count > 0))
                return new ProgramAccountsResultWrapper<List<GameMatch>>(res);
            List<GameMatch> resultingAccounts = new List<GameMatch>(res.Result.Count);
            resultingAccounts.AddRange(res.Result.Select(result => GameMatch.Deserialize(Convert.FromBase64String(result.Account.Data[0]))));
            return new ProgramAccountsResultWrapper<List<GameMatch>>(res, resultingAccounts);
        }

        public async Task<ProgramAccountsResultWrapper<List<PlayerBase>>> GetPlayerBasesAsync(string programAddress, Commitment commitment = Commitment.Finalized)
        {
            var list = new List<MemCmp> { new MemCmp { Bytes = PlayerBase.EncodedAccID, Offset = 0 } };
            var res = await RpcClient.GetProgramAccountsAsync(programAddress, commitment, memCmpList: list);
            if (!res.WasSuccessful || !(res.Result?.Count > 0))
                return new ProgramAccountsResultWrapper<List<PlayerBase>>(res);
            List<PlayerBase> resultingAccounts = new List<PlayerBase>(res.Result.Count);
            resultingAccounts.AddRange(res.Result.Select(result => PlayerBase.Deserialize(Convert.FromBase64String(result.Account.Data[0]))));
            return new ProgramAccountsResultWrapper<List<PlayerBase>>(res, resultingAccounts);
        }

        public async Task<ProgramAccountsResultWrapper<List<Player>>> GetPlayersAsync(string programAddress, Commitment commitment = Commitment.Finalized)
        {
            var list = new List<MemCmp> { new MemCmp { Bytes = Player.EncodedAccID, Offset = 0 } };
            var res = await RpcClient.GetProgramAccountsAsync(programAddress, commitment, memCmpList: list);
            if (!res.WasSuccessful || !(res.Result?.Count > 0))
                return new ProgramAccountsResultWrapper<List<Player>>(res);
            List<Player> resultingAccounts = new List<Player>(res.Result.Count);
            resultingAccounts.AddRange(res.Result.Select(result => Player.Deserialize(Convert.FromBase64String(result.Account.Data[0]))));
            return new ProgramAccountsResultWrapper<List<Player>>(res, resultingAccounts);
        }

        public async Task<ProgramAccountsResultWrapper<List<Season>>> GetSeasonsAsync(string programAddress, Commitment commitment = Commitment.Finalized)
        {
            var list = new List<MemCmp> { new MemCmp { Bytes = Season.EncodedAccID, Offset = 0 } };
            var res = await RpcClient.GetProgramAccountsAsync(programAddress, commitment, memCmpList: list);
            if (!res.WasSuccessful || !(res.Result?.Count > 0))
                return new ProgramAccountsResultWrapper<List<Season>>(res);
            List<Season> resultingAccounts = new List<Season>(res.Result.Count);
            resultingAccounts.AddRange(res.Result.Select(result => Season.Deserialize(Convert.FromBase64String(result.Account.Data[0]))));
            return new ProgramAccountsResultWrapper<List<Season>>(res, resultingAccounts);
        }

        public async Task<ProgramAccountsResultWrapper<List<Structure>>> GetStructuresAsync(string programAddress, Commitment commitment = Commitment.Finalized)
        {
            var list = new List<MemCmp> { new MemCmp { Bytes = Structure.EncodedAccID, Offset = 0 } };
            var res = await RpcClient.GetProgramAccountsAsync(programAddress, commitment, memCmpList: list);
            if (!res.WasSuccessful || !(res.Result?.Count > 0))
                return new ProgramAccountsResultWrapper<List<Structure>>(res);
            List<Structure> resultingAccounts = new List<Structure>(res.Result.Count);
            resultingAccounts.AddRange(res.Result.Select(result => Structure.Deserialize(Convert.FromBase64String(result.Account.Data[0]))));
            return new ProgramAccountsResultWrapper<List<Structure>>(res, resultingAccounts);
        }

        public async Task<ProgramAccountsResultWrapper<List<Unit>>> GetUnitsAsync(string programAddress, Commitment commitment = Commitment.Finalized)
        {
            var list = new List<MemCmp> { new MemCmp { Bytes = Unit.EncodedAccID, Offset = 0 } };
            var res = await RpcClient.GetProgramAccountsAsync(programAddress, commitment, memCmpList: list);
            if (!res.WasSuccessful || !(res.Result?.Count > 0))
                return new ProgramAccountsResultWrapper<List<Unit>>(res);
            List<Unit> resultingAccounts = new List<Unit>(res.Result.Count);
            resultingAccounts.AddRange(res.Result.Select(result => Unit.Deserialize(Convert.FromBase64String(result.Account.Data[0]))));
            return new ProgramAccountsResultWrapper<List<Unit>>(res, resultingAccounts);
        }

        public async Task<AccountResultWrapper<Army>> GetArmyAsync(string accountAddress, Commitment commitment = Commitment.Finalized)
        {
            var res = await RpcClient.GetAccountInfoAsync(accountAddress, commitment);
            if (!res.WasSuccessful)
                return new AccountResultWrapper<Army>(res);
            var resultingAccount = Army.Deserialize(Convert.FromBase64String(res.Result.Value.Data[0]));
            return new AccountResultWrapper<Army>(res, resultingAccount);
        }

        public async Task<AccountResultWrapper<GameMatch>> GetGameMatchAsync(string accountAddress, Commitment commitment = Commitment.Finalized)
        {
            var res = await RpcClient.GetAccountInfoAsync(accountAddress, commitment);
            if (!res.WasSuccessful)
                return new AccountResultWrapper<GameMatch>(res);
            var resultingAccount = GameMatch.Deserialize(Convert.FromBase64String(res.Result.Value.Data[0]));
            return new AccountResultWrapper<GameMatch>(res, resultingAccount);
        }

        public async Task<AccountResultWrapper<PlayerBase>> GetPlayerBaseAsync(string accountAddress, Commitment commitment = Commitment.Finalized)
        {
            var res = await RpcClient.GetAccountInfoAsync(accountAddress, commitment);
            if (!res.WasSuccessful)
                return new AccountResultWrapper<PlayerBase>(res);
            var resultingAccount = PlayerBase.Deserialize(Convert.FromBase64String(res.Result.Value.Data[0]));
            return new AccountResultWrapper<PlayerBase>(res, resultingAccount);
        }

        public async Task<AccountResultWrapper<Player>> GetPlayerAsync(string accountAddress, Commitment commitment = Commitment.Finalized)
        {
            var res = await RpcClient.GetAccountInfoAsync(accountAddress, commitment);
            if (!res.WasSuccessful)
                return new AccountResultWrapper<Player>(res);
            var resultingAccount = Player.Deserialize(Convert.FromBase64String(res.Result.Value.Data[0]));
            return new AccountResultWrapper<Player>(res, resultingAccount);
        }

        public async Task<AccountResultWrapper<Season>> GetSeasonAsync(string accountAddress, Commitment commitment = Commitment.Finalized)
        {
            var res = await RpcClient.GetAccountInfoAsync(accountAddress, commitment);
            if (!res.WasSuccessful)
                return new AccountResultWrapper<Season>(res);
            var resultingAccount = Season.Deserialize(Convert.FromBase64String(res.Result.Value.Data[0]));
            return new AccountResultWrapper<Season>(res, resultingAccount);
        }

        public async Task<AccountResultWrapper<Structure>> GetStructureAsync(string accountAddress, Commitment commitment = Commitment.Finalized)
        {
            var res = await RpcClient.GetAccountInfoAsync(accountAddress, commitment);
            if (!res.WasSuccessful)
                return new AccountResultWrapper<Structure>(res);
            var resultingAccount = Structure.Deserialize(Convert.FromBase64String(res.Result.Value.Data[0]));
            return new AccountResultWrapper<Structure>(res, resultingAccount);
        }

        public async Task<AccountResultWrapper<Unit>> GetUnitAsync(string accountAddress, Commitment commitment = Commitment.Finalized)
        {
            var res = await RpcClient.GetAccountInfoAsync(accountAddress, commitment);
            if (!res.WasSuccessful)
                return new AccountResultWrapper<Unit>(res);
            var resultingAccount = Unit.Deserialize(Convert.FromBase64String(res.Result.Value.Data[0]));
            return new AccountResultWrapper<Unit>(res, resultingAccount);
        }

        public async Task<SubscriptionState> SubscribeArmyAsync(string accountAddress, Action<SubscriptionState, ResponseValue<AccountInfo>, Army> callback, Commitment commitment = Commitment.Finalized)
        {
            SubscriptionState res = await StreamingRpcClient.SubscribeAccountInfoAsync(accountAddress, (s, e) =>
            {
                Army parsingResult = null;
                if (e.Value?.Data?.Count > 0)
                    parsingResult = Army.Deserialize(Convert.FromBase64String(e.Value.Data[0]));
                callback(s, e, parsingResult);
            }, commitment);
            return res;
        }

        public async Task<SubscriptionState> SubscribeGameMatchAsync(string accountAddress, Action<SubscriptionState, ResponseValue<AccountInfo>, GameMatch> callback, Commitment commitment = Commitment.Finalized)
        {
            SubscriptionState res = await StreamingRpcClient.SubscribeAccountInfoAsync(accountAddress, (s, e) =>
            {
                GameMatch parsingResult = null;
                if (e.Value?.Data?.Count > 0)
                    parsingResult = GameMatch.Deserialize(Convert.FromBase64String(e.Value.Data[0]));
                callback(s, e, parsingResult);
            }, commitment);
            return res;
        }

        public async Task<SubscriptionState> SubscribePlayerBaseAsync(string accountAddress, Action<SubscriptionState, ResponseValue<AccountInfo>, PlayerBase> callback, Commitment commitment = Commitment.Finalized)
        {
            SubscriptionState res = await StreamingRpcClient.SubscribeAccountInfoAsync(accountAddress, (s, e) =>
            {
                PlayerBase parsingResult = null;
                if (e.Value?.Data?.Count > 0)
                    parsingResult = PlayerBase.Deserialize(Convert.FromBase64String(e.Value.Data[0]));
                callback(s, e, parsingResult);
            }, commitment);
            return res;
        }

        public async Task<SubscriptionState> SubscribePlayerAsync(string accountAddress, Action<SubscriptionState, ResponseValue<AccountInfo>, Player> callback, Commitment commitment = Commitment.Finalized)
        {
            SubscriptionState res = await StreamingRpcClient.SubscribeAccountInfoAsync(accountAddress, (s, e) =>
            {
                Player parsingResult = null;
                if (e.Value?.Data?.Count > 0)
                    parsingResult = Player.Deserialize(Convert.FromBase64String(e.Value.Data[0]));
                callback(s, e, parsingResult);
            }, commitment);
            return res;
        }

        public async Task<SubscriptionState> SubscribeSeasonAsync(string accountAddress, Action<SubscriptionState, ResponseValue<AccountInfo>, Season> callback, Commitment commitment = Commitment.Finalized)
        {
            SubscriptionState res = await StreamingRpcClient.SubscribeAccountInfoAsync(accountAddress, (s, e) =>
            {
                Season parsingResult = null;
                if (e.Value?.Data?.Count > 0)
                    parsingResult = Season.Deserialize(Convert.FromBase64String(e.Value.Data[0]));
                callback(s, e, parsingResult);
            }, commitment);
            return res;
        }

        public async Task<SubscriptionState> SubscribeStructureAsync(string accountAddress, Action<SubscriptionState, ResponseValue<AccountInfo>, Structure> callback, Commitment commitment = Commitment.Finalized)
        {
            SubscriptionState res = await StreamingRpcClient.SubscribeAccountInfoAsync(accountAddress, (s, e) =>
            {
                Structure parsingResult = null;
                if (e.Value?.Data?.Count > 0)
                    parsingResult = Structure.Deserialize(Convert.FromBase64String(e.Value.Data[0]));
                callback(s, e, parsingResult);
            }, commitment);
            return res;
        }

        public async Task<SubscriptionState> SubscribeUnitAsync(string accountAddress, Action<SubscriptionState, ResponseValue<AccountInfo>, Unit> callback, Commitment commitment = Commitment.Finalized)
        {
            SubscriptionState res = await StreamingRpcClient.SubscribeAccountInfoAsync(accountAddress, (s, e) =>
            {
                Unit parsingResult = null;
                if (e.Value?.Data?.Count > 0)
                    parsingResult = Unit.Deserialize(Convert.FromBase64String(e.Value.Data[0]));
                callback(s, e, parsingResult);
            }, commitment);
            return res;
        }

        public string SendInitSeason(InitSeasonAccounts accounts, uint seasonId, PublicKey programId)
        {
            RequestResult<ResponseValue<LatestBlockHash>> blockHash = RpcClient.GetLatestBlockHash();
            TransactionInstruction instr = FrontierProgram.InitSeason(accounts, seasonId, programId);
            TransactionBuilder Instruction = new TransactionBuilder().
          SetRecentBlockHash(blockHash.Result.Value.Blockhash).
          SetFeePayer(AirlockWallet.playerAddress).
          AddInstruction(instr);
            byte[] transaction = AirlockWallet.SignMessage(Instruction);
            PacketChamber.Add(new TransactionPacket(true, transaction));
            return  "Transaction Signed & Sent to chamber!";
        }

        public string SendInitPlayerAccounts( FactionType faction, PublicKey programId)
        {
            var accounts = new InitPlayerAccountsAccounts { Owner = AirlockWallet.playerAddress, PlayerAccount = PlayerPDAs["player"], BaseAccount = PlayerPDAs["base"], ArmyAccount = PlayerPDAs["army"], SystemProgram = SystemProgram.ProgramIdKey };
            RequestResult<ResponseValue<LatestBlockHash>> blockHash = RpcClient.GetLatestBlockHash();
            TransactionInstruction instr = FrontierProgram.InitPlayerAccounts(accounts, faction, programId);
            TransactionBuilder Instruction = new TransactionBuilder().
           SetRecentBlockHash(blockHash.Result.Value.Blockhash).
           SetFeePayer(AirlockWallet.playerAddress).
           AddInstruction(instr);
            byte[] transaction = AirlockWallet.SignMessage(Instruction);
            PacketChamber.Add(new TransactionPacket(true, transaction));
            return  "Transaction Signed & Sent to chamber!";
        }

        public string SendBuildStructureAsync(BuildStructureAccounts accounts, uint structureCount, StructureType structureType, Position position, PublicKey programId)
        {
            RequestResult<ResponseValue<LatestBlockHash>> blockHash = RpcClient.GetLatestBlockHash();
            TransactionInstruction instr = FrontierProgram.BuildStructure(accounts, structureCount, structureType, position, programId);
            TransactionBuilder Instruction = new TransactionBuilder().
           SetRecentBlockHash(blockHash.Result.Value.Blockhash).
           SetFeePayer(AirlockWallet.playerAddress).
           AddInstruction(instr);
            byte[] transaction = AirlockWallet.SignMessage(Instruction);
            PacketChamber.Add(new TransactionPacket(true, transaction));
            return  "Transaction Signed & Sent to chamber!";
        }

        public TransactionInstruction CollectResourcesInstruction(CollectResourcesAccounts accounts, uint structureCount, PublicKey programId)
        {
            TransactionInstruction instr = FrontierProgram.CollectResources(accounts, structureCount, programId);
            return instr;
        }
        public string SendCollectResourcesAsync(List<TransactionInstruction> _resourceInstruBatch)
        {
            RequestResult<ResponseValue<LatestBlockHash>> blockHash = RpcClient.GetLatestBlockHash();
            TransactionBuilder builder = new TransactionBuilder().
                SetRecentBlockHash(blockHash.Result.Value.Blockhash).
                SetFeePayer(AirlockWallet.playerAddress);
            
            foreach(var resource in _resourceInstruBatch)
            {
                builder.AddInstruction(resource);
            }

         
            byte[] transaction = AirlockWallet.SignMessage(builder);
            PacketChamber.Add(new TransactionPacket(true, transaction));
            return  "Transaction Signed & Sent to chamber!";
        }

        public string SendMoveStructureAsync(MoveStructureAccounts accounts, uint structureCount, Position newPos, PublicKey feePayer, Func<byte[], PublicKey, byte[]> signingCallback, PublicKey programId)
        {
            RequestResult<ResponseValue<LatestBlockHash>> blockHash = RpcClient.GetLatestBlockHash();
            TransactionInstruction instr = FrontierProgram.MoveStructure(accounts, structureCount, newPos, programId);
            TransactionBuilder Instruction = new TransactionBuilder().
                SetRecentBlockHash(blockHash.Result.Value.Blockhash).
                SetFeePayer(AirlockWallet.playerAddress).
                AddInstruction(instr);
            byte[] transaction = AirlockWallet.SignMessage(Instruction);
            PacketChamber.Add(new TransactionPacket(true, transaction));
            return  "Transaction Signed & Sent to chamber!";
        }

        public string SendAssignWorkerAsync(AssignWorkerAccounts accounts, uint fromStructureCount, uint toStructureCount, PublicKey feePayer, Func<byte[], PublicKey, byte[]> signingCallback, PublicKey programId)
        {
            RequestResult<ResponseValue<LatestBlockHash>> blockHash = RpcClient.GetLatestBlockHash();
            TransactionInstruction instr = FrontierProgram.AssignWorker(accounts, fromStructureCount, toStructureCount, programId);
             TransactionBuilder Instruction = new TransactionBuilder().
                SetRecentBlockHash(blockHash.Result.Value.Blockhash).
                SetFeePayer(AirlockWallet.playerAddress).
                AddInstruction(instr);
            byte[] transaction = AirlockWallet.SignMessage(Instruction);
            PacketChamber.Add(new TransactionPacket(true, transaction));
            return  "Transaction Signed & Sent to chamber!";
        }

        public string SendTrainUnitAsync(TrainUnitAccounts accounts, uint unitCount, UnitType unitType, PublicKey feePayer, Func<byte[], PublicKey, byte[]> signingCallback, PublicKey programId)
        {
            RequestResult<ResponseValue<LatestBlockHash>> blockHash = RpcClient.GetLatestBlockHash();
            TransactionInstruction instr = FrontierProgram.TrainUnit(accounts, unitCount, unitType, programId);
            TransactionBuilder Instruction = new TransactionBuilder().
                SetRecentBlockHash(blockHash.Result.Value.Blockhash).
                SetFeePayer(AirlockWallet.playerAddress).
                AddInstruction(instr);
            byte[] transaction = AirlockWallet.SignMessage(Instruction);
            PacketChamber.Add(new TransactionPacket(true, transaction));
            return  "Transaction Signed & Sent to chamber!";
        }

        public string SendStartMatchAsync(StartMatchAccounts accounts, uint seasonId, uint matchId, uint pvpStructureId, PublicKey feePayer, Func<byte[], PublicKey, byte[]> signingCallback, PublicKey programId)
        {
            RequestResult<ResponseValue<LatestBlockHash>> blockHash = RpcClient.GetLatestBlockHash();
            TransactionInstruction instr = FrontierProgram.StartMatch(accounts, seasonId, matchId, pvpStructureId, programId);
            TransactionBuilder Instruction = new TransactionBuilder().
                SetRecentBlockHash(blockHash.Result.Value.Blockhash).
                SetFeePayer(AirlockWallet.playerAddress).
                AddInstruction(instr);
            byte[] transaction = AirlockWallet.SignMessage(Instruction);
            PacketChamber.Add(new TransactionPacket(true, transaction));
            return  "Transaction Signed & Sent to chamber!";
        }

        public string SendAddStructureToMatchAsync(AddStructureToMatchAccounts accounts, uint seasonId, uint matchId, uint addedStructureId, uint matchStructureId, PublicKey feePayer, Func<byte[], PublicKey, byte[]> signingCallback, PublicKey programId)
        {
            RequestResult<ResponseValue<LatestBlockHash>> blockHash = RpcClient.GetLatestBlockHash();
            TransactionInstruction instr = FrontierProgram.AddStructureToMatch(accounts, seasonId, matchId, addedStructureId, matchStructureId, programId);
            TransactionBuilder Instruction = new TransactionBuilder().
                SetRecentBlockHash(blockHash.Result.Value.Blockhash).
                SetFeePayer(AirlockWallet.playerAddress).
                AddInstruction(instr);
            byte[] transaction = AirlockWallet.SignMessage(Instruction);
            PacketChamber.Add(new TransactionPacket(true, transaction));
            return  "Transaction Signed & Sent to chamber!";
        }

        public string SendAddUnitToMatchAsync(AddUnitToMatchAccounts accounts, uint seasonId, uint matchId, uint addedUnitId, uint matchUnitId, PublicKey feePayer, Func<byte[], PublicKey, byte[]> signingCallback, PublicKey programId)
        {
            RequestResult<ResponseValue<LatestBlockHash>> blockHash = RpcClient.GetLatestBlockHash();
            TransactionInstruction instr = FrontierProgram.AddUnitToMatch(accounts, seasonId, matchId, addedUnitId, matchUnitId, programId);
            TransactionBuilder Instruction = new TransactionBuilder().
                SetRecentBlockHash(blockHash.Result.Value.Blockhash).
                SetFeePayer(AirlockWallet.playerAddress).
                AddInstruction(instr);
            byte[] transaction = AirlockWallet.SignMessage(Instruction);
            PacketChamber.Add(new TransactionPacket(true, transaction));
            return  "Transaction Signed & Sent to chamber!";
        }

        public string SendTransitionMatchStateAsync(TransitionMatchStateAccounts accounts, uint seasonId, uint matchId, MatchState matchState, PublicKey feePayer, Func<byte[], PublicKey, byte[]> signingCallback, PublicKey programId)
        {
            RequestResult<ResponseValue<LatestBlockHash>> blockHash = RpcClient.GetLatestBlockHash();
            TransactionInstruction instr = FrontierProgram.TransitionMatchState(accounts, seasonId, matchId, matchState, programId);
            TransactionBuilder Instruction = new TransactionBuilder().
                SetRecentBlockHash(blockHash.Result.Value.Blockhash).
                SetFeePayer(AirlockWallet.playerAddress).
                AddInstruction(instr);
            byte[] transaction = AirlockWallet.SignMessage(Instruction);
            PacketChamber.Add(new TransactionPacket(true, transaction));
            return  "Transaction Signed & Sent to chamber!";
        }

        public string SendAttackStructureAsync(AttackStructureAccounts accounts, uint seasonId, uint matchId, uint matchUnitId, uint matchStructureId, PublicKey feePayer, Func<byte[], PublicKey, byte[]> signingCallback, PublicKey programId)
        {
            RequestResult<ResponseValue<LatestBlockHash>> blockHash = RpcClient.GetLatestBlockHash();
            TransactionInstruction instr = FrontierProgram.AttackStructure(accounts, seasonId, matchId, matchUnitId, matchStructureId, programId);
            TransactionBuilder Instruction = new TransactionBuilder().
                SetRecentBlockHash(blockHash.Result.Value.Blockhash).
                SetFeePayer(AirlockWallet.playerAddress).
                AddInstruction(instr);
            byte[] transaction = AirlockWallet.SignMessage(Instruction);
            PacketChamber.Add(new TransactionPacket(true, transaction));
            return  "Transaction Signed & Sent to chamber!";
        }

        public string SendAttackUnitAsync(AttackUnitAccounts accounts, uint seasonId, uint matchId, uint matchUnitId, uint matchStructureId, PublicKey feePayer, Func<byte[], PublicKey, byte[]> signingCallback, PublicKey programId)
        {
            RequestResult<ResponseValue<LatestBlockHash>> blockHash = RpcClient.GetLatestBlockHash();
            TransactionInstruction instr = FrontierProgram.AttackUnit(accounts, seasonId, matchId, matchUnitId, matchStructureId, programId);
            TransactionBuilder Instruction = new TransactionBuilder().
                SetRecentBlockHash(blockHash.Result.Value.Blockhash).
                SetFeePayer(AirlockWallet.playerAddress).
                AddInstruction(instr);
            byte[] transaction = AirlockWallet.SignMessage(Instruction);
            PacketChamber.Add(new TransactionPacket(true, transaction));
            return "Transaction Signed & Sent to chamber!";
        }

        public string SendDistributeMatchRewardsAsync(DistributeMatchRewardsAccounts accounts, uint seasonId, uint matchId, PublicKey feePayer, Func<byte[], PublicKey, byte[]> signingCallback, PublicKey programId)
        {
            RequestResult<ResponseValue<LatestBlockHash>> blockHash = RpcClient.GetLatestBlockHash();
            TransactionInstruction instr = FrontierProgram.DistributeMatchRewards(accounts, seasonId, matchId, programId);
            TransactionBuilder Instruction = new TransactionBuilder().
                SetRecentBlockHash(blockHash.Result.Value.Blockhash).
                SetFeePayer(AirlockWallet.playerAddress).
                AddInstruction(instr);
            byte[] transaction = AirlockWallet.SignMessage(Instruction);
            PacketChamber.Add(new TransactionPacket(true, transaction));
            return "Distributed Match Rewards!";
        }
    }
}
