using HtmlAgilityPack;
using K_haku.Core.Application.Dtos.Movie;
using K_haku.Core.Application.Helpers;
using K_haku.Core.Application.WebScrapers.Common;
using Newtonsoft.Json.Linq;
using ScrapySharp.Html;
using ScrapySharp.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace K_haku.Core.Application.WebScrapers.Cuevana.GetVideoSource
{
    public class CuevanaGetVideoSource
    {
        public async Task<string> GetSource(VideoMovieResponse response)
        {
            if (response.Type == null || response.Type == "" || response.Type == "Trailer")
            {

            }
            else
            {
                ScrapingBrowser browser = new ScrapingBrowser();
                WebPage webPage = browser.NavigateToPage(new Uri($"https:{response.Link}"));
                SendFormCuevana formCN = new();
                HtmlDocument doc = new HtmlDocument();

                var vid = webPage.Html;
                if (response.Link.Contains("fembed"))
                {
                    var plater = (vid.SelectSingleNode("//div[@id='player1']").Attributes["onclick"].Value).Replace("location.href='", "").Replace("';", "");
                    var fembed = await browser.NavigateToPageAsync(new Uri($"https://api.cuevana3.me/fembed/{plater}"));
                    var getFunction = fembed.Html;
                    var getF = getFunction.SelectSingleNode("//html/body/script").InnerText;
                    var codeAPI = getF.Substring(getF.IndexOf(":"), getF.IndexOf("}") - (getF.IndexOf(":"))).Replace("\"", "").Replace(":", "").Trim();
                    string sendFembed = formCN.Send("https://api.cuevana3.me/fembed/api.php", codeAPI, "h");
                    JObject json = JObject.Parse(sendFembed);
                    Fembed fb = new();
                    var returnData = fb.GetSource(json.Value<string>("url"));
                    return returnData;
                }

                var link = vid.SelectSingleNode("//a/@href").Attributes["href"].Value;
                var videoStartPage = await browser.NavigateToPageAsync(new Uri($"https://apialfa.tomatomatela.com/ir/{link}"));
                var form = videoStartPage.Find("form", By.Id("FbAns")).FirstOrDefault();

                int i = 0;
                string dataFirst = "";
                while (i != 1)
                {
                    var action = form.SelectSingleNode("//form").Attributes["action"].Value;
                    var formData = form.SelectSingleNode("//form/input").Attributes["value"].Value;
                    var nameData = form.SelectSingleNode("//form/input").Attributes["name"].Value;
                    try
                    {
                        dataFirst = formCN.Send($"https://apialfa.tomatomatela.com/ir/{action}", $"{formData}&=", nameData);
                    }
                    catch(Exception ex)
                    {
                        dataFirst = formCN.Send($"https://api.cuevana3.me/sc/{action}", $"{formData}&=", nameData);
                    }
                    if (dataFirst.Contains("Streamtape.com"))
                    {
                        Streamtape tape = new();
                        return await tape.GetVideoSource(dataFirst);
                    }
                    if (!(dataFirst.ToString()).Contains("!DOCTYPE"))
                    {
                        i++;
                    }
                    doc.LoadHtml(dataFirst);
                    form = doc.DocumentNode;
                }
                string doneGet = webPage.Browser.NavigateToPage(new Uri($"https://tomatomatela.com/details.php?v={dataFirst.Substring(1)}"));
                JObject jsonR = JObject.Parse(doneGet);
                return jsonR.Value<string>("file");
            }

            return $"VIDEO NOT SUPPORTED: {response.Link}";
        }
    }
}
