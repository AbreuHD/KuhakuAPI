using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.DTOs.Genres
{
    public class TmdbGenreResponseListDto
    {
        public List<TmdbGenreResponseDto>? Movies { get; set; }
        public List<TmdbGenreResponseDto>? Series { get; set; }
    }
}
