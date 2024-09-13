using Core.Application.DTOs.Genres;
using Core.Application.DTOs.Scraping;

namespace Core.Application.DTOs.Movies
{
    public class InfoSearchMovieDto
    {
        public int ID { get; set; }
        public int TMDBID { get; set; }
        public string Title { get; set; }
        public bool? Adult { get; set; }
        public double? Vote_average { get; set; }
        public string? Overview { get; set; }
        public string? Poster_path { get; set; }
        public string? Backdrop_path { get; set; }
        public DateTime? Release_date { get; set; }

        public List<MovieWebDTO> Source { get; set; }
        public List<TmdbGenreResponseDto> Genres { get; set; }
    }
}
