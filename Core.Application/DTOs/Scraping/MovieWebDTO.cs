namespace Core.Application.DTOs.Scraping
{
    public class MovieWebDto
    {
        public required string Name { get; set; }
        public string? Overview { get; set; }
        public required string Url { get; set; }
        public string? Img { get; set; }
        public int ScrapPageID { get; set; }
        public int TMDBTempID { get; set; }
        public List<int>? Genres { get; set; }
    }
}
