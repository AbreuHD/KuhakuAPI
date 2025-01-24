using Core.Application.DTOs.Scraping;
using Core.Domain.Entities.Movie;

namespace Core.Application.DTOs.TMDB
{
    public class CheckedList
    {
        public required List<Movie> Movies { get; set; }
        public required List<MovieWebDto> MovieWebDto { get; set; }
    }
}
