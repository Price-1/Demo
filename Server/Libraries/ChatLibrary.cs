using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;
using Demo.Server.Players.Accounts;
using Demo.Server.Libraries;
using static Demo.Server.Libraries.AdminLibrary;

namespace Demo.Server.Libraries
{
    public class ChatLibrary
    {
        public static class ChatColors
        {
            public static string COLOR_WHITE = "!{#FFFFFF}";
            public static string COLOR_LIGHTGREY = "!{#B3B3B3}";
            public static string COLOR_GREY = "!{#7D7D7D}";
            public static string COLOR_ROLEPLAY = "!{#BCAAD4}";
            public static string COLOR_DISTANT_ROLEPLAY = "!{#C8BED4}";
        }

        public static List<Player> GetAdminPlayers(int adminRank)
        {
            List<Player> adminPlayers = new List<Player>();
            foreach (KeyValuePair<Player, Database.DataModels.Account> adminPlayer in AccountManager.PlayerAccountDictionary)
            {
                if (adminPlayer.Value.AdminRank >= adminRank)
                    adminPlayers.Add(adminPlayer.Key);
            }
            return adminPlayers;
        }

        /// <summary>
        /// Sends a chat message to all players with access to the general admin chat.
        /// </summary>
        /// <param name="player">The admin that sent the message.</param>
        /// <param name="message">The message being sent to other admins.</param>
        public static void SendAdminChat(String message)
        {
            List<Player> adminPlayers = GetAdminPlayers(AdminLibrary.AdminRank.RANK_LEVEL1);

            foreach (Player adminPlayer in adminPlayers)
            {

                if (adminPlayer.GetAdminRank() < AdminRank.RANK_LEVEL2)
                { string playerStringHex = AdminColors.COLOR_SUPPORT; }

                else if (adminPlayer.GetAdminRank() < AdminRank.RANK_LEADADMIN)
                { string playerStringHex = AdminColors.COLOR_ADMIN; }

                else if (adminPlayer.GetAdminRank() < AdminRank.RANK_MANAGEMENT)
                { string playerStringHex = AdminColors.COLOR_LEADADMIN; }

                else
                { string playerStringHex = AdminColors.COLOR_MANAGEMENT; }

                NAPI.Chat.SendChatMessageToPlayer(adminPlayer, adminPlayer.GetAdminName() + ": " + message);
            }

        }

        /// <summary>
        /// Given a source player, sends speech to local players.
        /// </summary>
        /// <param name="player">The player who is the source of the chat message.</param>
        /// <param name="radius">The radius of the chat message.</param>
        /// <param name="message">The message to send in a radius.</param>
        public static void SendProximitySpeech(Player player, float radius, string message)
        {
            List<Player> localPlayers = NAPI.Player.GetPlayersInRadiusOfPosition(radius, player.Position);
            if (localPlayers == null || localPlayers.Count == 0) return;

            foreach (Player localPlayer in localPlayers)
            {
                NAPI.Chat.SendChatMessageToPlayer(localPlayer, message);
            }
        }

        /// <summary>
        /// Given a source player, sends 'normal' roleplay - i.e. roleplay at the normal distance.
        /// </summary>
        /// <param name="player">The player who is the source of the roleplay.</param>
        /// <param name="radius">The radius of the roleplay message.</param>
        /// <param name="message">The message to send in a radius.</param>
        public static void SendProximityRoleplay(Player player, float radius, string message)
        {
            List<Player> localPlayers = NAPI.Player.GetPlayersInRadiusOfPosition(radius, player.Position);
            if (localPlayers.Count == 0) return;

            foreach (Player localPlayer in localPlayers)
            {
                NAPI.Chat.SendChatMessageToPlayer(localPlayer, message);
            }
        }

    }
}
