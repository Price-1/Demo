using GTANetworkAPI;
using System;
using System.Collections.Generic;
using System.Text;
using Demo.Server.Libraries;
using Demo.Server.Players.Accounts;

namespace Demo.Server.Players.Accounts
{
    public class AccountCommands : Script
    {
        [Command("login", "/login [username] [password]")]
        public static async void AccountCommand_Login(Player player, string username, string password)
        {
            if (await AccountManager.TryLogin(player, username, password))
            {
                NAPI.Chat.SendChatMessageToPlayer(player, "Login Successful!.");
            }

            else
            {
                NAPI.Chat.SendChatMessageToPlayer(player, "Login Failed!.");
            }
        }

        [Command("register", "/register [username] [password]")]
        public static async void AccountCommand_Register(Player player, string username, string password)
        {
            if (await AccountManager.TryRegisteration(player, username, password))
            {
                NAPI.Chat.SendChatMessageToPlayer(player, "Registeration successful with the username " + username + ". Proceed with logging in using /login.");
            }
        }
    
    }
}
