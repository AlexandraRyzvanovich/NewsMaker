using ServiceStack.OrmLite;
using ServiceStack.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApp1.Connection;
using WindowsFormsApp1.Model;

namespace WindowsFormsApp1.Dao
{
    public abstract class Dao
    {
        DbFactory connectionfactory = new DbFactory();
        public void save()
        {
            var dbFactory = connectionfactory.createConnection();
            using (var db = dbFactory.Open())
            {
                if (db.CreateTableIfNotExists<Article>())
                {
                    db.Insert(new Article { Id = 1, Name = "Seed Data" });
                }

                var result = db.SingleById<Article>(1);
                result.PrintDump(); //= {Id: 1, Name:Seed Data}
            }
        }
    }
}
