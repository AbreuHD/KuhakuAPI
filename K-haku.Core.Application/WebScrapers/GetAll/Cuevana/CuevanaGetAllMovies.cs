using HtmlAgilityPack;
using K_haku.Core.Application.ViewModels;
using PuppeteerSharp;
using ScrapySharp.Html;
using ScrapySharp.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace K_haku.Core.Application.WebsScrapers.GetAll.Cuevana
{
    //https://ww1.cuevana3.me/peliculas
    public class CuevanaGetAllMovies
    {
        /// <summary>
        /// Function to get all Movies in cuevana3.me
        /// </summary>
        /// <returns>Return MovieViewModel whit all Movies</returns>
        public async Task<List<MovieViewModel>> MovieList()
        {
            List<string> movieLinks = new();
            ScrapingBrowser browser = new ScrapingBrowser();

            //set UseDefaultCookiesParser as false if a website returns invalid cookies format
            //browser.UseDefaultCookiesParser = false;

            WebPage webPage = browser.NavigateToPage(new Uri("https://ww1.cuevana3.me/peliculas"));
            var webPageCount = webPage.Find("nav", By.Class("navigation")).FirstOrDefault();
            var movieWebPageCount = webPageCount.SelectSingleNode("//div/a[@class='page-link'][4]").InnerText;
            
            int i = 0;
            List<HtmlNodeCollection> movieList = new();
            while (i < Convert.ToInt16(movieWebPageCount)) //Convert.ToInt16(movieWebPageCount)
            {
                i++;
                Console.WriteLine($"Cuevana Page {i}");
                webPage = browser.NavigateToPage(new Uri("https://ww1.cuevana3.me/peliculas/page/" + i));
                var movieFather = webPage.Find("ul", By.Class("MovieList")).FirstOrDefault();
                movieList.Add(movieFather.SelectNodes("//li[@class='xxx TPostMv']"));
            }
            
            i = 0;
            List<MovieViewModel> vm = new();
            foreach (var moviePage in movieList)
            {
                i++;
                Console.WriteLine($"Movie number {i} get");
                vm.AddRange(moviePage.Select(movie => new MovieViewModel
                {
                    Title = ToUTF8(movie.SelectSingleNode("./div/a/h2").InnerText),
                    Photo = movie.SelectSingleNode("./div/a/div/figure/img/@src").Attributes["data-src"].Value,
                    Link = movie.SelectSingleNode("./div/a/@href").Attributes["href"].Value,
                    Age = movie.SelectSingleNode("./div/a/div/span").InnerText,
                }).ToList());
            }
            return vm;
        }
        
        public string ToUTF8(string Data)
        {
            byte[] bytes = Encoding.Default.GetBytes(Data);
            return Encoding.UTF8.GetString(bytes);
        }
    }
}
