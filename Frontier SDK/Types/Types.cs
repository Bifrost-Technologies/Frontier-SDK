using Solnet.Programs.Utilities;
using Solnet.Wallet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frontier.Types
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

    public class CollectResourcesAccounts
    {
        public PublicKey Owner { get; set; }

        public PublicKey PlayerAccount { get; set; }

        public PublicKey BaseAccount { get; set; }

        public PublicKey StructureAccount { get; set; }
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
}
