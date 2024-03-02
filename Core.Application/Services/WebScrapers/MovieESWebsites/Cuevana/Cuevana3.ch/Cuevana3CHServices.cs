using Core.Application.DTOs.Scraping;
using HtmlAgilityPack;
using ScrapySharp.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Services.WebScrapers.MovieESWebsites.Cuevana.Cuevana3.ch
{
    public class Cuevana3CHServices
    {
        private int DB_WEB_ID = 0;
        private string ORIGINAL_URI = "";

        public Cuevana3CHServices(int DB_WEB_ID, string ORIGINAL_URI)
        {
            this.ORIGINAL_URI = ORIGINAL_URI;
            this.DB_WEB_ID = DB_WEB_ID;
        }

        public async Task<int> GetCuevana3Pagination()
        {
            string uri = "/peliculas?page=1000";
            HtmlWeb web = new HtmlWeb();
            var htmlDoc = web.Load(ORIGINAL_URI + uri);
            return (int)Convert.ToInt64(htmlDoc.DocumentNode.SelectSingleNode("//*[@id=\"aa-wp\"]/div/div[2]/main/section/nav/div/a[7]").InnerText);
        }

        public async Task<List<MovieWebDTO>> GetCuevana3(int i)
        {
            string uri = "/peliculas";
            List<MovieWebDTO> movieList = new List<MovieWebDTO>();

            HtmlWeb web = new HtmlWeb();
            var htmlDoc = web.Load($"{ORIGINAL_URI + uri}?page={i}");

            var elements = htmlDoc.DocumentNode.CssSelect("#aa-wp > div > div.TpRwCont.cont > main > section > ul > li"); //SelectNodes("//li[@class='xxx TPostMv']")
            int count = 0;
            foreach (var node in elements)
            {
                count++;
                movieList.Add(GetMovieInfo(node)); //here
                Console.WriteLine($"Movie {count}");
            }
            return movieList;
        }

        private MovieWebDTO GetMovieInfo(HtmlNode node)
        {
            var data = new MovieWebDTO();

            data.Name = node.ChildNodes[1].ChildNodes[1].ChildNodes[3].InnerText; //ChildNodes[1].ChildNodes[1].SelectSingleNode("//h2").InnerText;
            data.Img = node.ChildNodes[1].ChildNodes[1].ChildNodes[1].ChildNodes[2].ChildNodes[1].Attributes["data-src"].Value;
            data.Url = node.ChildNodes[1].ChildNodes[1].Attributes["href"].Value;
            data.Overview = GetOverView(data.Url);
            data.ScrapPageID = DB_WEB_ID;

            return data;
        }

        private string GetOverView(string uri)
        {
            var node = "Error";

            try
            {
                HtmlWeb web = new HtmlWeb();
                var htmlDoc = web.Load(ORIGINAL_URI + uri);
                node = htmlDoc.DocumentNode.SelectSingleNode("//*[@class=\"Description\"]").InnerText;
            }
            catch
            {
                node = "Error";
            }
            return node;
        }
    }
}
