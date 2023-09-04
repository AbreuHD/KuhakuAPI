using HtmlAgilityPack;
using System;

namespace WebScrapingTesting.GetContent.pelisplushd.lat
{
    public class GetPelisplushd
    {
        public ScrapPelisPlusHDLat(uri)
        {
			HtmlWeb web = new HtmlWeb();
			var htmlDoc = web.Load(html);

			var node = htmlDoc.DocumentNode.SelectSingleNode("//head/title");
            Console.WriteLine(node);
		}
    }
}
