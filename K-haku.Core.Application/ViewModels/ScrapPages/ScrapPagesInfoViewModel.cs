using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace K_haku.Core.Application.ViewModels.ScrapPages
{
    public class ScrapPagesInfoViewModel
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Info { get; set; }
        public string Img { get; set; }
        public string PageUrl { get; set; }
        public char isOn { get; set; }
        public DateTime LastScrap { get; set; }
    }
}
