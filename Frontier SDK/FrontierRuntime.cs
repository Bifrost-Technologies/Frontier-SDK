using Solnet.Rpc;
using Frontier.Program;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnrealEngine.Framework;
using Frontier.Types;
using Frontier.Wallet;
using Frontier.Utilities;
using Solnet.Extensions;
using Solnet.Wallet;

namespace Frontier
{
    public static class FrontierRuntime
    {
        public static FrontierChainClient ChainClient { get; set; }

        public static FrontierGameWallet AirlockWallet { get; set; }

        public static Sequencer PacketChamber { get; set; }

       
        public static string InitializeChainClient(string password)
        {
            var rpc1 = ClientFactory.GetClient("https://stylish-crimson-tent.solana-mainnet.discover.quiknode.pro/dd0b3cd0bc4d3cbac5924a139c81e54e03404079/");
            var rpc2 = ClientFactory.GetStreamingClient("wss://stylish-crimson-tent.solana-mainnet.discover.quiknode.pro/dd0b3cd0bc4d3cbac5924a139c81e54e03404079/");
            ChainClient = new FrontierChainClient(rpc1, rpc2, FrontierProgram.ProgramAddress);
            AirlockWallet = new FrontierGameWallet(password);
            PacketChamber = new Sequencer();
            try
            {
                AirlockWallet.tokenWallet = TokenWallet.Load(ChainClient.RpcClient, AirlockWallet.tokenMintDatabase, AirlockWallet.playerAddress);
            }
            catch (Exception ex) 
            {
                Debug.Log(LogLevel.Warning, ex.Message);
            }
            Debug.Log(LogLevel.Warning, "Game Chain Client is initialized!");
            return "Game Chain Client is initialized!";
        }
        public static void RetrieveUpdatedWalletBalance()
        {
            AirlockWallet.tokenWallet = TokenWallet.Load(ChainClient.RpcClient, AirlockWallet.tokenMintDatabase, AirlockWallet.playerAddress);
            AirlockWallet.UpdateBalance(AirlockWallet.tokenWallet.Sol);
        }
        public static async Task RailGun()
        {
           if (PacketChamber != null && PacketChamber.ToArray().Count() > 0)
           {
               int delay = 200;
               if(PacketChamber.Count() > 5)
                   delay = 50;
               foreach (var tpkt in PacketChamber)
               {
                    await ChainClient.RpcClient.SendTransactionAsync(tpkt.signedTransaction, commitment: Solnet.Rpc.Types.Commitment.Confirmed);
                    Debug.Log(LogLevel.Warning, "Sending a game transaction! - " + Convert.ToBase64String(tpkt.signedTransaction));
                    await Task.Delay(delay);
               }
           }
        }
        public static void OnWorldBegin()
        {

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
           RailGun().Wait();
        }
    }
   
}
