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

namespace Frontier
{
    public static class FrontierRuntime
    {
        public static FrontierChainClient ChainClient { get; set; }

        private static FrontierGameWallet AirlockWallet { get; set; }

        public static string InitializeChainClient()
        {
            var rpc1 = ClientFactory.GetClient("https://stylish-crimson-tent.solana-mainnet.discover.quiknode.pro/dd0b3cd0bc4d3cbac5924a139c81e54e03404079/");
            var rpc2 = ClientFactory.GetStreamingClient("wss://stylish-crimson-tent.solana-mainnet.discover.quiknode.pro/dd0b3cd0bc4d3cbac5924a139c81e54e03404079/");
            ChainClient = new FrontierChainClient(rpc1, rpc2, FrontierProgram.FrontierProgramAddress);
           // Debug.Log(LogLevel.Warning, "Solana RPC client is initialized!");
            return "Solana RPC client is initialized!";
        }
        public static async void TestFrontierProgram()
        {
            try
            {
                InitPlayerAccountsAccounts Player = new InitPlayerAccountsAccounts
                {
                    Owner = AirlockWallet.playerAddress,
                    PlayerAccount = PDALookup.FindPlayerPDA(AirlockWallet.playerAddress),
                    BaseAccount = PDALookup.FindKingdomPDA(AirlockWallet.playerAddress),
                    ArmyAccount = PDALookup.FindArmyPDA(AirlockWallet.playerAddress)
                };
              var initPlayer =  await ChainClient.InitPlayerAccountsInstruction(Player);

               await ChainClient.RpcClient.SendTransactionAsync(initPlayer);
               // Debug.Log(LogLevel.Warning, "Testing frontier program...");
                var atest = await ChainClient.GetPlayersAsync();
          
                if (atest != null && atest.ParsedResult != null)
                {
                    var players = atest.ParsedResult;
                    foreach (var _player in players)
                    {
                       // Debug.Log(LogLevel.Warning, "Player Owner: " + _player.OwnerPubkey);
                      //  Debug.Log(LogLevel.Warning, "Player Rank:" + _player.Rank);
                    }
                }
            }
            catch (Exception ex)
            {
               // Debug.Log(LogLevel.Warning, ex.Message);
            }
           
        }

    }
}
