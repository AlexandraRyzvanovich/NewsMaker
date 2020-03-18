using Abot2.Crawler;
using Abot2.Poco;
using AngleSharp.Html.Dom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.Utils
{

    public class SiteParser
    {
        private const string siteUrl = "https://belaruspartisan.by/search/";

        private static List<string> hrefs = null;

        public async Task<List<string>> getAllLinksAsync()
        {
            await GetLinks();
            foreach (string href in hrefs)
            {
                if (!href.StartsWith("https://news.tut.by/"))
                {
                    hrefs.Remove(href);
                }
            }
            return hrefs;
        }
        private static async Task GetLinks()
        {
            var config = new CrawlConfiguration
            {
                MaxPagesToCrawl = 1000,
                MinCrawlDelayPerDomainMilliSeconds = 3000
            };

            var crawler = new PoliteWebCrawler(config);

            crawler.PageCrawlCompleted += PageCrawlCompleted;

            var crawlResult = await crawler.CrawlAsync(new Uri(siteUrl));
        }
        private static void PageCrawlCompleted(object sender, PageCrawlCompletedArgs e)
        {
            var crawledPage = e.CrawledPage;
            var crawlContext = e.CrawlContext;

            var httpStatus = crawledPage.HttpResponseMessage.StatusCode;
            var rawPageText = crawledPage.Content.Text;

            var document = crawledPage.AngleSharpHtmlDocument;
            var anchors = document.QuerySelectorAll("a").OfType<IHtmlAnchorElement>();
            List<string> hrefs;
            hrefs = anchors.Select(x => x.Href).ToList();
        }
    }
}
