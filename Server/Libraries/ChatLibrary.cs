using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;
using Demo.Server.Players.Accounts;
using Demo.Server.Libraries;

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
