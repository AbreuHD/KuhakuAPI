using Core.Application.DTOs.Genres;
using Core.Application.DTOs.Movies;

namespace Core.Application.DTOs.Home
{
    public class HomeDto
    {
        public TmdbGenreResponseDto Genre { get; set; }
        public List<PreviewSearchMovieDto>? Movies { get; set; }
        //public List<PreviewSearchMovieDto>? Animacion { get; set; }
        //public List<PreviewSearchMovieDto>? Horror { get; set; }
        //public List<PreviewSearchMovieDto>? Romance { get; set; }
        //public List<PreviewSearchMovieDto>? Familiar { get; set; }
        //public List<PreviewSearchMovieDto>? History { get; set; }
    }
}
