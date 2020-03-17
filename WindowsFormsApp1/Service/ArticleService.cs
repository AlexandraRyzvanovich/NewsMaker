using Abot2.Crawler;
using Abot2.Poco;
using AngleSharp.Html.Dom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WindowsFormsApp1.Service
{
    public class ArticleService
    {
        public static List<string> hrefs = null;

        public void ProcessAllLinks()
        {
            foreach(string href in hrefs)
            {
                if (!href.StartsWith("https://news.tut.by/"))
                {
                    hrefs.Remove(href);
                }
            }
        }

        public static async Task GetLinks()
        {
            var config = new CrawlConfiguration
            {
                MaxPagesToCrawl = 1000, 
                MinCrawlDelayPerDomainMilliSeconds = 3000
            };
           
            var crawler = new PoliteWebCrawler(config);
          
            crawler.PageCrawlCompleted += PageCrawlCompleted;

            var crawlResult = await crawler.CrawlAsync(new Uri("https://news.tut.by/"));
        }

        private static void PageCrawlCompleted(object sender, PageCrawlCompletedArgs e)
        {
            var crawledPage = e.CrawledPage;
            var crawlContext = e.CrawlContext;
            
            var httpStatus = crawledPage.HttpResponseMessage.StatusCode;
            var rawPageText = crawledPage.Content.Text;

            var document = crawledPage.AngleSharpHtmlDocument;
            var anchors = document.QuerySelectorAll("a").OfType<IHtmlAnchorElement>();
            hrefs = anchors.Select(x => x.Href).ToList();
        }
    }
}
