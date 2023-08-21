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
//using UnrealEngine.Framework;

    namespace Frontier.Program
{
    public static class FrontierProgram
    {
        public static PublicKey ProgramAddress = new PublicKey("3FKoVbicsX7moGuqVPCY1qkZ4adA85tTpYVFEe9Vs2ei");

        public static PublicKey Season1CreatorAddress = new PublicKey("Dxp8PzakbXz4CuzXzv37wJge6Dd2rTLoJwndQ4wMV7C4");
        public static TransactionInstruction InitSeason(InitSeasonAccounts accounts, uint seasonId, PublicKey programId)
        {
            List<AccountMeta> keys = new()
                {AccountMeta.Writable(accounts.Owner, true), AccountMeta.Writable(accounts.SeasonAccount, false), AccountMeta.ReadOnly(accounts.SystemProgram, false)};
            byte[] _data = new byte[1200];
            int offset = 0;
            _data.WriteU64(7110728007230500787UL, offset);
            offset += 8;
            _data.WriteU32(seasonId, offset);
            offset += 4;
            byte[] resultData = new byte[offset];
            Array.Copy(_data, resultData, offset);
            return new TransactionInstruction { Keys = keys, ProgramId = programId.KeyBytes, Data = resultData };
        }

        public static TransactionInstruction InitPlayerAccounts(InitPlayerAccountsAccounts accounts, FactionType faction, PublicKey programId)
        {
            List<AccountMeta> keys = new()
                {AccountMeta.Writable(accounts.Owner, true), AccountMeta.Writable(accounts.PlayerAccount, false), AccountMeta.Writable(accounts.BaseAccount, false), AccountMeta.Writable(accounts.ArmyAccount, false), AccountMeta.ReadOnly(accounts.SystemProgram, false)};
            byte[] _data = new byte[1200];
            int offset = 0;
            _data.WriteU64(5350291014044636550UL, offset);
            offset += 8;
            _data.WriteU8((byte)faction, offset);
            offset += 1;
            byte[] resultData = new byte[offset];
            Array.Copy(_data, resultData, offset);
            return new TransactionInstruction { Keys = keys, ProgramId = programId.KeyBytes, Data = resultData };
        }

        public static TransactionInstruction BuildStructure(BuildStructureAccounts accounts, uint structureCount, StructureType structureType, Position position, PublicKey programId)
        {
            List<AccountMeta> keys = new()
                {AccountMeta.Writable(accounts.Owner, true), AccountMeta.Writable(accounts.PlayerAccount, false), AccountMeta.Writable(accounts.BaseAccount, false), AccountMeta.Writable(accounts.StructureAccount, false), AccountMeta.ReadOnly(accounts.SystemProgram, false)};
            byte[] _data = new byte[1200];
            int offset = 0;
            _data.WriteU64(10501095274977850560UL, offset);
            offset += 8;
            _data.WriteU32(structureCount, offset);
            offset += 4;
            _data.WriteU8((byte)structureType, offset);
            offset += 1;
            offset += position.Serialize(_data, offset);
            byte[] resultData = new byte[offset];
            Array.Copy(_data, resultData, offset);
            return new TransactionInstruction { Keys = keys, ProgramId = programId.KeyBytes, Data = resultData };
        }

        public static TransactionInstruction CollectResources(CollectResourcesAccounts accounts, uint structureCount, PublicKey programId)
        {
            List<AccountMeta> keys = new()
                {AccountMeta.Writable(accounts.Owner, true), AccountMeta.Writable(accounts.PlayerAccount, false), AccountMeta.Writable(accounts.BaseAccount, false), AccountMeta.Writable(accounts.StructureAccount, false)};
            byte[] _data = new byte[1200];
            int offset = 0;
            _data.WriteU64(18180368797663540304UL, offset);
            offset += 8;
            _data.WriteU32(structureCount, offset);
            offset += 4;
            byte[] resultData = new byte[offset];
            Array.Copy(_data, resultData, offset);
            return new TransactionInstruction { Keys = keys, ProgramId = programId.KeyBytes, Data = resultData };
        }

        public static TransactionInstruction MoveStructure(MoveStructureAccounts accounts, uint structureCount, Position newPos, PublicKey programId)
        {
            List<AccountMeta> keys = new()
                {AccountMeta.Writable(accounts.Owner, true), AccountMeta.ReadOnly(accounts.PlayerAccount, false), AccountMeta.ReadOnly(accounts.BaseAccount, false), AccountMeta.Writable(accounts.StructureAccount, false)};
            byte[] _data = new byte[1200];
            int offset = 0;
            _data.WriteU64(13965897783998561854UL, offset);
            offset += 8;
            _data.WriteU32(structureCount, offset);
            offset += 4;
            offset += newPos.Serialize(_data, offset);
            byte[] resultData = new byte[offset];
            Array.Copy(_data, resultData, offset);
            return new TransactionInstruction { Keys = keys, ProgramId = programId.KeyBytes, Data = resultData };
        }

