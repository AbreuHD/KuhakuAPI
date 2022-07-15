using PuppeteerSharp;
using ScrapySharp.Html;
using ScrapySharp.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movie.GetAll.Cuevana
{
    public class CuevanaGetAllMovies
    {
        /// <summary>
        /// Function to get all Movies of a single movie page of cuevana3.me
        /// </summary>
        /// <returns>Return all Movies</returns>
        public List<string> MovieList()
        {
            List<string> movieLinks = new();
            ScrapingBrowser browser = new ScrapingBrowser();

            //set UseDefaultCookiesParser as false if a website returns invalid cookies format
            //browser.UseDefaultCookiesParser = false;

            WebPage webPage = browser.NavigateToPage(new Uri("https://ww1.cuevana3.me/peliculas"));

            var movieFather = webPage.Find("ul", By.Class("MovieList")).FirstOrDefault();
            var movieList = movieFather.SelectNodes("//li[@class='xxx TPostMv']");
            foreach (var movie in movieList)
            {
                var Tittle = movie.SelectSingleNode("./div/a/h2");
                var Links = movie.SelectSingleNode("./div/a/@href").Attributes["href"].Value;
                movieLinks.Add(Links);
            }
            return movieLinks;
        }
    }
}
