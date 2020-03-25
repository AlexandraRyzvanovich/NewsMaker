using Abot2.Crawler;
using Abot2.Poco;
using NewsMaker.AbstractDao;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using WindowsFormsApp1.Model;
using WindowsFormsApp1.Utils;
using System.Collections.Generic;
using WindowsFormsApp1.Dao;

namespace WindowsFormsApp1.Service
{
    public class ArticleService
    {
        private const string siteUrl = "https://belaruspartisan.by/lenta/";
        private ArticleDao articleDao = new ArticleDao();
        private EntitiesDao entitiesDao = new EntitiesDao();

        public async Task DownloadAllArticles()
        {
            var config = new CrawlConfiguration
            {
                MaxPagesToCrawl = 300,
                MinCrawlDelayPerDomainMilliSeconds = 300
            };
            var crawler = new PoliteWebCrawler(config);

            crawler.PageCrawlCompleted += PageCrawlCompleted;
            crawler.PageCrawlCompleted += Crawler_ProcessPageCrawlCompleted;

            var crawlResult = await crawler.CrawlAsync(new Uri(siteUrl));
        }

        public int FindWordInText(string textToFind, int atricleId)
        {
            Article article = articleDao.SelectById(atricleId);
            string text = article.Text;
            TextFinder finder = new TextFinder();
            int occurancesInText = finder.FindTextOccurances(textToFind, text);

            return occurancesInText;
        }

        public void Create()
        {
            PullentiEntitiesCreator pullentiEntitiesCreator = new PullentiEntitiesCreator();
            List<Article> articles = articleDao.SelectAll();
            List<Entity> entities = entitiesDao.SelectAll();
            List<Entity> newEntities = pullentiEntitiesCreator.CreateEntities(entities, articles);
            List<Entity> entitiesToUpdate = null;
            foreach (var item in newEntities)
            {
                var oldEntity = entities.Find(c => c.Value.Equals(item.Value));
                if (oldEntity != null)
                {
                    oldEntity.Properties = item.Properties;
                    entitiesToUpdate.Add(oldEntity);
                }
            }
            foreach (var item in entitiesToUpdate)
            {
                var entityToRemove = newEntities.Find(m => m.Value.Equals(item.Value));
                newEntities.Remove(entityToRemove);
            }
            entitiesDao.SaveAll(newEntities);
            entitiesDao.UpdateAll(entitiesToUpdate);
        }

        private async void Crawler_ProcessPageCrawlCompleted(object sender, PageCrawlCompletedArgs e)
        {
            CrawledPage crawledPage = e.CrawledPage;

            if (crawledPage.HttpRequestException != null || crawledPage.HttpResponseMessage.StatusCode != HttpStatusCode.OK)
            {
                Console.WriteLine($"Crawl of page failed {crawledPage.Uri.AbsoluteUri}");
            }
            else
                Console.WriteLine($"Crawl of page succeeded {crawledPage.Uri.AbsoluteUri}");

            if (string.IsNullOrEmpty(crawledPage.Content.Text))
                Console.WriteLine($"Page had no content {crawledPage.Uri.AbsoluteUri}");

            var angleSharpHtmlDocument = crawledPage.AngleSharpHtmlDocument;
            var url = crawledPage.Uri.AbsoluteUri;
            var allArticle = angleSharpHtmlDocument.GetElementsByClassName("pw article");
            if (allArticle.Length > 0)
            {
                string text = null;
                string html = null;
                string title = null;
                foreach (var f in allArticle)
                {
                    title += f.GetElementsByTagName("h1").First().TextContent;
                    text += f.TextContent;
                    html += f.InnerHtml;
                }

                var newsDate = angleSharpHtmlDocument.GetElementsByClassName("news-date-time news_date");

                DateTime date = new DateTime();

                foreach (var t in newsDate)
                {
                    date = DateTime.Parse(t.TextContent);
                }
                Article article = new Article(title, url, date, html, text);

                articleDao.Save(article);

            }
        }

        private static void PageCrawlCompleted(object sender, PageCrawlCompletedArgs e)
        {
            var httpStatus = e.CrawledPage.HttpResponseMessage?.StatusCode;
            var rawPageText = e.CrawledPage.Content.Text;
        }
    }

}
