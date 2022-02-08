using Demo.Database.DataModels;
using Demo.Metadata;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Text;
using System.Collections.Generic;

namespace Demo.Database
{
    public class DataContext : DbContext
    {
        public DataContext() { }

        public DbSet<Account> Accounts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            JObject connectionStringObject = CfgReader.RetrieveMetadataObject("DataContext");
            string connectionString = String.Format("server={0};database={1};user={2};password={3}", connectionStringObject.GetValue("server"), connectionStringObject.GetValue("database"), connectionStringObject.GetValue("user"), connectionStringObject.GetValue("password"));
            optionsBuilder.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 21)));
        }
    }
}
