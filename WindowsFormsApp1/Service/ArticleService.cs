using Abot2.Crawler;
using Abot2.Poco;
using AngleSharp;
using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WindowsFormsApp1.Utils;

namespace WindowsFormsApp1.Service
{
    public class ArticleService
    {
            private const string siteUrl = "https://belaruspartisan.by/lenta/";

        public async Task GetLinks()
        {
            var config = new CrawlConfiguration
            {
                MaxPagesToCrawl = 300, //Only crawl 10 pages
                MinCrawlDelayPerDomainMilliSeconds = 300 //Wait this many millisecs between requests
            };
            var crawler = new PoliteWebCrawler(config);

            crawler.PageCrawlCompleted += PageCrawlCompleted;//Several events available...
            crawler.PageCrawlCompleted += crawler_ProcessPageCrawlCompleted;

            var crawlResult = await crawler.CrawlAsync(new Uri(siteUrl));
        }

        async void crawler_ProcessPageCrawlCompleted(object sender, PageCrawlCompletedArgs e)
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

            var a = angleSharpHtmlDocument.QuerySelectorAll("div").OfType<IHtmlAnchorElement>().Select(m => m.ClassName.Equals("pw article")).ToList();
            //.Where(m => m.ClassName.Equals("pw article"))
            if (a.Count > 0)
            {
                Console.WriteLine("");
            }
        }

        private static void PageCrawlCompleted(object sender, PageCrawlCompletedArgs e)
        {
            var httpStatus = e.CrawledPage.HttpResponseMessage?.StatusCode;
            var rawPageText = e.CrawledPage.Content.Text;
        }
    }

}
