using NewsMaker.ConnectionFactory;
using ServiceStack.OrmLite;
using ServiceStack.Text;
using System.Collections.Generic;
using WindowsFormsApp1.Model;

namespace NewsMaker.AbstractDao
{
    public class Dao
    {
        private readonly DbConnectionFactory dbConnector = new DbConnectionFactory();
        public void Save(Article article)
        {
            var dbFactory = dbConnector.createConnectionFactory();

            using (var db = dbFactory.Open())
            {
                db.CreateTableIfNotExists<Article>();
                db.Insert(article);

            }
        }
        public List<Article> SelectAll()
        {
            var dbFactory = dbConnector.createConnectionFactory();

            using (var db = dbFactory.Open())
            {
                var articles = db.Select<Article>();
                articles.PrintDump();
                return articles;
            }
        }

        public List<Article> SelectById(int[] ids)
        {
            var dbFactory = dbConnector.createConnectionFactory();
            using (var db = dbFactory.Open())
            {
                var articlesByIds = db.SelectByIds<Article>(ids);
                return articlesByIds;
            }
        }
    }
}
