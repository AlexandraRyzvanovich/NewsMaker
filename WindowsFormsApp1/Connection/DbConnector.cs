using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApp1.Model;

namespace WindowsFormsApp1.Connection
{
    public class DbFactory
    {
        private const string server = "server";
        private const string port = "5432";
        private const string database = "news";
        private string username = "postgres";
        private string password = "123456";
        const string searchPath1 = "schema1,schema3";


        public OrmLiteConnectionFactory createConnection()
        {
            var connString1 = $"Server ={ server}; Port ={port}; Search Path = { searchPath1 }; Database ={ database}; UserId ={ username}; Password ={ password};";
            var db = new OrmLiteConnectionFactory(connString1, PostgreSqlDialect.Provider);
            return db;

        }

    }
}