        public static TransactionInstruction AssignWorker(AssignWorkerAccounts accounts, uint fromStructureCount, uint toStructureCount, PublicKey programId)
        {
            List<AccountMeta> keys = new()
                {AccountMeta.Writable(accounts.Owner, true), AccountMeta.Writable(accounts.PlayerAccount, false), AccountMeta.ReadOnly(accounts.BaseAccount, false), AccountMeta.Writable(accounts.FromStructureAccount, false), AccountMeta.Writable(accounts.ToStructureAccount, false)};
            byte[] _data = new byte[1200];
            int offset = 0;
            _data.WriteU64(13633494898780552279UL, offset);
            offset += 8;
            _data.WriteU32(fromStructureCount, offset);
            offset += 4;
            _data.WriteU32(toStructureCount, offset);
            offset += 4;
            byte[] resultData = new byte[offset];
            Array.Copy(_data, resultData, offset);
            return new TransactionInstruction { Keys = keys, ProgramId = programId.KeyBytes, Data = resultData };
        }

        public static TransactionInstruction TrainUnit(TrainUnitAccounts accounts, uint unitCount, UnitType unitType, PublicKey programId)
        {
            List<AccountMeta> keys = new()
                {AccountMeta.Writable(accounts.Owner, true), AccountMeta.Writable(accounts.PlayerAccount, false), AccountMeta.Writable(accounts.ArmyAccount, false), AccountMeta.Writable(accounts.UnitAccount, false), AccountMeta.ReadOnly(accounts.SystemProgram, false)};
            byte[] _data = new byte[1200];
            int offset = 0;
            _data.WriteU64(1537802545646946957UL, offset);
            offset += 8;
            _data.WriteU32(unitCount, offset);
            offset += 4;
            _data.WriteU8((byte)unitType, offset);
            offset += 1;
            byte[] resultData = new byte[offset];
            Array.Copy(_data, resultData, offset);
            return new TransactionInstruction { Keys = keys, ProgramId = programId.KeyBytes, Data = resultData };
        }

        public static TransactionInstruction StartMatch(StartMatchAccounts accounts, uint seasonId, uint matchId, uint pvpStructureId, PublicKey programId)
        {
            List<AccountMeta> keys = new()
                {AccountMeta.Writable(accounts.Attacker, true), AccountMeta.ReadOnly(accounts.AttackerAccount, false), AccountMeta.ReadOnly(accounts.AttackingArmy, false), AccountMeta.ReadOnly(accounts.Defender, false), AccountMeta.ReadOnly(accounts.DefenderAccount, false), AccountMeta.ReadOnly(accounts.DefendingBase, false), AccountMeta.ReadOnly(accounts.DefendingPvpStructure, false), AccountMeta.ReadOnly(accounts.SeasonOwner, false), AccountMeta.Writable(accounts.SeasonAccount, false), AccountMeta.Writable(accounts.GameMatch, false), AccountMeta.Writable(accounts.MatchDefendingBase, false), AccountMeta.Writable(accounts.MatchAttackingArmy, false), AccountMeta.ReadOnly(accounts.SystemProgram, false)};
            byte[] _data = new byte[1200];
            int offset = 0;
            _data.WriteU64(1440982215421851236UL, offset);
            offset += 8;
            _data.WriteU32(seasonId, offset);
            offset += 4;
            _data.WriteU32(matchId, offset);
            offset += 4;
            _data.WriteU32(pvpStructureId, offset);
            offset += 4;
            byte[] resultData = new byte[offset];
            Array.Copy(_data, resultData, offset);
            return new TransactionInstruction { Keys = keys, ProgramId = programId.KeyBytes, Data = resultData };
        }

