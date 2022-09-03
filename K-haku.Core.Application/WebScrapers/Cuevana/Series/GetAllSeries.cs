using K_haku.Core.Domain.Entities;
using ScrapySharp.Html;
using ScrapySharp.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace K_haku.Core.Application.WebScrapers.Cuevana.Series
{
    public class GetAllSeries
    {
        public GetAllSeries(string movieLink)
        {
            List<string> movieLinks = new();
            ScrapingBrowser browser = new ScrapingBrowser();

            //set UseDefaultCookiesParser as false if a website returns invalid cookies format
            //browser.UseDefaultCookiesParser = false;

            WebPage webPage = browser.NavigateToPage(new Uri("https://ww1.cuevana3.me/serie"));
            var webPageCount = webPage.Find("div", By.Id("tabserie-1")).FirstOrDefault();
            var movieWebPageCount = webPageCount.SelectSingleNode("//div/a[@class='page-link'][4]").InnerText;


        }
    }
}
