using Frontier.Accounts;
using Frontier.Errors;
using Frontier.Program;
using Frontier.Types;
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
using UnrealEngine.Framework;

namespace Frontier
{
    public partial class FrontierChainClient : BaseClient
    {
        public string programAddress { get; set; }
        public FrontierChainClient(IRpcClient rpcClient, IStreamingRpcClient streamingRpcClient, PublicKey programId) : base(rpcClient, streamingRpcClient, programId)
        {
            programAddress = programId.Key.ToString();
        }


        public async Task<ProgramAccountsResultWrapper<List<Army>>> GetArmysAsync( Commitment commitment = Commitment.Finalized)
        {
            var list = new List<MemCmp> { new MemCmp { Bytes = Army.EncodedAccID, Offset = 0 } };
            var res = await RpcClient.GetProgramAccountsAsync(programAddress, commitment, memCmpList: list);
            if (!res.WasSuccessful || !(res.Result?.Count > 0))
                return new ProgramAccountsResultWrapper<List<Army>>(res);

            List<Army> resultingAccounts = new List<Army>(res.Result.Count);
            resultingAccounts.AddRange(res.Result.Select(result => Army.Deserialize(Convert.FromBase64String(result.Account.Data[0]))));
            return new ProgramAccountsResultWrapper<List<Army>>(res, resultingAccounts);
        }

        public async Task<ProgramAccountsResultWrapper<List<PlayerBase>>> GetPlayerBasesAsync( Commitment commitment = Commitment.Finalized)
        {
            var list = new List<MemCmp> { new MemCmp { Bytes = PlayerBase.EncodedAccID, Offset = 0 } };
            var res = await RpcClient.GetProgramAccountsAsync(programAddress, commitment, memCmpList: list);
            if (!res.WasSuccessful || !(res.Result?.Count > 0))
                return new ProgramAccountsResultWrapper<List<PlayerBase>>(res);

            List<PlayerBase> resultingAccounts = new List<PlayerBase>(res.Result.Count);
            resultingAccounts.AddRange(res.Result.Select(result => PlayerBase.Deserialize(Convert.FromBase64String(result.Account.Data[0]))));
            return new ProgramAccountsResultWrapper<List<PlayerBase>>(res, resultingAccounts);
        }

        public async Task<ProgramAccountsResultWrapper<List<Accounts.Player>>> GetPlayersAsync( Commitment commitment = Commitment.Finalized)
        {
            var list = new List<MemCmp> { new MemCmp { Bytes = Accounts.Player.EncodedAccID, Offset = 0 } };
            var res = await RpcClient.GetProgramAccountsAsync(programAddress, commitment, memCmpList: list);
            Debug.Log(LogLevel.Warning, "Did getPlayers work? " + res.WasSuccessful + " | " + res.Result.Count());
            if (!res.WasSuccessful || !(res.Result?.Count > 0))
                return new ProgramAccountsResultWrapper<List<Accounts.Player>>(res);

            List<Accounts.Player> resultingAccounts = new List<Accounts.Player>(res.Result.Count);
            resultingAccounts.AddRange(res.Result.Select(result => Accounts.Player.Deserialize(Convert.FromBase64String(result.Account.Data[0]))));
            return new ProgramAccountsResultWrapper<List<Accounts.Player>>(res, resultingAccounts);
        }

        public async Task<ProgramAccountsResultWrapper<List<Structure>>> GetStructuresAsync( Commitment commitment = Commitment.Finalized)
        {
            var list = new List<MemCmp> { new MemCmp { Bytes = Structure.EncodedAccID, Offset = 0 } };
            var res = await RpcClient.GetProgramAccountsAsync(programAddress, commitment, memCmpList: list);
            if (!res.WasSuccessful || !(res.Result?.Count > 0))
                return new ProgramAccountsResultWrapper<List<Structure>>(res);

            List<Structure> resultingAccounts = new List<Structure>(res.Result.Count);
            resultingAccounts.AddRange(res.Result.Select(result => Structure.Deserialize(Convert.FromBase64String(result.Account.Data[0]))));
            return new ProgramAccountsResultWrapper<List<Structure>>(res, resultingAccounts);
        }

        public async Task<AccountResultWrapper<Army>> GetArmyAsync(string accountAddress, Commitment commitment = Commitment.Finalized)
        {
            var res = await RpcClient.GetAccountInfoAsync(accountAddress, commitment);
            if (!res.WasSuccessful)
                return new AccountResultWrapper<Army>(res);

            var resultingAccount = Army.Deserialize(Convert.FromBase64String(res.Result.Value.Data[0]));
            return new AccountResultWrapper<Army>(res, resultingAccount);
        }

