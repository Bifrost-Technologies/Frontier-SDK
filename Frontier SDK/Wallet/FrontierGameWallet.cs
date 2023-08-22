using Frontiers.Security;
using Microsoft.AspNetCore.DataProtection;
using Solnet.Extensions;
using Solnet.Rpc.Models;
using Solnet.Wallet;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnrealEngine.Framework;

namespace Frontiers.Wallet
{
    public class FrontierGameWallet
    {
        private string sessionKey { get; set; }
        private Titan playerTitan { get; set; }

        public PublicKey playerAddress { get; set; }
        public decimal Balance { get; set; }

        public bool freshWallet { get; set; }
        public TokenMintResolver tokenMintDatabase { get; set; }
        public TokenWallet tokenWallet { get; set; }
        public FrontierGameWallet(string password) 
        {
            playerTitan = new Titan(password);
            try
            {
                tokenMintDatabase = TokenMintResolver.Load();
            }catch(Exception ex) 
            {
                UnrealEngine.Framework.Debug.Log(LogLevel.Warning,"TokenMintResolver issue - "+ ex.Message);
            }
            
            LoadWallet();
            CreateWallet();
            playerAddress = Account.FromSecretKey(playerTitan.Shield.Unprotect(sessionKey)).PublicKey;
        }

        public byte[] SignMessage(byte[] transactionMessage)
        {
            //For extra layer of security scanning transaction messages before signing can be added
            //...
            //..
            Account _account = Account.FromSecretKey(playerTitan.Shield.Unprotect(sessionKey));
            return _account.Sign(transactionMessage);
        }
        public bool isLoaded()
        {
            if(sessionKey == null || sessionKey == String.Empty)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public void UpdateBalance(decimal _balance)
        {
           Balance = _balance;
        }
        public void CreateWallet()
        {
            try
            {
                if (sessionKey == null && freshWallet == true)
                {
                    Account newAccount = new Account();
                    sessionKey = playerTitan.Shield.Protect(newAccount.PrivateKey.Key);
                    SaveWallet();
                }
            }
            catch (Exception ex) 
            { 
                UnrealEngine.Framework.Debug.Log(LogLevel.Error, ex.ToString()); 
            }
        }
        public void SyncWallet(string secretKey)
        {
            try
            {
                if (sessionKey == null && freshWallet == true)
                {
                    sessionKey = playerTitan.Shield.Protect(secretKey);
                    SaveWallet();
                }
            }catch(Exception ex)
            {
                UnrealEngine.Framework.Debug.Log(LogLevel.Error, ex.ToString());
            }
        }
        public bool validCredentials()
        {
            try
            {
                var sanityCheck = playerTitan.Shield.Unprotect(sessionKey);
                return true;
            }catch(Exception ex)
            {
                return false;
            }
        }
        public void LoadWallet()
        {
            try
            {
                if(File.Exists(@"C:\\Program Files (x86)\Bifrost\Frontier\sealed_airlock.wallet"))
                {
                    sessionKey = File.ReadAllText(@"C:\\Program Files (x86)\Bifrost\Frontier\sealed_airlock.wallet");
                    freshWallet = false;
                }
                else
                {
                    freshWallet = true;
                }
               
            }catch(Exception ex) 
            {
                UnrealEngine.Framework.Debug.Log(LogLevel.Error, ex.ToString()); 
            }
        }
        public void SaveWallet()
        {
            try
            { 
                File.WriteAllText(@"C:\\Program Files (x86)\Bifrost\Frontier\sealed_airlock.wallet", sessionKey);
            }
            catch (Exception ex) 
            {
                UnrealEngine.Framework.Debug.Log(LogLevel.Error, ex.ToString());
            }
        }

    }
}
