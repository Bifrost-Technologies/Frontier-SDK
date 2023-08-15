using Frontier.Accounts;
using Frontier.Errors;
using Frontier.Program;
using Solnet.Programs.Abstract;
using Solnet.Rpc.Core.Http;
using Solnet.Rpc.Core.Sockets;
using Solnet.Rpc.Types;
using Solnet.Rpc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Solnet.Wallet;

namespace Frontier
{
    public partial class FrontierClient : TransactionalBaseClient<FrontierErrorKind>
    {
        public FrontierClient(IRpcClient rpcClient, IStreamingRpcClient streamingRpcClient, PublicKey programId) : base(rpcClient, streamingRpcClient, programId)
        {
        }

        public async Task<Solnet.Programs.Models.ProgramAccountsResultWrapper<List<Army>>> GetArmysAsync(string programAddress, Commitment commitment = Commitment.Finalized)
        {
            var list = new List<Solnet.Rpc.Models.MemCmp> { new Solnet.Rpc.Models.MemCmp { Bytes = Army.ACCOUNT_DISCRIMINATOR_B58, Offset = 0 } };
            var res = await RpcClient.GetProgramAccountsAsync(programAddress, commitment, memCmpList: list);
            if (!res.WasSuccessful || !(res.Result?.Count > 0))
                return new Solnet.Programs.Models.ProgramAccountsResultWrapper<List<Army>>(res);
            List<Army> resultingAccounts = new List<Army>(res.Result.Count);
            resultingAccounts.AddRange(res.Result.Select(result => Army.Deserialize(Convert.FromBase64String(result.Account.Data[0]))));
            return new Solnet.Programs.Models.ProgramAccountsResultWrapper<List<Army>>(res, resultingAccounts);
        }

        public async Task<Solnet.Programs.Models.ProgramAccountsResultWrapper<List<PlayerBase>>> GetPlayerBasesAsync(string programAddress, Commitment commitment = Commitment.Finalized)
        {
            var list = new List<Solnet.Rpc.Models.MemCmp> { new Solnet.Rpc.Models.MemCmp { Bytes = PlayerBase.ACCOUNT_DISCRIMINATOR_B58, Offset = 0 } };
            var res = await RpcClient.GetProgramAccountsAsync(programAddress, commitment, memCmpList: list);
            if (!res.WasSuccessful || !(res.Result?.Count > 0))
                return new Solnet.Programs.Models.ProgramAccountsResultWrapper<List<PlayerBase>>(res);
            List<PlayerBase> resultingAccounts = new List<PlayerBase>(res.Result.Count);
            resultingAccounts.AddRange(res.Result.Select(result => PlayerBase.Deserialize(Convert.FromBase64String(result.Account.Data[0]))));
            return new Solnet.Programs.Models.ProgramAccountsResultWrapper<List<PlayerBase>>(res, resultingAccounts);
        }

        public async Task<Solnet.Programs.Models.ProgramAccountsResultWrapper<List<Player>>> GetPlayersAsync(string programAddress, Commitment commitment = Commitment.Finalized)
        {
            var list = new List<Solnet.Rpc.Models.MemCmp> { new Solnet.Rpc.Models.MemCmp { Bytes = Player.ACCOUNT_DISCRIMINATOR_B58, Offset = 0 } };
            var res = await RpcClient.GetProgramAccountsAsync(programAddress, commitment, memCmpList: list);
            if (!res.WasSuccessful || !(res.Result?.Count > 0))
                return new Solnet.Programs.Models.ProgramAccountsResultWrapper<List<Player>>(res);
            List<Player> resultingAccounts = new List<Player>(res.Result.Count);
            resultingAccounts.AddRange(res.Result.Select(result => Player.Deserialize(Convert.FromBase64String(result.Account.Data[0]))));
            return new Solnet.Programs.Models.ProgramAccountsResultWrapper<List<Player>>(res, resultingAccounts);
        }

        public async Task<Solnet.Programs.Models.ProgramAccountsResultWrapper<List<Structure>>> GetStructuresAsync(string programAddress, Commitment commitment = Commitment.Finalized)
        {
            var list = new List<Solnet.Rpc.Models.MemCmp> { new Solnet.Rpc.Models.MemCmp { Bytes = Structure.ACCOUNT_DISCRIMINATOR_B58, Offset = 0 } };
            var res = await RpcClient.GetProgramAccountsAsync(programAddress, commitment, memCmpList: list);
            if (!res.WasSuccessful || !(res.Result?.Count > 0))
                return new Solnet.Programs.Models.ProgramAccountsResultWrapper<List<Structure>>(res);
            List<Structure> resultingAccounts = new List<Structure>(res.Result.Count);
            resultingAccounts.AddRange(res.Result.Select(result => Structure.Deserialize(Convert.FromBase64String(result.Account.Data[0]))));
            return new Solnet.Programs.Models.ProgramAccountsResultWrapper<List<Structure>>(res, resultingAccounts);
        }

