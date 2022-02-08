using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace Demo.Server.Players
{
    public class PlayerConnection : Script
    {
        [ServerEvent(Event.PlayerConnected)]
        public static void OnPlayerConnect(Player player)
        {
            NAPI.Util.ConsoleOutput("[CONNECTION] Player " + player.Name + " has connected to the server.", ConsoleColor.Blue);

            NAPI.Chat.SendChatMessageToPlayer(player, "Type /login or /register to continue.");
        }

    }
}
