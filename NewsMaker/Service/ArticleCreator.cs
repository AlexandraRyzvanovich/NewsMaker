using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsMaker.Service
{
    public class ArticleCreatorService : IArticleService
    {
        public void Execute()
        {
            var config = new CrawlConfiguration
            {
                MaxPagesToCrawl = 10, 
                MinCrawlDelayPerDomainMilliSeconds = 3000 
            };
            var crawler = new PoliteWebCrawler(config);

            crawler.PageCrawlCompleted += PageCrawlCompleted;

            var crawlResult = await crawler.CrawlAsync(new Uri("https://news.tut.by/"));
           
        }
        private static void PageCrawlCompleted(object sender, PageCrawlCompletedArgs e)
        {
            var httpStatus = e.CrawledPage.HttpResponseMessage.StatusCode;
            var rawPageText = e.CrawledPage.Content.Text;
        }
    }
}
