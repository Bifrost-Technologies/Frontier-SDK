using Solnet.Rpc;
using Frontiers.Program;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnrealEngine.Framework;
using Frontiers.Types;
using Frontiers.Wallet;
using Frontiers.Utilities;
using Solnet.Extensions;
using Solnet.Wallet;
using Frontiers.Security;
using Frontiers;
using System.Drawing;

namespace Game
{
    public class Main
    {
        public static FrontierChainClient ChainClient { get; set; }
        public static string tempPW { get; set; }

        public static void OnWorldBegin()
        {
            Debug.AddOnScreenMessage(-1, 20.0f, Color.PowderBlue, "UnrealCLR is initialized");
        }
        public static void OnWorldPostBegin()
        {

        }
        public static void OnWorldEnd()
        {

        }
        //Client transaction railgun
        public static void OnWorldDuringPhysicsTick(float deltaTime)
        {
            if (ChainClient != null && ChainClient.PacketChamber != null)
            {
                RailGun();
            }
        }
        public static void InitializeChainClient()
        {
            try
            {
                var rpc1 = ClientFactory.GetClient("https://side-virulent-butterfly.solana-devnet.quiknode.pro/c14f8529f9bbaef2a55542c7e3fbc5eb06e5acf0/");
                var rpc2 = ClientFactory.GetStreamingClient("wss://side-virulent-butterfly.solana-devnet.quiknode.pro/c14f8529f9bbaef2a55542c7e3fbc5eb06e5acf0/");
                ChainClient = new FrontierChainClient(rpc1, rpc2, FrontierProgram.ProgramAddress);
                ChainClient.InitializeChainClient("test");
            }
            catch (Exception ex)
            {
                Debug.Log(LogLevel.Warning, ex.Message);

            }

        }
        public static void RetrieveUpdatedWalletBalance()
        {
            ChainClient.AirlockWallet.tokenWallet = TokenWallet.Load(ChainClient.RpcClient, ChainClient.AirlockWallet.tokenMintDatabase, ChainClient.AirlockWallet.playerAddress);
            ChainClient.AirlockWallet.UpdateBalance(ChainClient.AirlockWallet.tokenWallet.Sol);
        }
        public static async void RailGun()
        {
            if (ChainClient.PacketChamber != null && ChainClient.PacketChamber.ToArray().Count() > 0)
            {
                int delay = 200;
                if (ChainClient.PacketChamber.Count() > 5)
                    delay = 50;
                foreach (var tpkt in ChainClient.PacketChamber.ToArray())
                {
                    await ChainClient.RpcClient.SendTransactionAsync(tpkt.signedTransaction, commitment: Solnet.Rpc.Types.Commitment.Confirmed);
                    Debug.Log(LogLevel.Warning, "Sending a game transaction! - " + Convert.ToBase64String(tpkt.signedTransaction));
                    var tpktRef = ChainClient.PacketChamber.Find(p => p == tpkt);
                    _ = ChainClient.PacketChamber.Remove(tpktRef);
                    await Task.Delay(delay);
                }
            }
        }
    }
}


