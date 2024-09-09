using Core.Application.DTOs.Movies;

namespace Core.Application.DTOs.Home
{
    public class HomeDto
    {
        public List<PreviewSearchMovieDTO>? Accion { get; set; }
        public List<PreviewSearchMovieDTO>? Animacion { get; set; }
        public List<PreviewSearchMovieDTO>? Horror { get; set; }
        public List<PreviewSearchMovieDTO>? Romance { get; set; }
        public List<PreviewSearchMovieDTO>? Familiar { get; set; }
        public List<PreviewSearchMovieDTO>? History { get; set; }
    }
}
