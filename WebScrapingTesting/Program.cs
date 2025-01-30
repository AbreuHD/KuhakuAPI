// See https://aka.ms/new-console-template for more information
using Core.Application.DTOs.Scraping;
using HtmlAgilityPack;

int DB_WEB_ID = 0;
string ORIGINAL_URI = "https://cuevana3.ch";

GetCuevana3Pagination();


int GetCuevana3Pagination()
{
    string uri = "/peliculas?page=1000";
    HtmlWeb web = new HtmlWeb();
    var htmlDoc = web.Load(ORIGINAL_URI + uri);
    return (int)Convert.ToInt64(htmlDoc.DocumentNode.SelectSingleNode("//*[@id=\"aa-wp\"]/div/div[2]/main/section/nav/div/a[7]").InnerText);
}

List<MovieWebDto> GetCuevana3(int i)
{
    string uri = "/peliculas";
    List<MovieWebDto> movieList = new List<MovieWebDto>();

    HtmlWeb web = new HtmlWeb();
    var htmlDoc = web.Load($"{ORIGINAL_URI + uri}?page={i}");

    var elements = htmlDoc.DocumentNode.SelectNodes("//li[@class='xxx TPostMv']");
    int count = 0;
    foreach (var node in elements)
    {
        count++;
        movieList.Add(GetMovieInfo(node)); //here
        Console.WriteLine($"Movie {count}");
    }
    return movieList;
}

MovieWebDto GetMovieInfo(HtmlNode node)
{
    var data = new MovieWebDto
    {
        Name = node.ChildNodes[1].ChildNodes[2].ChildNodes[1].InnerText,
        Url = node.ChildNodes[1].ChildNodes[1].Attributes["href"].Value,
        Img = node.ChildNodes[1].ChildNodes[1].ChildNodes[2].ChildNodes[1].Attributes["src"].Value
    };
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
        node = htmlDoc.DocumentNode.SelectSingleNode("//*[@class=\"Description\"]").InnerText;
    }
    catch
    {
        node = "Error";
    }
    return node;
}


