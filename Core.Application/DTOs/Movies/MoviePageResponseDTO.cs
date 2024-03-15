using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.DTOs.Movies
{
    public class MoviePageResponseDTO
    {
        public int Id { get; set; }
        public string WebPageTitle { get; set; }
        public string URI { get; set; }
    }
}
