using Core.Application.DTOs.Scraping;
using HtmlAgilityPack;

namespace Core.Application.Services.WebScrapers.MovieESWebsites.PelisPluslat
{
    public class GetPelisPlusLatMovies
    {
        private int DB_WEB_ID = 0;
        private string ORIGINAL_URI = "";

        public GetPelisPlusLatMovies(int DB_WEB_ID, string ORIGINAL_URI)
        {
            this.ORIGINAL_URI = ORIGINAL_URI;
            this.DB_WEB_ID = DB_WEB_ID;
        }

        public async Task<int> GetPelisplushdPagination()
        {
            string uri = "/peliculas/estrenos";
            HtmlWeb web = new HtmlWeb();
            var htmlDoc = web.Load(ORIGINAL_URI + uri);
            return (int)Convert.ToInt64(htmlDoc.DocumentNode.SelectSingleNode("//*[@id=\"default-tab-1\"]/nav/ul/li[15]").InnerText);
        }

        public async Task<List<MovieWebDTO>> GetPelisplushd(int i)
        {
            string uri = "/peliculas/estrenos";
            List<MovieWebDTO> movieList = new List<MovieWebDTO>();

            HtmlWeb web = new HtmlWeb();
            var htmlDoc = web.Load($"{ORIGINAL_URI + uri}?page={i}");
            //var htmlDoc = web.Load(ORIGINAL_URI + uri);

            var elements = htmlDoc.DocumentNode.SelectNodes("//*[@id='default-tab-1']/div/a");
            //int pagination = (int)Convert.ToInt64(htmlDoc.DocumentNode.SelectSingleNode("//*[@id=\"default-tab-1\"]/nav/ul/li[15]").InnerText);
            //int i = 1;

            //while (i <= pagination)
            //{}
            int count = 0;
            foreach (var node in elements)
            {
                count++;
                movieList.Add(GetMovieInfo(node)); //here
                Console.WriteLine($"Movie {count}");
            }
            //htmlDoc = web.Load($"{ORIGINAL_URI + uri}?page={i}");
            //elements = htmlDoc.DocumentNode.SelectNodes("//*[@id='default-tab-1']/div//a");
            return movieList;
        }

        private MovieWebDTO GetMovieInfo(HtmlNode node)
        {
            var data = new MovieWebDTO();

            data.Name = node.ChildNodes[3].ChildNodes[1].InnerText;
            data.Url = node.Attributes["href"].Value;
            data.Img = node.ChildNodes[1].Attributes["src"].Value;
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
