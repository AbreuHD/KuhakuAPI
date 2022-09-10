using K_haku.Core.Application.ViewModels.Cuevana;
using PuppeteerSharp;
using ScrapySharp.Html;
using ScrapySharp.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using ScrapySharp.Extensions;
using ScrapySharp.Html.Forms;
using K_haku.Core.Application.Helpers;
using Newtonsoft.Json.Linq;
using K_haku.Core.Application.WebScrapers.Common;
using K_haku.Core.Application.Dtos.Movie;
using MovieVideoResponse = K_haku.Core.Application.Dtos.Movie.MovieVideoResponse;

namespace K_haku.Core.Application.WebsScrapers.GetVideos.Cuevana
{
    public class CuevanaGetAllVideos
    {
        /// <summary>
        /// To get All links of movies and languages
        /// </summary>
        /// <param name="movieLinks"></param>
        /// <returns>Languages and Movie Links https://ww1.cuevana3.me/peliculas</returns>

        public async Task<List<MovieVideoResponse>> MovieVideos(string movieLink)
        {
            ScrapingBrowser browser = new ScrapingBrowser();
            List<string> language = new();
            List<int> languageInt = new();
            List<string> movieLinks = new();

            WebPage webPage = browser.NavigateToPage(new Uri(movieLink));
            var moviePage = webPage.Find("div", By.Class("video")).FirstOrDefault();
            var languageList = moviePage.SelectNodes("//li[@class='open_submenu']/div[2]/ul/li");

            List<MovieVideoResponse> response = languageList.Select(iLanguage => new MovieVideoResponse
            {
                Language = iLanguage.SelectSingleNode("./span/span").InnerText.Substring(3),
                Link = iLanguage.SelectSingleNode($"//div[@id='{iLanguage.SelectSingleNode(".").Attributes["data-tplayernv"].Value}']/iframe").Attributes["data-src"].Value,
                Type = iLanguage.SelectSingleNode("./span/div").InnerText,
            }).ToList();

            MovieVideoResponse trailer = new();
            trailer.Language = "Unknow";
            trailer.Link = moviePage.SelectSingleNode($"//div[@id='OptY']/iframe").Attributes["data-src"].Value;
            trailer.Type = "Trailer";
            response.Add(trailer);

            return response;

        }
    }
}
