using Solnet.Programs.Utilities;
using Solnet.Wallet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frontiers.Types
{
    public class InitSeasonAccounts
    {
        public PublicKey Owner { get; set; }

        public PublicKey SeasonAccount { get; set; }

        public PublicKey SystemProgram { get; set; }
    }

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

    public class CollectResourcesAccounts
    {
        public PublicKey Owner { get; set; }

        public PublicKey PlayerAccount { get; set; }

        public PublicKey BaseAccount { get; set; }

        public PublicKey StructureAccount { get; set; }
    }

    public class MoveStructureAccounts
    {
        public PublicKey Owner { get; set; }

        public PublicKey PlayerAccount { get; set; }

        public PublicKey BaseAccount { get; set; }

        public PublicKey StructureAccount { get; set; }
    }

    public class AssignWorkerAccounts
    {
        public PublicKey Owner { get; set; }

        public PublicKey PlayerAccount { get; set; }

        public PublicKey BaseAccount { get; set; }

        public PublicKey FromStructureAccount { get; set; }

        public PublicKey ToStructureAccount { get; set; }
    }

    public class TrainUnitAccounts
    {
        public PublicKey Owner { get; set; }

        public PublicKey PlayerAccount { get; set; }

        public PublicKey ArmyAccount { get; set; }

        public PublicKey UnitAccount { get; set; }

        public PublicKey SystemProgram { get; set; }
    }

    public class StartMatchAccounts
    {
        public PublicKey Attacker { get; set; }

        public PublicKey AttackerAccount { get; set; }

        public PublicKey AttackingArmy { get; set; }

        public PublicKey Defender { get; set; }

        public PublicKey DefenderAccount { get; set; }

        public PublicKey DefendingBase { get; set; }

        public PublicKey DefendingPvpStructure { get; set; }

        public PublicKey SeasonOwner { get; set; }

        public PublicKey SeasonAccount { get; set; }

        public PublicKey GameMatch { get; set; }

        public PublicKey MatchDefendingBase { get; set; }

        public PublicKey MatchAttackingArmy { get; set; }

        public PublicKey SystemProgram { get; set; }
    }

    public class AddStructureToMatchAccounts
    {
        public PublicKey Attacker { get; set; }

        public PublicKey AttackerAccount { get; set; }

        public PublicKey AttackingArmy { get; set; }

        public PublicKey Defender { get; set; }

        public PublicKey DefenderAccount { get; set; }

        public PublicKey DefendingBase { get; set; }

        public PublicKey StructureToAdd { get; set; }

        public PublicKey SeasonOwner { get; set; }

        public PublicKey SeasonAccount { get; set; }

        public PublicKey GameMatch { get; set; }

        public PublicKey MatchDefendingBase { get; set; }

        public PublicKey MatchStructureAccount { get; set; }

        public PublicKey SystemProgram { get; set; }
    }

    public class AddUnitToMatchAccounts
    {
        public PublicKey Attacker { get; set; }

        public PublicKey AttackerAccount { get; set; }

        public PublicKey AttackingArmy { get; set; }

        public PublicKey UnitToAdd { get; set; }

        public PublicKey Defender { get; set; }

        public PublicKey DefenderAccount { get; set; }

        public PublicKey DefendingBase { get; set; }

        public PublicKey SeasonOwner { get; set; }

        public PublicKey SeasonAccount { get; set; }

        public PublicKey GameMatch { get; set; }

        public PublicKey MatchAttackingArmy { get; set; }

        public PublicKey MatchUnitAccount { get; set; }

        public PublicKey SystemProgram { get; set; }
    }

    public class TransitionMatchStateAccounts
    {
        public PublicKey Attacker { get; set; }

        public PublicKey AttackerAccount { get; set; }

        public PublicKey AttackingArmy { get; set; }

        public PublicKey Defender { get; set; }

        public PublicKey DefenderAccount { get; set; }

        public PublicKey DefendingBase { get; set; }

        public PublicKey SeasonOwner { get; set; }

        public PublicKey SeasonAccount { get; set; }

        public PublicKey GameMatch { get; set; }
    }

    public class AttackStructureAccounts
    {
        public PublicKey Attacker { get; set; }

        public PublicKey AttackerAccount { get; set; }

        public PublicKey AttackingArmy { get; set; }

        public PublicKey Defender { get; set; }

        public PublicKey DefenderAccount { get; set; }

        public PublicKey DefendingBase { get; set; }

        public PublicKey SeasonOwner { get; set; }

        public PublicKey SeasonAccount { get; set; }

        public PublicKey GameMatch { get; set; }

        public PublicKey MatchAttackingArmy { get; set; }

        public PublicKey AttackingMatchUnit { get; set; }

        public PublicKey MatchDefendingBase { get; set; }

        public PublicKey DefendingMatchStructure { get; set; }
    }

    public class AttackUnitAccounts
    {
        public PublicKey Attacker { get; set; }

        public PublicKey AttackerAccount { get; set; }

        public PublicKey AttackingArmy { get; set; }

        public PublicKey Defender { get; set; }

        public PublicKey DefenderAccount { get; set; }

        public PublicKey DefendingBase { get; set; }

        public PublicKey SeasonOwner { get; set; }

        public PublicKey SeasonAccount { get; set; }

        public PublicKey GameMatch { get; set; }

        public PublicKey MatchAttackingArmy { get; set; }

        public PublicKey AttackingMatchUnit { get; set; }

        public PublicKey MatchDefendingBase { get; set; }

        public PublicKey DefendingMatchStructure { get; set; }
    }

    public class DistributeMatchRewardsAccounts
    {
        public PublicKey Attacker { get; set; }

        public PublicKey AttackerAccount { get; set; }

        public PublicKey AttackingArmy { get; set; }

        public PublicKey Defender { get; set; }

        public PublicKey DefenderAccount { get; set; }

        public PublicKey DefendingBase { get; set; }

        public PublicKey SeasonOwner { get; set; }

        public PublicKey SeasonAccount { get; set; }

        public PublicKey GameMatch { get; set; }
    }

    public partial class Resources
    {
        public uint Wood { get; set; }

        public uint Stone { get; set; }

        public uint Iron { get; set; }

        public uint Steel { get; set; }

        public uint Mana { get; set; }

        public uint Gold { get; set; }

        public int Serialize(byte[] _data, int initialOffset)
        {
            int offset = initialOffset;
            _data.WriteU32(Wood, offset);
            offset += 4;
            _data.WriteU32(Stone, offset);
            offset += 4;
            _data.WriteU32(Iron, offset);
            offset += 4;
            _data.WriteU32(Steel, offset);
            offset += 4;
            _data.WriteU32(Mana, offset);
            offset += 4;
            _data.WriteU32(Gold, offset);
            offset += 4;
            return offset - initialOffset;
        }

        public static int Deserialize(ReadOnlySpan<byte> _data, int initialOffset, out Resources result)
        {
            int offset = initialOffset;
            result = new Resources();
            result.Wood = _data.GetU32(offset);
            offset += 4;
            result.Stone = _data.GetU32(offset);
            offset += 4;
            result.Iron = _data.GetU32(offset);
            offset += 4;
            result.Steel = _data.GetU32(offset);
            offset += 4;
            result.Mana = _data.GetU32(offset);
            offset += 4;
            result.Gold = _data.GetU32(offset);
            offset += 4;
            return offset - initialOffset;
        }
    }

    public partial class StructureStats
    {
        public ushort Rank { get; set; }

        public ushort Health { get; set; }

        public ushort Attack { get; set; }

        public ushort Defense { get; set; }

        public ushort Speed { get; set; }

        public ushort Range { get; set; }

        public byte AssignedWorkers { get; set; }

        public ushort CollectionInterval { get; set; }

        public long LastInteractionTime { get; set; }

        public int Serialize(byte[] _data, int initialOffset)
        {
            int offset = initialOffset;
            _data.WriteU16(Rank, offset);
            offset += 2;
            _data.WriteU16(Health, offset);
            offset += 2;
            _data.WriteU16(Attack, offset);
            offset += 2;
            _data.WriteU16(Defense, offset);
            offset += 2;
            _data.WriteU16(Speed, offset);
            offset += 2;
            _data.WriteU16(Range, offset);
            offset += 2;
            _data.WriteU8(AssignedWorkers, offset);
            offset += 1;
            _data.WriteU16(CollectionInterval, offset);
            offset += 2;
            _data.WriteS64(LastInteractionTime, offset);
            offset += 8;
            return offset - initialOffset;
        }

        public static int Deserialize(ReadOnlySpan<byte> _data, int initialOffset, out StructureStats result)
        {
            int offset = initialOffset;
            result = new StructureStats();
            result.Rank = _data.GetU16(offset);
            offset += 2;
            result.Health = _data.GetU16(offset);
            offset += 2;
            result.Attack = _data.GetU16(offset);
            offset += 2;
            result.Defense = _data.GetU16(offset);
            offset += 2;
            result.Speed = _data.GetU16(offset);
            offset += 2;
            result.Range = _data.GetU16(offset);
            offset += 2;
            result.AssignedWorkers = _data.GetU8(offset);
            offset += 1;
            result.CollectionInterval = _data.GetU16(offset);
            offset += 2;
            result.LastInteractionTime = _data.GetS64(offset);
            offset += 8;
            return offset - initialOffset;
        }
    }

    public partial class Position
    {
        public ushort X { get; set; }

        public ushort Y { get; set; }

        public int Serialize(byte[] _data, int initialOffset)
        {
            int offset = initialOffset;
            _data.WriteU16(X, offset);
            offset += 2;
            _data.WriteU16(Y, offset);
            offset += 2;
            return offset - initialOffset;
        }

        public static int Deserialize(ReadOnlySpan<byte> _data, int initialOffset, out Position result)
        {
            int offset = initialOffset;
            result = new Position();
            result.X = _data.GetU16(offset);
            offset += 2;
            result.Y = _data.GetU16(offset);
            offset += 2;
            return offset - initialOffset;
        }
    }

    public partial class UnitStats
    {
        public ushort Rank { get; set; }

        public ushort Health { get; set; }

        public ushort Attack { get; set; }

        public ushort Defense { get; set; }

        public ushort Speed { get; set; }

        public ushort Range { get; set; }

        public int Serialize(byte[] _data, int initialOffset)
        {
            int offset = initialOffset;
            _data.WriteU16(Rank, offset);
            offset += 2;
            _data.WriteU16(Health, offset);
            offset += 2;
            _data.WriteU16(Attack, offset);
            offset += 2;
            _data.WriteU16(Defense, offset);
            offset += 2;
            _data.WriteU16(Speed, offset);
            offset += 2;
            _data.WriteU16(Range, offset);
            offset += 2;
            return offset - initialOffset;
        }

        public static int Deserialize(ReadOnlySpan<byte> _data, int initialOffset, out UnitStats result)
        {
            int offset = initialOffset;
            result = new UnitStats();
            result.Rank = _data.GetU16(offset);
            offset += 2;
            result.Health = _data.GetU16(offset);
            offset += 2;
            result.Attack = _data.GetU16(offset);
            offset += 2;
            result.Defense = _data.GetU16(offset);
            offset += 2;
            result.Speed = _data.GetU16(offset);
            offset += 2;
            result.Range = _data.GetU16(offset);
            offset += 2;
            return offset - initialOffset;
        }
    }

    public enum FactionType : byte
    {
        Orc,
        Lizardmen,
        Fishmen
    }

    public enum MatchState : byte
    {
        Populating,
        InProgress,
        Cancelled,
        AwaitingRewardDistribution,
        Completed
    }

    public enum Victor : byte
    {
        None,
        Attacker,
        Defender
    }

    public enum SeasonState : byte
    {
        Open,
        Closed
    }

    public enum StructureType : byte
    {
        ThroneHall,
        Barracks,
        Blacksmith,
        ManaWell,
        CarpenterHut,
        PvpPortal,
        Mine,
        Quarry,
        LumberMill,
        ArcherTower,
        MageTower,
        Wall,
        SentryCreature
    }

    public enum UnitType : byte
    {
        Soldier,
        Archer,
        Siege,
        Healer
    }
}
