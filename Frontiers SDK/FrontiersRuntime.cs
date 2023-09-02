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
using System.Numerics;
using LinkStream.Server;
using Frontiers.Accounts;
using Solnet.Rpc.Models;
using Org.BouncyCastle.Utilities;
using System.Net.Sockets;
using System.Net;

namespace Game
{
    public class Main
    {
        public static FrontierChainClient ChainClient { get; set; }
       public static LinkNetwork _LinkNetwork { get; set; }
        public static async void OnWorldBegin()
        {
            Debug.AddOnScreenMessage(-1, 20.0f, Color.PowderBlue, "UnrealSOLNET is initialized");
        
           
            await Task.CompletedTask;
        }
        public static async void OnWorldPostBegin()
        {
            _LinkNetwork = new LinkNetwork(50510, _LinkServerName: "NET Runtime Listener");
            var ip =IPAddress.Parse("127.0.0.1");
            _LinkNetwork.LinkServer = new TcpListener(ip, _LinkNetwork.LinkPort);
            Byte[] bytes = new Byte[1400];
            _LinkNetwork.isOnline = true;
            _LinkNetwork.LinkServer.Start();
            Debug.AddOnScreenMessage(-1, 20.0f, Color.PowderBlue, "Linkstream Module online!");
        }
        public static void OnWorldEnd()
        {
            _LinkNetwork.isOnline = false;
            _LinkNetwork = null;
        }
        //Client transaction railgun
        public static async void OnWorldDuringPhysicsTick(float deltaTime)
        {
            try
            {
                Byte[] bytes = new Byte[1400];
                if (ChainClient != null && ChainClient.PacketChamber != null)
                {
                    RailGun();
                }
                bool? hasPending = _LinkNetwork.LinkServer.Pending();
               
                if (hasPending != null && hasPending == true)
                {
                    try
                    {
                        //Blueprint is talking to NET runtime
                        _LinkNetwork.LinkClient = await _LinkNetwork.LinkServer.AcceptTcpClientAsync();
                        NetworkStream stream = _LinkNetwork.LinkClient.GetStream();
                        int i = await stream.ReadAsync(bytes, 0, bytes.Length);
                        string data = System.Text.Encoding.ASCII.GetString(bytes, 0, i);
                        string response = string.Empty;
                        string data_decrypted = string.Empty;
                        response = HandleRequestEvent(data);
                        Byte[] response_data = System.Text.Encoding.ASCII.GetBytes(response);
                        await stream.WriteAsync(response_data, 0, response_data.Length);
                        stream.Close();
                        _LinkNetwork.LinkClient.Close();
                        stream.Dispose();
                        _LinkNetwork.LinkClient.Dispose();
                        _LinkNetwork.LinkClient = null;
                    }
                    catch (Exception packetIssues)
                    {
                        Debug.AddOnScreenMessage(-1, 7.0f, Color.PowderBlue, "Read client error from TCP: " +packetIssues.Message);
                    }
                }
            }
            catch(Exception ex) 
            {

            }
        }
       public static void InitializeGameClient()
        {
            try
            {
                var rpc1 = ClientFactory.GetClient("");
               var rpc2 = ClientFactory.GetStreamingClient("");
                ChainClient = new FrontierChainClient(rpc1, rpc2, FrontierProgram.ProgramAddress);
                ChainClient.InitializeChainClient("test");
            }
            catch (Exception ex)
            {
                Debug.Log(LogLevel.Warning, ex.Message);
            }
        }
        public static async void CollectResources()
        {
            try
            {
                var pBase = await ChainClient.GetPlayerBaseAsync(ChainClient.PlayerPDAs["base"]);
                int count = 0;
                uint total = pBase.ParsedResult.StructureCount;
                List<TransactionInstruction> resourceInstructions = new List<TransactionInstruction>();
                while (count <= total)
                {
                    if (count == 0 || count == 1)
                    {
                        count++;
                        continue;
                    }

                    uint index = (uint)count;
                    CollectResourcesAccounts collectResourcesAccounts = new CollectResourcesAccounts { Owner = ChainClient.AirlockWallet.playerAddress, PlayerAccount = ChainClient.PlayerPDAs["player"], BaseAccount = ChainClient.PlayerPDAs["base"] };
                    collectResourcesAccounts.StructureAccount = PDALookup.FindStructurePDA(ChainClient.PlayerPDAs["base"], index);
                    resourceInstructions.Add(ChainClient.CollectResourcesInstruction(collectResourcesAccounts, index, FrontierProgram.ProgramAddress));
                    count++;
                }
                ChainClient.SendCollectResources(resourceInstructions);
                Debug.AddOnScreenMessage(-1, 7.0f, Color.PowderBlue, "Collecting Resources...");
            }
            catch(Exception ex)
            {

            }
        }public static void RetrieveUpdatedWalletBalance()
        {
            ChainClient.AirlockWallet.tokenWallet = TokenWallet.Load(ChainClient.RpcClient, ChainClient.AirlockWallet.tokenMintDatabase, ChainClient.AirlockWallet.playerAddress);
            ChainClient.AirlockWallet.UpdateBalance(ChainClient.AirlockWallet.tokenWallet.Sol);
        }
        public static void RailGun()
        {
            try
            {
                if (ChainClient.PacketChamber != null && ChainClient.PacketChamber.ToArray().Count() > 0)
                {
                    int delay = 200;
                    if (ChainClient.PacketChamber.Count() > 5)
                        delay = 50;
                    foreach (var tpkt in ChainClient.PacketChamber.ToArray())
                    {
                        var tx = ChainClient.RpcClient.SendTransaction(tpkt.signedTransaction, true, commitment: Solnet.Rpc.Types.Commitment.Finalized);
                        Debug.AddOnScreenMessage(-1, 7.0f, Color.PowderBlue, "Sending game transaction! - "+ tx.RawRpcResponse);
                        Debug.Log(LogLevel.Warning, "Sending a game transaction! - " + Convert.ToBase64String(tpkt.signedTransaction) + " | " + tx.RawRpcResponse);
                        var tpktRef = ChainClient.PacketChamber.Find(p => p == tpkt);
                        _ = ChainClient.PacketChamber.Remove(tpktRef);
                        //await Task.Delay(delay);
                    }
                }
            }catch (Exception ex)
            {
                Debug.Log(LogLevel.Warning, ex.Message);
            }
        }
        public static string HandleRequestEvent(string message)
        {
           // Debug.AddOnScreenMessage(-1, 7.0f, Color.PowderBlue, "Request received by Linkstream!" + message);
            if (message.Contains('|'))
            {
                try
                {
                    string[] parsedData = message.Split('|');
                    int requestID = Convert.ToInt32(parsedData[0]);
                    //Base building requests
                    if (requestID == 0)
                    {
                        ChainClient.SendInitPlayerAccounts(FactionType.Orc, FrontierProgram.ProgramAddress);
                    }
                    if (requestID == 1)
                    {
                       // Debug.AddOnScreenMessage(-1, 20.0f, Color.PowderBlue, "Found build request!");
                        byte buildingType = Convert.ToByte(parsedData[1]);
                        int offset = 20000;
                        int x = Convert.ToInt32(parsedData[2]) +offset;
                        int y = Convert.ToInt32(parsedData[3]) +offset;

                        x = 0;
                        y = 0;
                        Position pos = new Position { X = Convert.ToUInt16(x), Y = Convert.ToUInt16(y) };
                        BuildStructureAccounts buildStructureAccounts = new BuildStructureAccounts { Owner = ChainClient.AirlockWallet.playerAddress, PlayerAccount = ChainClient.PlayerPDAs["player"], BaseAccount = ChainClient.PlayerPDAs["base"], SystemProgram = Solnet.Programs.SystemProgram.ProgramIdKey };

                        buildStructureAccounts.StructureAccount = PDALookup.FindStructurePDA(ChainClient.PlayerPDAs["base"], ChainClient.Kingdom.StructureCount + 1);
                        ChainClient.SendBuildStructure(buildStructureAccounts,ChainClient.Kingdom.StructureCount + 1, (StructureType)buildingType, pos, FrontierProgram.ProgramAddress);
                     

                        Math.Round(ChainClient.AirlockWallet.Balance, 3).ToString();
                    }

                    if (requestID == 2)
                    {
                        var pBase = ChainClient.GetPlayerBaseAsync(ChainClient.PlayerPDAs["player"]).Result;
                        int count = 0;
                        uint total = pBase.ParsedResult.StructureCount - 1;
                        List<TransactionInstruction> resourceInstructions = new List<TransactionInstruction>();
                        while (count <= total)
                        {
                            if (count == 0)
                            {
                                count++;
                                continue;
                            }

                            uint index = (uint)count;
                            CollectResourcesAccounts collectResourcesAccounts = new CollectResourcesAccounts { Owner = ChainClient.AirlockWallet.playerAddress, PlayerAccount = ChainClient.PlayerPDAs["player"], BaseAccount = ChainClient.PlayerPDAs["base"] };
                            collectResourcesAccounts.StructureAccount = PDALookup.FindStructurePDA(ChainClient.PlayerPDAs["base"], index);
                            resourceInstructions.Add(ChainClient.CollectResourcesInstruction(collectResourcesAccounts, index, FrontierProgram.ProgramAddress));
                            count++;
                        }
                        ChainClient.SendCollectResources(resourceInstructions);
                        Math.Round(ChainClient.AirlockWallet.Balance, 3).ToString();
                    }
                    if (requestID == 3)
                    {

                    }
                    if (requestID == 4)
                    {
                        uint fromIndex = Convert.ToUInt32(parsedData[1]);
                        uint toIndex = Convert.ToUInt32(parsedData[2]);

                        AssignWorkerAccounts assignWorkersAccounts = new AssignWorkerAccounts { Owner = ChainClient.AirlockWallet.playerAddress, PlayerAccount = ChainClient.PlayerPDAs["player"], BaseAccount = ChainClient.PlayerPDAs["base"] };
                        assignWorkersAccounts.ToStructureAccount = PDALookup.FindStructurePDA(ChainClient.PlayerPDAs["base"], 2);
                        assignWorkersAccounts.FromStructureAccount = PDALookup.FindStructurePDA(ChainClient.PlayerPDAs["base"], 1);
                        ChainClient.SendAssignWorker(assignWorkersAccounts, fromIndex, toIndex, FrontierProgram.ProgramAddress);
                        Debug.AddOnScreenMessage(-1, 7.0f, Color.PowderBlue, "Sent assign worker transaction to Chamber!");
                        return Math.Round(ChainClient.AirlockWallet.Balance, 3).ToString();
                    }
              
                }catch (Exception ex)
                {
                    Debug.AddOnScreenMessage(-1, 20.0f, Color.PowderBlue, ex.Message);
                }
                return Math.Round(ChainClient.AirlockWallet.Balance, 3).ToString();
            } return Math.Round(ChainClient.AirlockWallet.Balance, 3).ToString();
        }
     
        
    }
}


