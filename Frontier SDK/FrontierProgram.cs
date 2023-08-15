using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;
using Solnet;
using Solnet.Programs.Abstract;
using Solnet.Programs.Utilities;
using Solnet.Rpc.Models;
using Solnet.Rpc;
using Solnet.Rpc.Builders;
using Solnet.Rpc.Core.Http;
using Solnet.Rpc.Core.Sockets;
using Solnet.Rpc.Types;
using Solnet.Wallet;
using Frontier;
using Frontier.Program;
using Frontier.Errors;
using Frontier.Accounts;
using Frontier.Types;

    namespace Frontier.Program
    {
        public class InitPlayerAccountsAccounts
        {
            public PublicKey Owner { get; set; }

            public PublicKey PlayerAccount { get; set; }

            public PublicKey BaseAccount { get; set; }

            public PublicKey ArmyAccount { get; set; }

            public PublicKey SystemProgram { get; set; }
        }

        public class BuildStructureAccounts
        {
            public PublicKey Owner { get; set; }

            public PublicKey PlayerAccount { get; set; }

            public PublicKey BaseAccount { get; set; }

            public PublicKey StructureAccount { get; set; }

            public PublicKey SystemProgram { get; set; }
        }

        public static class FrontierProgram
        {
            public static TransactionInstruction InitPlayerAccounts(InitPlayerAccountsAccounts accounts, PublicKey programId)
            {
                List<AccountMeta> keys = new()
                {AccountMeta.Writable(accounts.Owner, true), AccountMeta.Writable(accounts.PlayerAccount, false), AccountMeta.Writable(accounts.BaseAccount, false), AccountMeta.Writable(accounts.ArmyAccount, false), AccountMeta.ReadOnly(accounts.SystemProgram, false)};
                byte[] _data = new byte[1200];
                int offset = 0;
                _data.WriteU64(5350291014044636550UL, offset);
                offset += 8;
                byte[] resultData = new byte[offset];
                Array.Copy(_data, resultData, offset);
                return new TransactionInstruction { Keys = keys, ProgramId = programId.KeyBytes, Data = resultData};
            }

            public static TransactionInstruction BuildStructure(BuildStructureAccounts accounts, uint structureCount, uint structureType, PublicKey programId)
            {
                List<AccountMeta> keys = new()
                {AccountMeta.Writable(accounts.Owner, true), AccountMeta.Writable(accounts.PlayerAccount, false), AccountMeta.Writable(accounts.BaseAccount, false), AccountMeta.Writable(accounts.StructureAccount, false), AccountMeta.ReadOnly(accounts.SystemProgram, false)};
                byte[] _data = new byte[1200];
                int offset = 0;
                _data.WriteU64(10501095274977850560UL, offset);
                offset += 8;
                _data.WriteU32(structureCount, offset);
                offset += 4;
                _data.WriteU32(structureType, offset);
                offset += 4;
                byte[] resultData = new byte[offset];
                Array.Copy(_data, resultData, offset);
                return new TransactionInstruction { Keys = keys, ProgramId = programId.KeyBytes, Data = resultData};
            }
        }
    }