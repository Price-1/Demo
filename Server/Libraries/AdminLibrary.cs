using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Server.Libraries
{
    public class AdminLibrary
    {
        public class AdminRank
        {
            public static int RANK_DEFAULT = 0;
            public static int RANK_LEVEL1 = 1;
            public static int RANK_LEVEL2 = 2;
            public static int RANK_LEVEL3 = 3;
            public static int RANK_LEVEL4 = 4;
            public static int RANK_LEADADMIN = 5;
            public static int RANK_MANAGEMENT = 6;
        }

        public class AdminColors
        {
            public static string COLOR_SUPPORT = "!{#FF9D00}";
            public static string COLOR_ADMIN = "!{#23FF00}";
            public static string COLOR_LEADADMIN = "!{#FF0000}";
            public static string COLOR_MANAGEMENT = "!{#000EFF}";

        }
    }
}
