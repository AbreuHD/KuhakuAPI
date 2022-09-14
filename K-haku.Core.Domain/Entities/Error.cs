using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace K_haku.Core.Domain.Entities
{
    public class Error
    {
        public int ID { get; set; }
        public string className { get; set; }
        public string ErrorType { get; set; }
        public string Info { get; set; }
    }
}
