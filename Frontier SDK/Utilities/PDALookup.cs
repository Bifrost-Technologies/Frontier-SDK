using Frontier.Program;
using Solnet.Programs;
using Solnet.Programs.Utilities;
using Solnet.Wallet;
using System;
using System.Collections.Generic;
using System.Text;

namespace Frontier.Utilities
{
    /// <summary>
    /// PDA Lookup Class to make finding PDAs simple
    /// </summary>
    public static class PDALookup
    {

        public static PublicKey FindSeasonPDA(uint seasonNumber)
        {
            byte[] buff = new byte[4];
            buff.WriteU32(seasonNumber, 0);
            PublicKey.TryFindProgramAddress(new List<byte[]>()
            {
                Encoding.UTF8.GetBytes("season"),
                buff,
                FrontierProgram.Season1CreatorAddress
            },
            FrontierProgram.ProgramAddress,
            out PublicKey seasonPDA,
            out _);

            return seasonPDA;
        }
        public static PublicKey FindPlayerPDA(PublicKey ownerAddress)
        {
            PublicKey.TryFindProgramAddress(
            new List<byte[]>() {
                    Encoding.UTF8.GetBytes("player"),
                    ownerAddress
            },
            FrontierProgram.ProgramAddress,
            out PublicKey playerPDA,
            out _);
            return playerPDA;
        }
        public static PublicKey FindThroneHallPDA(PublicKey kingdomPDA)
        {
            byte[] idIndex = new byte[4];
            idIndex.WriteU32(1, 0);
            PublicKey.TryFindProgramAddress(new List<byte[]>()
            {
                idIndex,
                kingdomPDA
            },
            FrontierProgram.ProgramAddress,
            out PublicKey unitPDA,
            out _);

            return unitPDA;
        }
        public static PublicKey FindStructurePDA(PublicKey kingdomPDA, uint _structureIndex)
        {
            byte[] structureIndex = new byte[4];
            structureIndex.WriteU32(_structureIndex, 0);
            PublicKey.TryFindProgramAddress(new List<byte[]>()
            {
                structureIndex,
                kingdomPDA
            },
            FrontierProgram.ProgramAddress,
            out PublicKey structurePDA,
            out _);

            return structurePDA;
        }
        public static PublicKey FindUnitPDA(PublicKey armyPDA, uint _idIndex)
        {
            byte[] idIndex = new byte[4];
            idIndex.WriteU32(_idIndex, 0);
            PublicKey.TryFindProgramAddress(new List<byte[]>()
            {
                idIndex,
                armyPDA
            },
            FrontierProgram.ProgramAddress,
            out PublicKey unitPDA,
            out _);

            return unitPDA;
        }
        public static PublicKey FindKingdomPDA(PublicKey playerPDA)
        {
            PublicKey.TryFindProgramAddress(new List<byte[]>()
            {
                Encoding.UTF8.GetBytes("base"),
                playerPDA
            },
            FrontierProgram.ProgramAddress,
            out PublicKey kingdomPDA,
            out _);

            return kingdomPDA;
        }
     
   
        public static PublicKey FindArmyPDA(PublicKey playerPDA)
        {
            PublicKey.TryFindProgramAddress(new List<byte[]>()
            {
                Encoding.UTF8.GetBytes("army"),
                playerPDA
            },
            FrontierProgram.ProgramAddress,
            out PublicKey armyPDA,
            out _);

            return armyPDA;
        }
        public static PublicKey FindMatchPDA(PublicKey seasonPDA, PublicKey armyPDA, PublicKey kingdomPDA, uint matchNumber)
        {
            byte[] matchNumBytes = new byte[4];
            matchNumBytes.WriteU32(matchNumber, 0);
            PublicKey.TryFindProgramAddress(new List<byte[]>()
            {
                matchNumBytes,
                seasonPDA,
                armyPDA,
                kingdomPDA

            },
            FrontierProgram.ProgramAddress,
            out PublicKey matchPDA,
            out _);

            return matchPDA;
        }
        public static PublicKey FindMatchArmyPDA(PublicKey playerPDA, PublicKey matchPDA)
        {
            PublicKey.TryFindProgramAddress(new List<byte[]>()
            {
                Encoding.UTF8.GetBytes("army"),
                matchPDA,
                playerPDA
            },
            FrontierProgram.ProgramAddress,
            out PublicKey matchArmyPDA,
            out _);

            return matchArmyPDA;
        }
        public static PublicKey FindMatchKingdomPDA(PublicKey playerPDA, PublicKey matchPDA)
        {
            PublicKey.TryFindProgramAddress(new List<byte[]>()
            {
                Encoding.UTF8.GetBytes("base"),
                 matchPDA,
                playerPDA
            },
            FrontierProgram.ProgramAddress,
            out PublicKey matchKingdomPDA,
            out _);

            return matchKingdomPDA;
        }
    }
}
