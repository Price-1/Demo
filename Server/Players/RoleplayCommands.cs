using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;
using Demo.Server.Libraries;
using Demo.Server.Players.Accounts;
using static Demo.Server.Libraries.AdminLibrary;
using static Demo.Server.Players.Accounts.AccountManager;

namespace Demo.Server.Players
{
    public class RoleplayCommands : Script
    {
        [Command("me", "~o~USAGE:~s~ /me [action]", GreedyArg = true)]
        public static void RoleplayCommand_Me(Player player, string action)
        {
            if (!player.IsLoggedIn()) return;

            string roleplayLine = String.Format("** {0} {1}", player.Name, action);

            ChatLibrary.SendProximityRoleplay(player, 15, roleplayLine);
        }

        [Command("meclose", "~o~USAGE:~s~ /melow [action]", Alias = "mec", GreedyArg = true)]
        public static void RoleplayCommand_MeClose(Player player, string action)
        {
            if (!player.IsLoggedIn()) return;

            string roleplayLine = String.Format("** {0} {1}", player.Name, action);

            ChatLibrary.SendProximityRoleplay(player, 5, roleplayLine);
        }

        [Command("meloud", "~o~USAGE:~s~ /meloud [action]", Alias = "mel", GreedyArg = true)]
        public static void RoleplayCommand_Meloud(Player player, string action)
        {
            if (!player.IsLoggedIn()) return;

            string roleplayLine = String.Format("** {0} {1}", player.Name, action);

            ChatLibrary.SendProximityRoleplay(player, 30, roleplayLine);
        }

        [Command("my", "~o~USAGE:~s~ /my [action]", GreedyArg = true)]
        public static void RoleplayCommand_My(Player player, string action)
        {
            if (!player.IsLoggedIn()) return;

            string roleplayLine = string.Format("** {0}'s {1}", player.Name, action);

            ChatLibrary.SendProximityRoleplay(player, 15, roleplayLine);
        }

        [Command("myclose", "~o~USAGE:~s~ /myclose [action]", Alias = "myc", GreedyArg = true)]
        public static void RoleplayCommand_MyClose(Player player, string action)
        {
            if (!player.IsLoggedIn()) return;

            string roleplayLine = string.Format("** {0}'s {1}", player.Name, action);

            ChatLibrary.SendProximityRoleplay(player, 5, roleplayLine);
        }

        [Command("myloud", "~o~USAGE:~s~ /myloud [action]", Alias = "myl", GreedyArg = true)]
        public static void RoleplayCommand_MyLoud(Player player, string action)
        {
            if (!player.IsLoggedIn()) return;

            string roleplayLine = string.Format("** {0}'s {1}", player.Name, action);

            ChatLibrary.SendProximityRoleplay(player, 30, roleplayLine);
        }

        [Command("do", "~o~USAGE:~s~ /do [action]", GreedyArg = true)]
        public static void RoleplayCommand_Do(Player player, string action)
        {
            if (!player.IsLoggedIn()) return;

            string roleplayLine = string.Format("** {0}", action);

            ChatLibrary.SendProximityRoleplay(player, 15, roleplayLine);
        }

        [Command("doclose", "~o~USAGE:~s~ /dolow [action]", Alias = "doc", GreedyArg = true)]
        public static void RoleplayCommand_DoClose(Player player, string action)
        {
            if (!player.IsLoggedIn()) return;

            string roleplayLine = string.Format("** {0}", action);

            ChatLibrary.SendProximityRoleplay(player, 5, roleplayLine);
        }

        [Command("doloud", "~o~USAGE:~s~ /doloud [action]", Alias = "dol", GreedyArg = true)]
        public static void RoleplayCommand_DoLoud(Player player, string action)
        {
            if (!player.IsLoggedIn()) return;

            string roleplayLine = string.Format("** {0}", action);

            ChatLibrary.SendProximityRoleplay(player, 30, roleplayLine);
        }
           
        [Command("shout", "~o~USAGE:~s~ /shout [speech]", Alias = "yell,scream", GreedyArg = true)]
        public static void ChatCommand_Shout(Player player, string message)
        {
            if (!player.IsLoggedIn()) return;

            string roleplayLine = string.Format("{0} shouts: {1}", player.Name, message);

            ChatLibrary.SendProximitySpeech(player, 30, roleplayLine);
        }

        [Command("low", "~o~USAGE:~s~ /low [speech]", Alias = "quiet,mumble", GreedyArg = true)]
        public static void ChatCommand_Low(Player player, string message)
        {
            if (!player.IsLoggedIn()) return;

            string roleplayLine = string.Format("{0} says quietly: {1}", player.Name, message);

            ChatLibrary.SendProximitySpeech(player, 5, roleplayLine);
        }

        [Command("goto", "/goto [x] [y] [z]", Alias = "tp", GreedyArg = true)]
        public static void Command_Goto(Player player, string x, string y, string z)
        {
            if (player.GetAccount().AdminRank > AdminRank.RANK_DEFAULT)
            player.Position = new Vector3(Convert.ToSingle(x), Convert.ToSingle(y), Convert.ToSingle(z));
            player.Dimension = 0;
            NAPI.Chat.SendChatMessageToPlayer(player, "You have been teleported.");
        }

        [Command("vehicle", "/vehicle [vehicle name]", Alias = "veh")]
        public static void Command_SpawnVehicle(Player player, string vehicleName)
        {
            VehicleHash vehicle = (VehicleHash)Enum.Parse(typeof(VehicleHash), vehicleName);
            if (vehicle == 0)
            {
                NAPI.Chat.SendChatMessageToPlayer(player, "Could not find a vehicle with that name.");
                return;
            }

            Vehicle createdVehicle = NAPI.Vehicle.CreateVehicle(vehicle, player.Position, player.Rotation.Z, 0, 0, "test");
            player.SetIntoVehicle(createdVehicle.Handle, 0);

            NAPI.Chat.SendChatMessageToPlayer(player, "You have spawned a " + NAPI.Vehicle.GetVehicleDisplayName(vehicle));
        }

        [Command("respawn")]
        public static void Command_Respawn(Player player)
        {
            if (player.Dead)
            {
                NAPI.Player.SpawnPlayer(player, player.Position);
                NAPI.Chat.SendChatMessageToPlayer(player, "You have respawned!");
                return;
            }

            NAPI.Chat.SendChatMessageToPlayer(player, "You cannot respawn if you are not dead.");
        }
    }
}
