using Core.Application.DTOs.Movies;

namespace Core.Application.DTOs.Home
{
    public class HomeDto
    {
        public List<PreviewSearchMovieDto>? Accion { get; set; }
        public List<PreviewSearchMovieDto>? Animacion { get; set; }
        public List<PreviewSearchMovieDto>? Horror { get; set; }
        public List<PreviewSearchMovieDto>? Romance { get; set; }
        public List<PreviewSearchMovieDto>? Familiar { get; set; }
        public List<PreviewSearchMovieDto>? History { get; set; }
    }
}
