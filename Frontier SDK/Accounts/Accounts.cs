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
            public static ulong AccountId => 16876076247667461172UL;
            public static ReadOnlySpan<byte> AccountIdBytes => new byte[]{52, 76, 160, 24, 239, 221, 51, 234};
            public static string EncodedAccountId => "9kNPy72CECH";
            public PublicKey PlayerAccount { get; set; }

            public uint UnitCount { get; set; }

            public uint ArmySize { get; set; }

            public uint Rating { get; set; }

            public bool IsInitialized { get; set; }

            public static Army Deserialize(ReadOnlySpan<byte> _data)
            {
                int offset = 0;
                ulong accountHashValue = _data.GetU64(offset);
                offset += 8;
                if (accountHashValue != AccountId)
                {
                    return null;
                }

                Army result = new Army();
                result.PlayerAccount = _data.GetPubKey(offset);
                offset += 32;
                result.UnitCount = _data.GetU32(offset);
                offset += 4;
                result.ArmySize = _data.GetU32(offset);
                offset += 4;
                result.Rating = _data.GetU32(offset);
                offset += 4;
                result.IsInitialized = _data.GetBool(offset);
                offset += 1;
                return result;
            }
        }

        public partial class PlayerBase
        {
            public static ulong AccountId => 15322206904575986686UL;
            public static ReadOnlySpan<byte> AccountIdBytes => new byte[]{254, 11, 74, 202, 255, 105, 163, 212};
            public static string EncodedAccountId => "jVYg4oU8vRh";
            public PublicKey PlayerAccount { get; set; }

            public uint StructureCount { get; set; }

            public uint BaseSize { get; set; }

            public uint MaxBaseSize { get; set; }

            public uint Rating { get; set; }

            public bool IsInitialized { get; set; }

            public static PlayerBase Deserialize(ReadOnlySpan<byte> _data)
            {
                int offset = 0;
                ulong accountHashValue = _data.GetU64(offset);
                offset += 8;
                if (accountHashValue != AccountId)
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
                result.Rating = _data.GetU32(offset);
                offset += 4;
                result.IsInitialized = _data.GetBool(offset);
                offset += 1;
                return result;
            }
        }

        public partial class Player
        {
            public static ulong AccountId => 15766710478567431885UL;
            public static ReadOnlySpan<byte> AccountIdBytes => new byte[]{205, 222, 112, 7, 165, 155, 206, 218};
            public static string EncodedAccountId => "bSBoKNsSHuj";
            public PublicKey OwnerPubkey { get; set; }

            public byte Rank { get; set; }

            public uint Experience { get; set; }

            public Resources Resources { get; set; }

            public bool IsInitialized { get; set; }

            public static Player Deserialize(ReadOnlySpan<byte> _data)
            {
                int offset = 0;
                ulong accountHashValue = _data.GetU64(offset);
                offset += 8;
                if (accountHashValue != AccountId)
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
                result.IsInitialized = _data.GetBool(offset);
                offset += 1;
                return result;
            }
        }

        public partial class Structure
        {
            public static ulong AccountId => 16504117151182136175UL;
            public static ReadOnlySpan<byte> AccountIdBytes => new byte[]{111, 163, 164, 63, 27, 103, 10, 229};
            public static string EncodedAccountId => "Kg3LfHYE7K2";
            public uint Id { get; set; }

            public PublicKey PlayerBase { get; set; }

            public PublicKey Player { get; set; }

            public uint Rank { get; set; }

            public uint StructureType { get; set; }

            public StructureStats Stats { get; set; }

            public Position Position { get; set; }

            public bool IsInitialized { get; set; }

            public static Structure Deserialize(ReadOnlySpan<byte> _data)
            {
                int offset = 0;
                ulong accountHashValue = _data.GetU64(offset);
                offset += 8;
                if (accountHashValue != AccountId)
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
                result.Rank = _data.GetU32(offset);
                offset += 4;
                result.StructureType = _data.GetU32(offset);
                offset += 4;
                offset += StructureStats.Deserialize(_data, offset, out var resultStats);
                result.Stats = resultStats;
                offset += Position.Deserialize(_data, offset, out var resultPosition);
                result.Position = resultPosition;
                result.IsInitialized = _data.GetBool(offset);
                offset += 1;
                return result;
            }
        }
}
