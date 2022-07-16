using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace K_haku.Core.Movie.GetVideos
{
    public class Fembed
    {
        public string GetSource(string url)
        {
            url = url.Replace("/v/", "/api/source/");

            var httpRequest = (HttpWebRequest)WebRequest.Create(url);
            httpRequest.Method = "POST";

            httpRequest.ContentType = "application/x-www-form-urlencoded";

            var data = "key1=value1&key2=value2";

            using (var streamWriter = new StreamWriter(httpRequest.GetRequestStream()))
            {
                streamWriter.Write(data);
            }

            var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                string result = streamReader.ReadToEnd();
                JObject json = JObject.Parse(result);

                var dataR = json.GetValue("data");
                return dataR[1].Value<string>("file");
            }

            
            return null;
        }
    }
}
