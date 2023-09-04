using Core.Application.DTOs.Scraping;
using Core.Domain.Entities.GeneralMovie;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.DTOs.TMDB
{
    public class CheckedList
    {
        public List<Movie> Movies { get; set; }
        public List<MovieWebDTO> MovieWebDTO { get; set; }
    }
}