        public static TransactionInstruction AddStructureToMatch(AddStructureToMatchAccounts accounts, uint seasonId, uint matchId, uint addedStructureId, uint matchStructureId, PublicKey programId)
        {
            List<AccountMeta> keys = new()
                {AccountMeta.Writable(accounts.Attacker, true), AccountMeta.ReadOnly(accounts.AttackerAccount, false), AccountMeta.ReadOnly(accounts.AttackingArmy, false), AccountMeta.ReadOnly(accounts.Defender, false), AccountMeta.ReadOnly(accounts.DefenderAccount, false), AccountMeta.ReadOnly(accounts.DefendingBase, false), AccountMeta.ReadOnly(accounts.StructureToAdd, false), AccountMeta.ReadOnly(accounts.SeasonOwner, false), AccountMeta.ReadOnly(accounts.SeasonAccount, false), AccountMeta.Writable(accounts.GameMatch, false), AccountMeta.Writable(accounts.MatchDefendingBase, false), AccountMeta.Writable(accounts.MatchStructureAccount, false), AccountMeta.ReadOnly(accounts.SystemProgram, false)};
            byte[] _data = new byte[1200];
            int offset = 0;
            _data.WriteU64(263069692725642216UL, offset);
            offset += 8;
            _data.WriteU32(seasonId, offset);
            offset += 4;
            _data.WriteU32(matchId, offset);
            offset += 4;
            _data.WriteU32(addedStructureId, offset);
            offset += 4;
            _data.WriteU32(matchStructureId, offset);
            offset += 4;
            byte[] resultData = new byte[offset];
            Array.Copy(_data, resultData, offset);
            return new TransactionInstruction { Keys = keys, ProgramId = programId.KeyBytes, Data = resultData };
        }

        public static TransactionInstruction AddUnitToMatch(AddUnitToMatchAccounts accounts, uint seasonId, uint matchId, uint addedUnitId, uint matchUnitId, PublicKey programId)
        {
            List<AccountMeta> keys = new()
                {AccountMeta.Writable(accounts.Attacker, true), AccountMeta.ReadOnly(accounts.AttackerAccount, false), AccountMeta.ReadOnly(accounts.AttackingArmy, false), AccountMeta.Writable(accounts.UnitToAdd, false), AccountMeta.ReadOnly(accounts.Defender, false), AccountMeta.ReadOnly(accounts.DefenderAccount, false), AccountMeta.ReadOnly(accounts.DefendingBase, false), AccountMeta.ReadOnly(accounts.SeasonOwner, false), AccountMeta.ReadOnly(accounts.SeasonAccount, false), AccountMeta.Writable(accounts.GameMatch, false), AccountMeta.Writable(accounts.MatchAttackingArmy, false), AccountMeta.Writable(accounts.MatchUnitAccount, false), AccountMeta.ReadOnly(accounts.SystemProgram, false)};
            byte[] _data = new byte[1200];
            int offset = 0;
            _data.WriteU64(16272767738533302294UL, offset);
            offset += 8;
            _data.WriteU32(seasonId, offset);
            offset += 4;
            _data.WriteU32(matchId, offset);
            offset += 4;
            _data.WriteU32(addedUnitId, offset);
            offset += 4;
            _data.WriteU32(matchUnitId, offset);
            offset += 4;
            byte[] resultData = new byte[offset];
            Array.Copy(_data, resultData, offset);
            return new TransactionInstruction { Keys = keys, ProgramId = programId.KeyBytes, Data = resultData };
        }

        public static TransactionInstruction TransitionMatchState(TransitionMatchStateAccounts accounts, uint seasonId, uint matchId, MatchState matchState, PublicKey programId)
        {
            List<AccountMeta> keys = new()
                {AccountMeta.Writable(accounts.Attacker, true), AccountMeta.ReadOnly(accounts.AttackerAccount, false), AccountMeta.ReadOnly(accounts.AttackingArmy, false), AccountMeta.ReadOnly(accounts.Defender, false), AccountMeta.ReadOnly(accounts.DefenderAccount, false), AccountMeta.ReadOnly(accounts.DefendingBase, false), AccountMeta.ReadOnly(accounts.SeasonOwner, false), AccountMeta.ReadOnly(accounts.SeasonAccount, false), AccountMeta.Writable(accounts.GameMatch, false)};
            byte[] _data = new byte[1200];
            int offset = 0;
            _data.WriteU64(16437371718255083793UL, offset);
            offset += 8;
            _data.WriteU32(seasonId, offset);
            offset += 4;
            _data.WriteU32(matchId, offset);
            offset += 4;
            _data.WriteU8((byte)matchState, offset);
            offset += 1;
            byte[] resultData = new byte[offset];
            Array.Copy(_data, resultData, offset);
            return new TransactionInstruction { Keys = keys, ProgramId = programId.KeyBytes, Data = resultData };
        }

