using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace K_haku.Core.Application.WebScrapers.Common
{
    public class Streamtape
    {
        public async Task<string> GetVideoSource(string link)
        {
            var formatLink = link.Substring(link.IndexOf("<script>document.getElementById('ideoolink').innerHTML = "))
                .Replace("\"<script>document.getElementById('ideoolink').innerHTML = ", "");

            var first = formatLink.Substring(formatLink.IndexOf("/"));
            first = first.Substring(0, first.IndexOf("\""))
                ;
            var second = formatLink.Substring(formatLink.IndexOf("xcdb")+4);
            second = second.Substring(0, second.IndexOf("'"));

            return "https:/" + first + second;
        }
    }
}
