using Core.Application.DTOs.Scraping;
using HtmlAgilityPack;

namespace Core.Application.Services.WebScrapers.MovieESWebsites.PelisPluslat
{
    public class GetPelisPlusLatMovies(int DB_WEB_ID, string ORIGINAL_URI)
    {
        private readonly int DB_WEB_ID = DB_WEB_ID;
        private readonly string ORIGINAL_URI = ORIGINAL_URI;

        public int GetPelisplushdPagination()
        {
            string uri = "/peliculas/estrenos";
            HtmlWeb web = new();
            var htmlDoc = web.Load(ORIGINAL_URI + uri);
            return (int)Convert.ToInt64(htmlDoc.DocumentNode.SelectSingleNode("//*[@id=\"default-tab-1\"]/nav/ul/li[15]").InnerText);
        }

        public List<MovieWebDto> GetPelisplushd(int i)
        {
            string uri = "/peliculas/estrenos";
            List<MovieWebDto> movieList = [];

            HtmlWeb web = new();
            var htmlDoc = web.Load($"{ORIGINAL_URI + uri}?page={i}");

            var elements = htmlDoc.DocumentNode.SelectNodes("//*[@id='default-tab-1']/div/a");

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
                Name = node.ChildNodes[3].ChildNodes[1].InnerText,
                Url = node.Attributes["href"].Value,
                Img = node.ChildNodes[1].Attributes["src"].Value
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
                node = htmlDoc.DocumentNode.SelectSingleNode("/html/body/div[1]/div/div/section/div/div[1]/div[1]/div[2]/div/div[2]/div[1]").InnerText;
            }
            catch
            {
                node = "Error";
            }
            return node;
        }
    }
}
