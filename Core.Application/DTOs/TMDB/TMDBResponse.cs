using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.DTOs.TMDB
{
    public class TMDBResponse
    {
        public List<TMDBResult> results { get; set; }
    }
}
