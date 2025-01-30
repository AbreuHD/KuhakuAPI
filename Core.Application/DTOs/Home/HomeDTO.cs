using Core.Application.DTOs.Genres;
using Core.Application.DTOs.Movies;

namespace Core.Application.DTOs.Home
{
    public class HomeDto
    {
        public required TmdbGenreResponseDto Genre { get; set; }
        public List<PreviewSearchMovieDto>? Movies { get; set; }
    }
}
