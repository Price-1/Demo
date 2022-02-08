using GTANetworkAPI;
using System;
using System.Collections.Generic;
using System.Text;
using Demo.Server.Libraries;
using Demo.Database.DataModels;

namespace Demo.Server.Players.Accounts
{
    public static class PlayerExtentions
    {
        public static Account GetAccount(this Player player)
        {
            if (!AccountManager.PlayerAccountDictionary.ContainsKey(player)) return null;

            return AccountManager.PlayerAccountDictionary[player];
        }

        /// <summary>
        /// Checks if the player is logged in.
        /// </summary>
        /// <param name="player">The player to check.</param>
        /// <returns>True if logged in, false if not.</returns>
        public static bool IsLoggedIn(this Player player)
        {
            return player.GetAccount() != null;
        }

        public static int GetAdminRank(this Player player)
        {
            if (!player.IsLoggedIn()) return -1;

            return player.GetAccount().AdminRank;
        }

        /// <summary>
        /// Returns the admin name of the player.
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        public static string GetAdminName(this Player player)
        {
            if (!player.IsLoggedIn()) return null;

            return player.GetAccount().AdminName;
        }
    }
}
