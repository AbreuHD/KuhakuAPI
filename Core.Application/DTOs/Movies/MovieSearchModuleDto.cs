using Core.Application.DTOs.Genres;

namespace Core.Application.DTOs.Movies
{
    public class MovieSearchModuleDto
    {
        public List<TmdbGenreResponseDto>? Genres { get; set; }
        public required List<PreviewSearchMovieDto> Movies { get; set; }
    }
}
