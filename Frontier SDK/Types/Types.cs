using Solnet.Programs.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frontier.Types
{
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
        public uint Health { get; set; }

        public uint Attack { get; set; }

        public uint Defense { get; set; }

        public uint Speed { get; set; }

        public uint Range { get; set; }

        public uint Cost { get; set; }

        public uint Upkeep { get; set; }

        public uint BuildTime { get; set; }

        public uint Level { get; set; }

        public uint Experience { get; set; }

        public uint ExperienceToLevel { get; set; }

        public int Serialize(byte[] _data, int initialOffset)
        {
            int offset = initialOffset;
            _data.WriteU32(Health, offset);
            offset += 4;
            _data.WriteU32(Attack, offset);
            offset += 4;
            _data.WriteU32(Defense, offset);
            offset += 4;
            _data.WriteU32(Speed, offset);
            offset += 4;
            _data.WriteU32(Range, offset);
            offset += 4;
            _data.WriteU32(Cost, offset);
            offset += 4;
            _data.WriteU32(Upkeep, offset);
            offset += 4;
            _data.WriteU32(BuildTime, offset);
            offset += 4;
            _data.WriteU32(Level, offset);
            offset += 4;
            _data.WriteU32(Experience, offset);
            offset += 4;
            _data.WriteU32(ExperienceToLevel, offset);
            offset += 4;
            return offset - initialOffset;
        }

        public static int Deserialize(ReadOnlySpan<byte> _data, int initialOffset, out StructureStats result)
        {
            int offset = initialOffset;
            result = new StructureStats();
            result.Health = _data.GetU32(offset);
            offset += 4;
            result.Attack = _data.GetU32(offset);
            offset += 4;
            result.Defense = _data.GetU32(offset);
            offset += 4;
            result.Speed = _data.GetU32(offset);
            offset += 4;
            result.Range = _data.GetU32(offset);
            offset += 4;
            result.Cost = _data.GetU32(offset);
            offset += 4;
            result.Upkeep = _data.GetU32(offset);
            offset += 4;
            result.BuildTime = _data.GetU32(offset);
            offset += 4;
            result.Level = _data.GetU32(offset);
            offset += 4;
            result.Experience = _data.GetU32(offset);
            offset += 4;
            result.ExperienceToLevel = _data.GetU32(offset);
            offset += 4;
            return offset - initialOffset;
        }
    }

    public partial class Position
    {
        public uint X { get; set; }

        public uint Y { get; set; }

        public int Serialize(byte[] _data, int initialOffset)
        {
            int offset = initialOffset;
            _data.WriteU32(X, offset);
            offset += 4;
            _data.WriteU32(Y, offset);
            offset += 4;
            return offset - initialOffset;
        }

        public static int Deserialize(ReadOnlySpan<byte> _data, int initialOffset, out Position result)
        {
            int offset = initialOffset;
            result = new Position();
            result.X = _data.GetU32(offset);
            offset += 4;
            result.Y = _data.GetU32(offset);
            offset += 4;
            return offset - initialOffset;
        }
    }
}
