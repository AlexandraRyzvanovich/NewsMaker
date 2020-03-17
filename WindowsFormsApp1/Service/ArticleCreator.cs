using Abot2.Crawler;
using Abot2.Poco;
using AngleSharp.Html.Dom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.Service
{
    public class ArticleCreatorService
    {
        static List<string> hrefs = null;

        public static async Task Execute()
        {
            var config = new CrawlConfiguration
            {
                MaxPagesToCrawl = 10, //Only crawl 10 pages
                MinCrawlDelayPerDomainMilliSeconds = 3000 //Wait this many millisecs between requests
            };
            WebClient client = new WebClient
            {
                Credentials = CredentialCache.DefaultNetworkCredentials
            };
            String content = client.DownloadString("https://news.tut.by/");

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
