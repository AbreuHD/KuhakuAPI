using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.DTOs.Relations
{
    public class Movie_MovieWebDTO
    {
        public int MovieID { get; set; }
        public int MovieWebID { get; set; }
        public bool Verified { get; set; }
    }
}
