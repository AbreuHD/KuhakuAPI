using K_haku.Core.Application.ViewModels.Cuevana;
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
using System.Net.NetworkInformation;
using AutoMapper.Internal;

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

            //WebPage webPage = await browser.NavigateToPageAsync(new Uri(movieLink));
            HtmlWeb web = new();
            HtmlDocument webPage = web.Load(movieLink);

            /*HtmlNode moviePage = webPage.Find("div", By.Id("top-single")).FirstOrDefault();
            HtmlNodeCollection movieData = moviePage.SelectNodes("//div[2]/ul/li/div/ul/li");*/
            var movieData = webPage.DocumentNode.SelectNodes("//div[2]/ul/li/div/ul/li");


            List<MovieVideoResponse> response = movieData.Select(data => new MovieVideoResponse
            {
                Language = ServerLang(data.Attributes["data-lang"].Value).Result,
                Link = ServerLink(webPage, data.Attributes["data-tplayernv"].Value).Result,
                Type = ServerType(data.Attributes["data-server"].Value).Result,
            }).ToList();


            MovieVideoResponse trailer = new();
            trailer.Language = "Unknow";
            trailer.Link = webPage.DocumentNode.SelectSingleNode($"//div[@id='OptY']/iframe").Attributes["data-src"].Value;
            trailer.Type = "Trailer";
            response.Add(trailer);

            return response;

        }

        public async Task<string> ServerLink(HtmlDocument page, string opt)
        {
            return page.DocumentNode.SelectSingleNode($"//div[@id='{opt}']/iframe").Attributes["data-src"].Value;
        }
        public async Task<string> ServerType(string datos)
        {
            int data = Convert.ToInt32(datos);
            if (data == 55625)
            {
                return "Google";
            }
            if (data == 54846)
            {
                return "Streamtape";
            }
            if (data == 28190)
            {
                return "Fembed";
            }

            return "No Identificado";
        }
        public async Task<string> ServerLang(string dato)
        {
            int data = Convert.ToInt32(dato);
            if (data == 48)
            {
                return "Latino";
            }
            if (data == 210)
            {
                return "Castellano";
            }
            if (data == 51)
            {
                return "Subtitulado";
            }

            return "No Identificado";
        }

    }
}