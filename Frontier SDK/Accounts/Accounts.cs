using Frontier.Types;
using Solnet.Programs.Utilities;
using Solnet.Wallet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frontier.Accounts
{
    public partial class Army
    {
        public static ulong AccID => 16876076247667461172UL;
        public static ReadOnlySpan<byte> AccIDbytes => new byte[] { 52, 76, 160, 24, 239, 221, 51, 234 };
        public static string EncodedAccID => "9kNPy72CECH";
        public PublicKey PlayerAccount { get; set; }

        public uint ArmySize { get; set; }

        public uint ArmyMaxSize { get; set; }

        public uint Rating { get; set; }

        public FactionType Faction { get; set; }

        public bool IsInitialized { get; set; }

        public static Army Deserialize(ReadOnlySpan<byte> _data)
        {
            int offset = 0;
            ulong accountHashValue = _data.GetU64(offset);
            offset += 8;
            if (accountHashValue != AccID)
            {
                return null;
            }

            Army result = new Army();
            result.PlayerAccount = _data.GetPubKey(offset);
            offset += 32;
            result.ArmySize = _data.GetU32(offset);
            offset += 4;
            result.ArmyMaxSize = _data.GetU32(offset);
            offset += 4;
            result.Rating = _data.GetU32(offset);
            offset += 4;
            result.Faction = (FactionType)_data.GetU8(offset);
            offset += 1;
            result.IsInitialized = _data.GetBool(offset);
            offset += 1;
            return result;
        }
    }

    public partial class GameMatch
    {
        public static ulong AccID => 2846051679389268823UL;
        public static ReadOnlySpan<byte> AccIDbytes => new byte[] { 87, 187, 102, 98, 236, 52, 127, 39 };
        public static string EncodedAccID => "Fg7R48zaW22";
        public uint Id { get; set; }

        public MatchState State { get; set; }

        public uint ActiveUnits { get; set; }

        public uint ActiveStructures { get; set; }

        public bool ThroneHallActive { get; set; }

        public Victor Victor { get; set; }

        public Resources MatchReward { get; set; }

        public PublicKey AttackingArmy { get; set; }

        public PublicKey DefendingBase { get; set; }

        public bool IsInitialized { get; set; }

        public static GameMatch Deserialize(ReadOnlySpan<byte> _data)
        {
            int offset = 0;
            ulong accountHashValue = _data.GetU64(offset);
            offset += 8;
            if (accountHashValue != AccID)
            {
                return null;
            }

            GameMatch result = new GameMatch();
            result.Id = _data.GetU32(offset);
            offset += 4;
            result.State = (MatchState)_data.GetU8(offset);
            offset += 1;
            result.ActiveUnits = _data.GetU32(offset);
            offset += 4;
            result.ActiveStructures = _data.GetU32(offset);
            offset += 4;
            result.ThroneHallActive = _data.GetBool(offset);
            offset += 1;
            result.Victor = (Victor)_data.GetU8(offset);
            offset += 1;
            offset += Resources.Deserialize(_data, offset, out var resultMatchReward);
            result.MatchReward = resultMatchReward;
            result.AttackingArmy = _data.GetPubKey(offset);
            offset += 32;
            result.DefendingBase = _data.GetPubKey(offset);
            offset += 32;
            result.IsInitialized = _data.GetBool(offset);
            offset += 1;
            return result;
        }
    }

    public partial class PlayerBase
    {
        public static ulong AccID => 15322206904575986686UL;
        public static ReadOnlySpan<byte> AccIDbytes => new byte[] { 254, 11, 74, 202, 255, 105, 163, 212 };
        public static string EncodedAccID => "jVYg4oU8vRh";
        public PublicKey PlayerAccount { get; set; }

        public uint StructureCount { get; set; }

        public uint BaseSize { get; set; }

        public uint MaxBaseSize { get; set; }

        public uint MaxWorkers { get; set; }

        public ushort Rating { get; set; }

        public FactionType Faction { get; set; }

        public bool IsInitialized { get; set; }

        public static PlayerBase Deserialize(ReadOnlySpan<byte> _data)
        {
            int offset = 0;
            ulong accountHashValue = _data.GetU64(offset);
            offset += 8;
            if (accountHashValue != AccID)
            {
                return null;
            }

            PlayerBase result = new PlayerBase();
            result.PlayerAccount = _data.GetPubKey(offset);
            offset += 32;
            result.StructureCount = _data.GetU32(offset);
            offset += 4;
            result.BaseSize = _data.GetU32(offset);
            offset += 4;
            result.MaxBaseSize = _data.GetU32(offset);
            offset += 4;
            result.MaxWorkers = _data.GetU32(offset);
            offset += 4;
            result.Rating = _data.GetU16(offset);
            offset += 2;
            result.Faction = (FactionType)_data.GetU8(offset);
            offset += 1;
            result.IsInitialized = _data.GetBool(offset);
            offset += 1;
            return result;
        }
    }

    public partial class Player
    {
        public static ulong AccID => 15766710478567431885UL;
        public static ReadOnlySpan<byte> AccIDbytes => new byte[] { 205, 222, 112, 7, 165, 155, 206, 218 };
        public static string EncodedAccID => "bSBoKNsSHuj";
        public PublicKey OwnerPubkey { get; set; }

        public byte Rank { get; set; }

        public uint Experience { get; set; }

        public Resources Resources { get; set; }

        public FactionType Faction { get; set; }

        public bool IsInitialized { get; set; }

        public static Player Deserialize(ReadOnlySpan<byte> _data)
        {
            int offset = 0;
            ulong accountHashValue = _data.GetU64(offset);
            offset += 8;
            if (accountHashValue != AccID)
            {
                return null;
            }

            Player result = new Player();
            result.OwnerPubkey = _data.GetPubKey(offset);
            offset += 32;
            result.Rank = _data.GetU8(offset);
            offset += 1;
            result.Experience = _data.GetU32(offset);
            offset += 4;
            offset += Resources.Deserialize(_data, offset, out var resultResources);
            result.Resources = resultResources;
            result.Faction = (FactionType)_data.GetU8(offset);
            offset += 1;
            result.IsInitialized = _data.GetBool(offset);
            offset += 1;
            return result;
        }
    }

    public partial class Season
    {
        public static ulong AccID => 3456686113049887564UL;
        public static ReadOnlySpan<byte> AccIDbytes => new byte[] { 76, 67, 93, 156, 180, 157, 248, 47 };
        public static string EncodedAccID => "DkrBsPxjgBp";
        public uint SeasonId { get; set; }

        public PublicKey SeasonInitializer { get; set; }

        public uint MatchCount { get; set; }

        public uint PlayerCount { get; set; }

        public SeasonState State { get; set; }

        public bool IsInitialized { get; set; }

        public static Season Deserialize(ReadOnlySpan<byte> _data)
        {
            int offset = 0;
            ulong accountHashValue = _data.GetU64(offset);
            offset += 8;
            if (accountHashValue != AccID)
            {
                return null;
            }

            Season result = new Season();
            result.SeasonId = _data.GetU32(offset);
            offset += 4;
            result.SeasonInitializer = _data.GetPubKey(offset);
            offset += 32;
            result.MatchCount = _data.GetU32(offset);
            offset += 4;
            result.PlayerCount = _data.GetU32(offset);
            offset += 4;
            result.State = (SeasonState)_data.GetU8(offset);
            offset += 1;
            result.IsInitialized = _data.GetBool(offset);
            offset += 1;
            return result;
        }
    }

    public partial class Structure
    {
        public static ulong AccID => 16504117151182136175UL;
        public static ReadOnlySpan<byte> AccIDbytes => new byte[] { 111, 163, 164, 63, 27, 103, 10, 229 };
        public static string EncodedAccID => "Kg3LfHYE7K2";
        public uint Id { get; set; }

        public PublicKey PlayerBase { get; set; }

        public PublicKey Player { get; set; }

        public StructureType StructureType { get; set; }

        public StructureStats Stats { get; set; }

        public Position Position { get; set; }

        public bool IsDestroyed { get; set; }

        public bool IsInitialized { get; set; }

        public static Structure Deserialize(ReadOnlySpan<byte> _data)
        {
            int offset = 0;
            ulong accountHashValue = _data.GetU64(offset);
            offset += 8;
            if (accountHashValue != AccID)
            {
                return null;
            }

            Structure result = new Structure();
            result.Id = _data.GetU32(offset);
            offset += 4;
            result.PlayerBase = _data.GetPubKey(offset);
            offset += 32;
            result.Player = _data.GetPubKey(offset);
            offset += 32;
            result.StructureType = (StructureType)_data.GetU8(offset);
            offset += 1;
            offset += StructureStats.Deserialize(_data, offset, out var resultStats);
            result.Stats = resultStats;
            offset += Position.Deserialize(_data, offset, out var resultPosition);
            result.Position = resultPosition;
            result.IsDestroyed = _data.GetBool(offset);
            offset += 1;
            result.IsInitialized = _data.GetBool(offset);
            offset += 1;
            return result;
        }
    }

    public partial class Unit
    {
        public static ulong AccID => 1207677667194103049UL;
        public static ReadOnlySpan<byte> AccIDbytes => new byte[] { 9, 229, 70, 148, 112, 136, 194, 16 };
        public static string EncodedAccID => "2ezzK5jZAS3";
        public uint Id { get; set; }

        public PublicKey Player { get; set; }

        public PublicKey Army { get; set; }

        public UnitType UnitType { get; set; }

        public UnitStats Stats { get; set; }

        public bool IsDestroyed { get; set; }

        public bool IsInitialized { get; set; }

        public static Unit Deserialize(ReadOnlySpan<byte> _data)
        {
            int offset = 0;
            ulong accountHashValue = _data.GetU64(offset);
            offset += 8;
            if (accountHashValue != AccID)
            {
                return null;
            }

            Unit result = new Unit();
            result.Id = _data.GetU32(offset);
            offset += 4;
            result.Player = _data.GetPubKey(offset);
            offset += 32;
            result.Army = _data.GetPubKey(offset);
            offset += 32;
            result.UnitType = (UnitType)_data.GetU8(offset);
            offset += 1;
            offset += UnitStats.Deserialize(_data, offset, out var resultStats);
            result.Stats = resultStats;
            result.IsDestroyed = _data.GetBool(offset);
            offset += 1;
            result.IsInitialized = _data.GetBool(offset);
            offset += 1;
            return result;
        }
    }
}
