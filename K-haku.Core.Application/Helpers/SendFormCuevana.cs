using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace K_haku.Core.Application.Helpers
{
    public class SendFormCuevana
    {
        public string Send(string _url, string _code, string var)
        {
            var httpRequest = (HttpWebRequest)WebRequest.Create(_url);
            httpRequest.Method = "POST";

            httpRequest.ContentType = "application/x-www-form-urlencoded";

            var data = $"{var}={_code}";

            using (var streamWriter = new StreamWriter(httpRequest.GetRequestStream()))
            {
                streamWriter.Write(data);
            }

            var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
            var result = "";
            //var url = new StreamReader(httpResponse.ResponseUri.ToString());
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                result = streamReader.ReadToEnd();
            }
            Console.WriteLine(httpResponse.StatusCode);
            if ((httpResponse.ResponseUri.ToString()).Contains("#"))
            {
                return (httpResponse.ResponseUri.ToString()).Substring((httpResponse.ResponseUri.ToString()).IndexOf("#"));
            }
            return result;
        }
    }
}
