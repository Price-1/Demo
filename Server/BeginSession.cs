using Demo.Database;
using GTANetworkAPI;
using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;

namespace Demo
{
    public class BeginSession : Script
    {
        /// <summary>
        /// Constructor that calls the function to verify the connection to the database and disallows players from spawning once connected by default.
        /// </summary>
        public BeginSession()
        {
            VerifyConnectionToDB();

            NAPI.Server.SetAutoRespawnAfterDeath(false);
            NAPI.Server.SetAutoSpawnOnConnect(false);

        }

        /// <summary>
        /// A function to verify the connection to the database and to terminate the session if no connection is established.
        /// </summary>
        private void VerifyConnectionToDB()
        {
            using (DataContext db = new DataContext())
            {
                if (db.Database.CanConnect())
                {
                    NAPI.Util.ConsoleOutput("DATABASE CONNECTION ESTABLISHED...", ConsoleColor.Green);
                    return;
                }

                NAPI.Util.ConsoleOutput("DATABASE FAILED TO CONNECT", ConsoleColor.Red);

                Timer timeoutTimer = new Timer();
                timeoutTimer.Interval = 20000;
                timeoutTimer.Elapsed += TerminateSession;
                timeoutTimer.AutoReset = false;
                timeoutTimer.Start();
            }
        }
        
        private void TerminateSession(object sender, ElapsedEventArgs elapsed)
        {
            Environment.Exit(0);
        }
    }
}
