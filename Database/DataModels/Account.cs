using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Demo.Database.DataModels
{
    public class Account
    {
        [Key]
        public int ID { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public int AdminRank { get; set; }

        public string AdminName { get; set; }
    }
}
