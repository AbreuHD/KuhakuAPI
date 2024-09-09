using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.DTOs.Genres
{
    public class TmdbGenreApiResponseDto
    {
        public List<TmdbGenreResponseDto>? genres { get; set; }
    }
}
