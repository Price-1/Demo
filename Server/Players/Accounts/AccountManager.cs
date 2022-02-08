using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using GTANetworkAPI;
using Demo.Database;
using Demo.Database.DataModels;
using Demo.Server.Libraries;

namespace Demo.Server.Players.Accounts
{
    public class AccountManager : Script
    {

        public static Dictionary<Player, Account> PlayerAccountDictionary = new Dictionary<Player, Account>();

        [ServerEvent(Event.PlayerDisconnected)]
        public void OnPlayerDisconnect(Player player, DisconnectionType type, string reason)
        {
            if (PlayerAccountDictionary.ContainsKey(player))
            {
                PlayerAccountDictionary.Remove(player);
            }
        }

        public static async Task<bool> TryRegisteration(Player player, string username, string password)
        {
            if (await DoesAccountExist(username))
            {
                NAPI.Chat.SendChatMessageToPlayer(player, "This username is in use!");
                return false;
            }

            bool success = await CreateAccount(player, username, password);
            if (!success)
            {
                NAPI.Chat.SendChatMessageToPlayer(player, "Error encountered");
                return false;
            }
            return true;
        }

        public static async Task<bool> DoesAccountExist(String username)
        {
            return await GetAccountFromUsername(username) != default(Account);
        }

        /// <summary>
        /// Creates an account with the specified username and password.<br></br>
        /// <b>NOTE:</b> does not do any account username validation.
        /// </summary>
        /// <param name="player">The player who the account is being created for.</param>
        /// <param name="username">The username of the new account.</param>
        /// <param name="password">The plaintext passowrd of the new account as input by the user.</param>
        /// <returns>True if successful, false of not. Outputs stack trace to console if false.</returns>
        public static async Task<bool> CreateAccount(Player player, string username, string password)
        {
            using (DataContext context = new DataContext())
            {

                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);
                Account newAccount = new Account
                {
                    Username = username,
                    Password = hashedPassword,
                    AdminRank = AdminLibrary.AdminRank.RANK_DEFAULT,
                    AdminName = username
                };
                context.Accounts.Add(newAccount);
                context.Entry(newAccount).State = EntityState.Added;

                try
                {
                    await context.SaveChangesAsync();
                    Console.WriteLine("New account created with username " + newAccount.Username);
                    return true;
                }
                catch (Exception e)
                {
                    Console.WriteLine("An error occurred attempting to register from username " + username + ":");
                    Console.WriteLine(e.StackTrace);
                    Console.WriteLine(e.Message);
                    return false;
                }
            }
        }

        /// <summary>
        /// Attempts to retrieve an account from the database with a given username (unique identifier).
        /// If an account does not exist, default(Account) is returned.
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public static async Task<Account> GetAccountFromUsername(string username)
        {
            using (DataContext db = new DataContext())
            {
                return await db.Accounts.FirstOrDefaultAsync(x => x.Username == username);
            }
        }

        public static async Task<bool> TryLogin(Player player, string username, string password)
        {
            // Make sure player isn't already logged in.
            if (player.IsLoggedIn())
            {
                NAPI.ClientEventThreadSafe.TriggerClientEvent(player, "You are already logged in.");
                return false;
            }

            // Does an account exist with that username?
            if (!await AccountManager.DoesAccountExist(username))
            {
                NAPI.ClientEventThreadSafe.TriggerClientEvent(player, "Your username or password is incorrect.");
                return false;
            }

            // Do the passwords match?
            Account retrievedAccount = await AccountManager.GetAccountFromUsername(username);
            if (!BCrypt.Net.BCrypt.Verify(password, retrievedAccount.Password))
            {
                NAPI.ClientEventThreadSafe.TriggerClientEvent(player, "Your username or password is incorrect.");
                return false;
            }

            bool loginSuccessful = await LoginPlayer(player, retrievedAccount);
            if (!loginSuccessful)
            {
                NAPI.ClientEventThreadSafe.TriggerClientEvent(player, "There was an error logging you in. Please reconnect and try again.");
                return false;
            }

            return true;
        }

        /// <summary>
        /// Once validation is complete in <see cref="TryLogin(Player, string, string)"/>, logs the player into their respective account.
        /// </summary>
        /// <param name="player">The player who is logging in.</param>
        /// <param name="account">The account they are logging in to.</param>
        /// <returns>True if successful, false if errors (already logged in).</returns>
        private static async Task<bool> LoginPlayer(Player player, Account account)
        {
            if (account == null || account == default(Account)) return false;

            if (PlayerAccountDictionary.ContainsKey(player)) return false;

            if (PlayerAccountDictionary.ContainsValue(account)) return false;

            PlayerAccountDictionary.Add(player, account);

            Console.WriteLine("[LOGIN] " + player.Name + " has logged in with username: " + account.Username);

            using (DataContext context = new DataContext())
            {
                context.Entry(account).State = EntityState.Modified;
                await context.SaveChangesAsync();
            }

            NAPI.ClientEvent.TriggerClientEvent(player, "LoginSuccessful");
            return true;
        }

        public static bool IsValidPassword(string username, string password)
        {
            return password.Length > 3 && !password.Contains(username);
        }
    }
}
