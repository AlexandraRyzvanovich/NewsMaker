using Abot2.Crawler;
using Abot2.Poco;
using AngleSharp.Html.Dom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WindowsFormsApp1.Utils;

namespace WindowsFormsApp1.Service
{
    public class ArticleService
    {
        public async Task ExecuteAsync()
        {
            SiteParser siteParser = new SiteParser();
            
            List<string> urlList = await siteParser.getAllLinksAsync();
            
            
        }

    }
}
