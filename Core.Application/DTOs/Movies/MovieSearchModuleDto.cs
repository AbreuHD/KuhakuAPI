using Core.Application.DTOs.Genres;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.DTOs.Movies
{
    public class MovieSearchModuleDto
    {
        public List<TmdbGenreResponseDto>? Genres { get; set; }
        public required List<PreviewSearchMovieDto> Movies { get; set; }
    }
}
