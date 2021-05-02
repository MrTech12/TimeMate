using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace DataAccessLayer.Repositories
{
    public class SQLDatabaseRepository
    {
        public string GetConnectionString()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json",optional:true,reloadOnChange: true);
            return builder.Build().GetSection("ConnectionStrings").GetSection("FontysMS").Value;
        }
    }
}
