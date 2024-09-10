using Core.Application.DTOs.Scraping;
using Core.Domain.Entities.GeneralMovie;

namespace Core.Application.DTOs.TMDB
{
    public class CheckedList
    {
        public List<Movie> Movies { get; set; }
        public List<MovieWebDTO> MovieWebDTO { get; set; }
    }
}
