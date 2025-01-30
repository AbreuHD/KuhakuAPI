namespace Core.Application.DTOs.Movies
{
    public class MoviePageResponseDto
    {
        public int Id { get; set; }
        public required string WebPageTitle { get; set; }
        public required string URI { get; set; }
    }
}
