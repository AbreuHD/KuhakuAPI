namespace Core.Application.DTOs.TMDB
{
    public class TmdbResult
    {
        public int ID { get; set; }
        public required string Title { get; set; }
        public string? OriginalTitle { get; set; }
        public List<int>? GenreIds { get; set; }
        public bool? Adult { get; set; }
        public double? VoteAverage { get; set; }
        public int? VoteCount { get; set; }
        public string? Overview { get; set; }
        public string? Video { get; set; }
        public string? PosterPath { get; set; }
        public string? BackdropPath { get; set; }
        public string? ReleaseDate { get; set; }
    }
}
