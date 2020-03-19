using Abot2.Core;
using Abot2.Crawler;
using Abot2.Poco;
using AngleSharp.Html.Dom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WindowsFormsApp1.Utils
{

    public class SiteParser
    {
        private PageRequester pageRequester = new PageRequester(new CrawlConfiguration(), new WebContentExtractor());
        private const string siteUrl = "https://belaruspartisan.by/lenta/";
        private const string regExHtml = "<[^<>]+>";

        public async Task<List<string>> GetLinks()
        {
            var crawledPage = await pageRequester.MakeRequestAsync(new Uri(siteUrl));
            var angleSharpHtmlDocument = crawledPage.AngleSharpHtmlDocument;
            var anchors = angleSharpHtmlDocument.QuerySelectorAll("a").OfType<IHtmlAnchorElement>();

            List<string> hrefs = anchors.Select(x => x.Href).Where(h => h.StartsWith("about")).ToList();

            return hrefs;
        }

        public async Task<List<string>> GetDomElements(string url)
        {
            var crawledPage = await pageRequester.MakeRequestAsync(new Uri(url));
            var angleSharpHtmlDocument = crawledPage.AngleSharpHtmlDocument;
            string title = angleSharpHtmlDocument.Title;
            List<string> elements = new List<string>();
            elements.Add(title);
            string pageContent = crawledPage.Content.Text;
            string html = Regex.Replace(pageContent, @"<[^>]*>", string.Empty);
            elements.Add(html);
            string text = Regex.Replace(pageContent, @">([a-z, ,0-9]*)<", string.Empty);
            elements.Add(text);

            return elements;
        }

    }
}
