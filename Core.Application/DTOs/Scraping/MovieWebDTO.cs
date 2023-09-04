
namespace Core.Application.DTOs.Scraping
{
    public class MovieWebDTO
    {
        public string Name { get; set; }
        public string Overview { get; set; }
        public string Url { get; set; }
        public string Img { get; set; }
        public int ScrapPageID { get; set; }
        public int TMDBTempID { get; set; }
    }
}
