using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApp1.Model;

namespace NewsMaker.ConnectionFactory
{
    public class DbConnectionFactory
    {
        public static string PostgresDb_10 = "Server=localhost;Port=5432;User Id=postgres;Password=123456;Database=postgres;Pooling=true;MinPoolSize=0;MaxPoolSize=200";

        public OrmLiteConnectionFactory createConnectionFactory()
        {
            var dbFactory = new OrmLiteConnectionFactory(PostgresDb_10, PostgreSqlDialect.Provider);
            return dbFactory;

        }
    }
}