        public async Task<AccountResultWrapper<PlayerBase>> GetPlayerBaseAsync(string accountAddress, Commitment commitment = Commitment.Finalized)
        {
            var res = await RpcClient.GetAccountInfoAsync(accountAddress, commitment);
            if (!res.WasSuccessful)
                return new AccountResultWrapper<PlayerBase>(res);

            var resultingAccount = PlayerBase.Deserialize(Convert.FromBase64String(res.Result.Value.Data[0]));
            return new AccountResultWrapper<PlayerBase>(res, resultingAccount);
        }

        public async Task<AccountResultWrapper<Accounts.Player>> GetPlayerAsync(string accountAddress, Commitment commitment = Commitment.Finalized)
        {
            var res = await RpcClient.GetAccountInfoAsync(accountAddress, commitment);
            if (!res.WasSuccessful)
                return new AccountResultWrapper<Accounts.Player>(res);

            var resultingAccount = Accounts.Player.Deserialize(Convert.FromBase64String(res.Result.Value.Data[0]));
            return new AccountResultWrapper<Accounts.Player>(res, resultingAccount);
        }

        public async Task<AccountResultWrapper<Structure>> GetStructureAsync(string accountAddress, Commitment commitment = Commitment.Finalized)
        {
            var res = await RpcClient.GetAccountInfoAsync(accountAddress, commitment);
            if (!res.WasSuccessful)
                return new AccountResultWrapper<Structure>(res);

            var resultingAccount = Structure.Deserialize(Convert.FromBase64String(res.Result.Value.Data[0]));
            return new AccountResultWrapper<Structure>(res, resultingAccount);
        }

        public async Task<SubscriptionState> SubscribeArmyAsync(string accountAddress, Action<SubscriptionState, Solnet.Rpc.Messages.ResponseValue<AccountInfo>, Army> callback, Commitment commitment = Commitment.Finalized)
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

        public async Task<SubscriptionState> SubscribePlayerBaseAsync(string accountAddress, Action<SubscriptionState, Solnet.Rpc.Messages.ResponseValue<AccountInfo>, PlayerBase> callback, Commitment commitment = Commitment.Finalized)
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

        public async Task<SubscriptionState> SubscribePlayerAsync(string accountAddress, Action<SubscriptionState, Solnet.Rpc.Messages.ResponseValue<AccountInfo>, Accounts.Player> callback, Commitment commitment = Commitment.Finalized)
        {
            SubscriptionState res = await StreamingRpcClient.SubscribeAccountInfoAsync(accountAddress, (s, e) =>
            {
                Accounts.Player parsingResult = null;
                if (e.Value?.Data?.Count > 0)
                    parsingResult = Accounts.Player.Deserialize(Convert.FromBase64String(e.Value.Data[0]));
                callback(s, e, parsingResult);
            }, commitment);
            return res;
        }

        public async Task<SubscriptionState> SubscribeStructureAsync(string accountAddress, Action<SubscriptionState, Solnet.Rpc.Messages.ResponseValue<AccountInfo>, Structure> callback, Commitment commitment = Commitment.Finalized)
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

        public async Task<byte[]> InitPlayerAccountsInstruction(InitPlayerAccountsAccounts accounts)
        {
            RequestResult<ResponseValue<LatestBlockHash>> blockHash = await RpcClient.GetLatestBlockHashAsync();
            TransactionInstruction instr = FrontierProgram.InitPlayerAccounts(accounts);
            byte[] InitPlayerInstruction = new TransactionBuilder().
           SetRecentBlockHash(blockHash.Result.Value.Blockhash).
           SetFeePayer(accounts.Owner).
           AddInstruction(instr).
           CompileMessage();

            return InitPlayerInstruction;
        }

        public async Task<byte[]> BuildStructureInstruction(BuildStructureAccounts accounts, uint structureCount, StructureType structureType, Position position, PublicKey ownerAddress, Func<byte[], PublicKey, byte[]> signingCallback, PublicKey programId)
        {
            RequestResult<ResponseValue<LatestBlockHash>> blockHash = await RpcClient.GetLatestBlockHashAsync();
            TransactionInstruction instr = FrontierProgram.BuildStructure(accounts, structureCount, structureType, position);
            byte[] BuildStructureInstruction = new TransactionBuilder().
      SetRecentBlockHash(blockHash.Result.Value.Blockhash).
      SetFeePayer(ownerAddress).
      AddInstruction(instr).
      CompileMessage();

            return BuildStructureInstruction;
        }

        public async Task<byte[]> SendCollectResourcesAsync(CollectResourcesAccounts accounts, uint structureCount, PublicKey ownerAddress)
        {
            RequestResult<ResponseValue<LatestBlockHash>> blockHash = await RpcClient.GetLatestBlockHashAsync();
            TransactionInstruction instr = FrontierProgram.CollectResources(accounts, structureCount);
            byte[] CollectResourcesInstruction = new TransactionBuilder().
      SetRecentBlockHash(blockHash.Result.Value.Blockhash).
      SetFeePayer(ownerAddress).
      AddInstruction(instr).
      CompileMessage();

            return CollectResourcesInstruction;
        }

    }
}
