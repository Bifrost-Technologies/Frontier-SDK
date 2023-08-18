using Frontier.Program;
using Solnet.Programs;
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
       
        public static PublicKey FindKingdomPDA(PublicKey playerAddress)
        {
            PublicKey.TryFindProgramAddress(new List<byte[]>()
            {
                Encoding.UTF8.GetBytes("base"),
                FrontierProgram.FrontierProgramAddress,
                playerAddress
            },
            FrontierProgram.FrontierProgramAddress,
            out PublicKey baseAddress,
            out _);

            return baseAddress;
        }
     
        public static PublicKey FindPlayerPDA(PublicKey ownerAddress)
        {
            PublicKey.TryFindProgramAddress(
            new List<byte[]>() {
                    Encoding.UTF8.GetBytes("player"),
                    FrontierProgram.FrontierProgramAddress,
                    ownerAddress
            },
            FrontierProgram.FrontierProgramAddress,
            out PublicKey playerAddress,
            out _);
            return playerAddress;
        }
        public static PublicKey FindArmyPDA(PublicKey playerAddress)
        {
            PublicKey.TryFindProgramAddress(new List<byte[]>()
            {
                Encoding.UTF8.GetBytes("army"),
                FrontierProgram.FrontierProgramAddress,
                playerAddress
            },
            FrontierProgram.FrontierProgramAddress,
            out PublicKey armyAddress,
            out _);

            return armyAddress;
        }
    }
}