        public static TransactionInstruction AttackStructure(AttackStructureAccounts accounts, uint seasonId, uint matchId, uint matchUnitId, uint matchStructureId, PublicKey programId)
        {
            List<AccountMeta> keys = new()
                {AccountMeta.Writable(accounts.Attacker, true), AccountMeta.ReadOnly(accounts.AttackerAccount, false), AccountMeta.ReadOnly(accounts.AttackingArmy, false), AccountMeta.ReadOnly(accounts.Defender, false), AccountMeta.ReadOnly(accounts.DefenderAccount, false), AccountMeta.ReadOnly(accounts.DefendingBase, false), AccountMeta.ReadOnly(accounts.SeasonOwner, false), AccountMeta.ReadOnly(accounts.SeasonAccount, false), AccountMeta.ReadOnly(accounts.GameMatch, false), AccountMeta.Writable(accounts.MatchAttackingArmy, false), AccountMeta.ReadOnly(accounts.AttackingMatchUnit, false), AccountMeta.ReadOnly(accounts.MatchDefendingBase, false), AccountMeta.Writable(accounts.DefendingMatchStructure, false)};
            byte[] _data = new byte[1200];
            int offset = 0;
            _data.WriteU64(8795960845634108692UL, offset);
            offset += 8;
            _data.WriteU32(seasonId, offset);
            offset += 4;
            _data.WriteU32(matchId, offset);
            offset += 4;
            _data.WriteU32(matchUnitId, offset);
            offset += 4;
            _data.WriteU32(matchStructureId, offset);
            offset += 4;
            byte[] resultData = new byte[offset];
            Array.Copy(_data, resultData, offset);
            return new TransactionInstruction { Keys = keys, ProgramId = programId.KeyBytes, Data = resultData };
        }

        public static TransactionInstruction AttackUnit(AttackUnitAccounts accounts, uint seasonId, uint matchId, uint matchUnitId, uint matchStructureId, PublicKey programId)
        {
            List<AccountMeta> keys = new()
                {AccountMeta.Writable(accounts.Attacker, true), AccountMeta.ReadOnly(accounts.AttackerAccount, false), AccountMeta.ReadOnly(accounts.AttackingArmy, false), AccountMeta.ReadOnly(accounts.Defender, false), AccountMeta.ReadOnly(accounts.DefenderAccount, false), AccountMeta.ReadOnly(accounts.DefendingBase, false), AccountMeta.ReadOnly(accounts.SeasonOwner, false), AccountMeta.ReadOnly(accounts.SeasonAccount, false), AccountMeta.ReadOnly(accounts.GameMatch, false), AccountMeta.Writable(accounts.MatchAttackingArmy, false), AccountMeta.Writable(accounts.AttackingMatchUnit, false), AccountMeta.ReadOnly(accounts.MatchDefendingBase, false), AccountMeta.ReadOnly(accounts.DefendingMatchStructure, false)};
            byte[] _data = new byte[1200];
            int offset = 0;
            _data.WriteU64(4409208135940919702UL, offset);
            offset += 8;
            _data.WriteU32(seasonId, offset);
            offset += 4;
            _data.WriteU32(matchId, offset);
            offset += 4;
            _data.WriteU32(matchUnitId, offset);
            offset += 4;
            _data.WriteU32(matchStructureId, offset);
            offset += 4;
            byte[] resultData = new byte[offset];
            Array.Copy(_data, resultData, offset);
            return new TransactionInstruction { Keys = keys, ProgramId = programId.KeyBytes, Data = resultData };
        }

        public static TransactionInstruction DistributeMatchRewards(DistributeMatchRewardsAccounts accounts, uint seasonId, uint matchId, PublicKey programId)
        {
            List<AccountMeta> keys = new()
                {AccountMeta.Writable(accounts.Attacker, true), AccountMeta.ReadOnly(accounts.AttackerAccount, false), AccountMeta.Writable(accounts.AttackingArmy, false), AccountMeta.ReadOnly(accounts.Defender, false), AccountMeta.ReadOnly(accounts.DefenderAccount, false), AccountMeta.ReadOnly(accounts.DefendingBase, false), AccountMeta.ReadOnly(accounts.SeasonOwner, false), AccountMeta.ReadOnly(accounts.SeasonAccount, false), AccountMeta.Writable(accounts.GameMatch, false)};
            byte[] _data = new byte[1200];
            int offset = 0;
            _data.WriteU64(7628045518403667986UL, offset);
            offset += 8;
            _data.WriteU32(seasonId, offset);
            offset += 4;
            _data.WriteU32(matchId, offset);
            offset += 4;
            byte[] resultData = new byte[offset];
            Array.Copy(_data, resultData, offset);
            return new TransactionInstruction { Keys = keys, ProgramId = programId.KeyBytes, Data = resultData };
        }
    }
}