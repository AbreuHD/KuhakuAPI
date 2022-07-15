using K_haku.Core.Application.ViewModels.Cuevana;
using PuppeteerSharp;
using ScrapySharp.Html;
using ScrapySharp.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace K_haku.Core.Movie.GetVideos.Cuevana
{
    //https://ww1.cuevana3.me/peliculas
    public class CuevanaGetAllVideos
    {
        /// <summary>
        /// To get All links of movies and languages
        /// </summary>
        /// <param name="movieLinks"></param>
        /// <returns>Languages and Movie Links</returns>

        public List<CuevanaVideoViewModel> MovieVideos(string movieLink)
        {
            ScrapingBrowser browser = new ScrapingBrowser();
            List<string> language = new();
            List<int> languageInt = new();
            List<string> movieLinks = new();

            WebPage webPage = browser.NavigateToPage(new Uri(movieLink));
            var moviePage = webPage.Find("div", By.Class("video")).FirstOrDefault();
            var languageList = moviePage.SelectNodes("//li[@class='open_submenu']/div[2]/ul/li");

            List<CuevanaVideoViewModel> vm = languageList.Select(iLanguage => new CuevanaVideoViewModel
            {
                Language = iLanguage.SelectSingleNode("./span/span").InnerText.Substring(3),
                Link = iLanguage.SelectSingleNode($"//div[@id='{iLanguage.SelectSingleNode(".").Attributes["data-tplayernv"].Value}']/iframe").Attributes["data-src"].Value,
                Type = iLanguage.SelectSingleNode("./span/div").InnerText,
            }).ToList();

            CuevanaVideoViewModel trailer = new();
            trailer.Language= "Unknow";
            trailer.Link = moviePage.SelectSingleNode($"//div[@id='OptY']/iframe").Attributes["data-src"].Value;
            trailer.Type = "Trailer";
            vm.Add(trailer);

            return vm;
            ///Get Languages
            /*var languageList = moviePage.SelectNodes("//div[@class='_3CT5n_0 L6v6v_0']");
            foreach (var iLanguage in languageList)
            {
                language.Add(iLanguage.InnerText);
            }

            ///Get LanguagesInt
            var languageIntList = moviePage.SelectNodes("//li[@class='open_submenu']/div[2]/ul");
            foreach (var iLangueInt in languageIntList)
            {
                languageInt.Add(iLangueInt.ChildNodes.Count());
            }

            ///Get Movie Links
            var movieList = moviePage.SelectNodes("//div[@class='TPlayerTb']");
            foreach (var iMovies in movieList)
            {
                movieLinks.Add(iMovies.SelectSingleNode("./iframe").Attributes["data-src"].Value);
            }

            string[,] movieVideos = new string[movieLinks.Count, 2];

            int indexLanguage = 0;
            int o = 0;
            for (int i = 0; i < movieLinks.Count; i++)
            {
                if (o >= languageInt[indexLanguage])
                {
                    o = 0;
                    indexLanguage += 1;
                }
                movieVideos[i, 0] = language[indexLanguage];
                movieVideos[i, 1] = movieLinks[i];
                o++;
            }
            return movieVideos;*/
        }

        public async Task<List<string>> getSource(List<string> uri)
        {
            List<string> urls = new();
            foreach (var item in uri)
            {
                try
                {
                    ScrapingBrowser browser = new ScrapingBrowser();
                    WebPage webPage = browser.NavigateToPage(new Uri(item));
                    var vid = webPage.Html;
                    var link = vid.SelectSingleNode("//a/@href").Attributes["href"].Value;
                    //var bro = browser.NavigateToPage(new Uri($"https://apialfa.tomatomatela.com/ir/{link}"));

                    using var browserFetcher = new BrowserFetcher();
                    await browserFetcher.DownloadAsync();
                    await using var browserHead = await Puppeteer.LaunchAsync(
                        new LaunchOptions { Headless = true });

                    await using var page = await browserHead.NewPageAsync();
                    await page.GoToAsync($"https://apialfa.tomatomatela.com/ir/{link}");
                    await page.WaitForNavigationAsync();
                    await page.WaitForNavigationAsync();
                    urls.Add(page.Url);
                }
                catch
                {

                }
            }
            return urls;
        }
    }
}
