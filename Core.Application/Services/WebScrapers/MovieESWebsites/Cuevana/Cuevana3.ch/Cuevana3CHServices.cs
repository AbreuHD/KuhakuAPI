using Core.Application.DTOs.Scraping;
using HtmlAgilityPack;
using ScrapySharp.Extensions;

namespace Core.Application.Services.WebScrapers.MovieESWebsites.Cuevana.Cuevana3.ch
{
    public class Cuevana3CHServices(int DB_WEB_ID, string ORIGINAL_URI)
    {
        private readonly int DB_WEB_ID = DB_WEB_ID;
        private readonly string ORIGINAL_URI = ORIGINAL_URI;

        public int GetCuevana3Pagination()
        {
            string uri = "/peliculas?page=1000";
            HtmlWeb web = new();
            var htmlDoc = web.Load(ORIGINAL_URI + uri);
            return (int)Convert.ToInt64(htmlDoc.DocumentNode.SelectSingleNode("//*[@id=\"aa-wp\"]/div/div[2]/main/section/nav/div/a[7]").InnerText);
        }

        public List<MovieWebDto> GetCuevana3(int i)
        {
            string uri = "/peliculas";
            List<MovieWebDto> movieList = [];

            HtmlWeb web = new();
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

        private MovieWebDto GetMovieInfo(HtmlNode node)
        {
            var data = new MovieWebDto
            {
                Name = node.ChildNodes[1].ChildNodes[1].ChildNodes[3].InnerText, //ChildNodes[1].ChildNodes[1].SelectSingleNode("//h2").InnerText;
                Img = node.ChildNodes[1].ChildNodes[1].ChildNodes[1].ChildNodes[2].ChildNodes[1].Attributes["data-src"].Value,
                Url = node.ChildNodes[1].ChildNodes[1].Attributes["href"].Value
            };
            data.Overview = GetOverView(data.Url);
            data.ScrapPageID = DB_WEB_ID;

            return data;
        }

        private string GetOverView(string uri)
        {
            string? node;

            try
            {
                HtmlWeb web = new();
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
