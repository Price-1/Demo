using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using GTANetworkAPI;
using static Demo.Server.Libraries.ChatLibrary;
using static Demo.Server.Libraries.AdminLibrary;
using static Demo.Server.Players.Accounts.AccountManager;
using Demo.Server.Players.Accounts;

namespace Demo.Server.Players.Accounts
{
    public class AdminCommands : Script
    {
        [Command("a", "~o~USAGE:~s~ /a [Message]", GreedyArg = true)]
        public static void AdminCommand_AChat(Player player, String message)
        {
            if (!player.IsLoggedIn()) return;

            if (player.GetAdminRank() < 1) return;

            SendAdminChat(message);
        }

        [Command("SetAdminRank", "~o~USAGE:~s~ /SetAdminRank [Person's Account ID] [Desired Rank]", GreedyArg = false)]
        public static void AdminCommand_SetRank(Player player, int targetPlayer, int adminRank)
        {
            if (player.GetAdminRank() < AdminRank.RANK_LEADADMIN) return;

            if (!player.IsLoggedIn()) return;

            Player adminPlayer = (Player)PlayerAccountDictionary.Keys.Where(x => x.GetAccount().ID == targetPlayer).FirstOrDefault();

            if (adminPlayer == default(Player))
            {
                NAPI.Chat.SendChatMessageToPlayer(player, "The player can't be found!");
                return;
            }

            if (adminPlayer == player)
            {
                NAPI.Chat.SendChatMessageToPlayer(player, "You can't set your own rank!");
                return;
            }

            if (adminPlayer.GetAdminRank() == adminRank)
            {
                NAPI.Chat.SendChatMessageToPlayer(player, "The player already has that admin rank!");
                return;
            }

            if (player.GetAdminRank() == AdminRank.RANK_LEADADMIN && adminRank == AdminRank.RANK_MANAGEMENT)
            {
                NAPI.Chat.SendChatMessageToPlayer(player, "This rank is reserved for HeadStaff.");
                return;
            }

            if (adminPlayer.GetAdminName() == null)
            {
                adminPlayer.GetAccount().AdminName = adminPlayer.GetAccount().Username;
            }

            adminPlayer.GetAccount().AdminRank = adminRank;
        }
    }
}
