using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace K_haku.Core.Application.Features.Cuevana.Commands.UpdateCuevanaMovie
{
    public class UpdateCuevanaMovieResponse
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Photo { get; set; }
        public string Link { get; set; }
        public string Age { get; set; }
        public string TMDB { get; set; }
        public bool Confirmed { get; set; }
    }
}
