// See https://aka.ms/new-console-template for more information
using Core.Application.DTOs.Scraping;
using HtmlAgilityPack;
using System;
String ORIGINAL_URI = "https://www12.pelisplushd.lat";
int DB_WEB_ID = 1;

GetPelisplushd("https://www12.pelisplushd.lat/peliculas/estrenos");


List<MovieWebDTO> GetPelisplushd(String uri)
{
    List<MovieWebDTO> movieList = new List<MovieWebDTO>();

    HtmlWeb web = new HtmlWeb();
    var htmlDoc = web.Load(uri);

    var elements = htmlDoc.DocumentNode.SelectNodes("//*[@id='default-tab-1']/div/a");
    int pagination = (int)Convert.ToInt64(htmlDoc.DocumentNode.SelectSingleNode("//*[@id=\"default-tab-1\"]/nav/ul/li[15]").InnerText);
    int i = 1;

    while(i <= pagination){
        foreach (var node in elements)
        {
            movieList.Add(GetMovieInfo(node));
        }
        i++;
        htmlDoc = web.Load($"{uri}?page={i}");
        elements = htmlDoc.DocumentNode.SelectNodes("//*[@id='default-tab-1']/div//a");
    }
    return movieList;
}

MovieWebDTO GetMovieInfo(HtmlNode node)
{
    var data = new MovieWebDTO();

    data.Name = node.ChildNodes[3].ChildNodes[1].InnerText;
    data.Url = node.Attributes["href"].Value;
    data.Img = node.ChildNodes[1].Attributes["src"].Value;
    data.Overview = GetOverView(data.Url);
    data.ScrapPageID = DB_WEB_ID;

    return data;
}

string GetOverView(string uri)
{
    var node = "Error";

    try
    {
        HtmlWeb web = new HtmlWeb();
        var htmlDoc = web.Load(ORIGINAL_URI + uri);
        node = htmlDoc.DocumentNode.SelectSingleNode("/html/body/div[1]/div/div/section/div/div[1]/div[1]/div[2]/div/div[2]/div[1]").InnerText;
    }
    catch
    {
        node = "Error";
    }
    return node;
}