        public async Task<Solnet.Programs.Models.AccountResultWrapper<Army>> GetArmyAsync(string accountAddress, Commitment commitment = Commitment.Finalized)
        {
            var res = await RpcClient.GetAccountInfoAsync(accountAddress, commitment);
            if (!res.WasSuccessful)
                return new Solnet.Programs.Models.AccountResultWrapper<Army>(res);
            var resultingAccount = Army.Deserialize(Convert.FromBase64String(res.Result.Value.Data[0]));
            return new Solnet.Programs.Models.AccountResultWrapper<Army>(res, resultingAccount);
        }

        public async Task<Solnet.Programs.Models.AccountResultWrapper<PlayerBase>> GetPlayerBaseAsync(string accountAddress, Commitment commitment = Commitment.Finalized)
        {
            var res = await RpcClient.GetAccountInfoAsync(accountAddress, commitment);
            if (!res.WasSuccessful)
                return new Solnet.Programs.Models.AccountResultWrapper<PlayerBase>(res);
            var resultingAccount = PlayerBase.Deserialize(Convert.FromBase64String(res.Result.Value.Data[0]));
            return new Solnet.Programs.Models.AccountResultWrapper<PlayerBase>(res, resultingAccount);
        }

        public async Task<Solnet.Programs.Models.AccountResultWrapper<Player>> GetPlayerAsync(string accountAddress, Commitment commitment = Commitment.Finalized)
        {
            var res = await RpcClient.GetAccountInfoAsync(accountAddress, commitment);
            if (!res.WasSuccessful)
                return new Solnet.Programs.Models.AccountResultWrapper<Player>(res);
            var resultingAccount = Player.Deserialize(Convert.FromBase64String(res.Result.Value.Data[0]));
            return new Solnet.Programs.Models.AccountResultWrapper<Player>(res, resultingAccount);
        }

        public async Task<Solnet.Programs.Models.AccountResultWrapper<Structure>> GetStructureAsync(string accountAddress, Commitment commitment = Commitment.Finalized)
        {
            var res = await RpcClient.GetAccountInfoAsync(accountAddress, commitment);
            if (!res.WasSuccessful)
                return new Solnet.Programs.Models.AccountResultWrapper<Structure>(res);
            var resultingAccount = Structure.Deserialize(Convert.FromBase64String(res.Result.Value.Data[0]));
            return new Solnet.Programs.Models.AccountResultWrapper<Structure>(res, resultingAccount);
        }

        public async Task<SubscriptionState> SubscribeArmyAsync(string accountAddress, Action<SubscriptionState, Solnet.Rpc.Messages.ResponseValue<Solnet.Rpc.Models.AccountInfo>, Army> callback, Commitment commitment = Commitment.Finalized)
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

        public async Task<SubscriptionState> SubscribePlayerBaseAsync(string accountAddress, Action<SubscriptionState, Solnet.Rpc.Messages.ResponseValue<Solnet.Rpc.Models.AccountInfo>, PlayerBase> callback, Commitment commitment = Commitment.Finalized)
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

        public async Task<SubscriptionState> SubscribePlayerAsync(string accountAddress, Action<SubscriptionState, Solnet.Rpc.Messages.ResponseValue<Solnet.Rpc.Models.AccountInfo>, Player> callback, Commitment commitment = Commitment.Finalized)
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

        public async Task<SubscriptionState> SubscribeStructureAsync(string accountAddress, Action<SubscriptionState, Solnet.Rpc.Messages.ResponseValue<Solnet.Rpc.Models.AccountInfo>, Structure> callback, Commitment commitment = Commitment.Finalized)
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

        public async Task<RequestResult<string>> SendInitPlayerAccountsAsync(InitPlayerAccountsAccounts accounts, PublicKey feePayer, Func<byte[], PublicKey, byte[]> signingCallback, PublicKey programId)
        {
            Solnet.Rpc.Models.TransactionInstruction instr = Frontier.Program.FrontierProgram.InitPlayerAccounts(accounts, programId);
            return await SignAndSendTransaction(instr, feePayer, signingCallback);
        }

        public async Task<RequestResult<string>> SendBuildStructureAsync(BuildStructureAccounts accounts, uint structureCount, uint structureType, PublicKey feePayer, Func<byte[], PublicKey, byte[]> signingCallback, PublicKey programId)
        {
            Solnet.Rpc.Models.TransactionInstruction instr = Frontier.Program.FrontierProgram.BuildStructure(accounts, structureCount, structureType, programId);
            return await SignAndSendTransaction(instr, feePayer, signingCallback);
        }

        protected override Dictionary<uint, ProgramError<FrontierErrorKind>> BuildErrorsDictionary()
        {
            throw new NotImplementedException();
        }
    }
}
