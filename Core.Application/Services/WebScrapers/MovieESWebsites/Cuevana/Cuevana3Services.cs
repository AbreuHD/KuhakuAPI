using Core.Application.DTOs.Scraping;
using HtmlAgilityPack;
using System.Net;

namespace Core.Application.Services.WebScrapers.MovieESWebsites.Cuevana
{
    public class Cuevana3Services(int DB_WEB_ID, string ORIGINAL_URI)
    {
        private readonly int DB_WEB_ID = DB_WEB_ID;
        private readonly string ORIGINAL_URI = ORIGINAL_URI;

        /// <summary>
        /// Gets or sets the URI used to retrieve the first page of the movie pagination.
        /// </summary>
        public virtual string PagePaginationUri { get; set; } = "/peliculas?page=1";

        /// <summary>
        /// Gets or sets the base URI to retrieve movie pages with different page numbers.
        /// </summary>
        public virtual string PageNumberUri { get; set; } = "/peliculas/page/";

        /// <summary>
        /// Gets or sets the URI to be replaced for the movie image source.
        /// </summary>
        public virtual string ReplaceMovieUri { get; set; } = "/_next/image?url=";

        /// <summary>
        /// Gets or sets the XPath to the node containing the pagination information.
        /// </summary>
        public virtual string PagePaginationNode { get; set; } = "//*[@id=\"aa-wp\"]/div/div/div/main/section/nav/div/span[3]";

        /// <summary>
        /// Gets or sets the XPath to the node containing the list of movies on a page.
        /// </summary>
        public virtual string GetMoviePageList { get; set; } = "//*[@id=\"aa-wp\"]/div/div/div/main/section/div[2]/ul/li";

        /// <summary>
        /// Gets or sets the XPath to the node containing the movie name.
        /// </summary>
        public virtual string GetMovieName { get; set; } = "./div/div/div[@class=\"Title\"]";

        /// <summary>
        /// Gets or sets the XPath to the node containing the movie image.
        /// </summary>
        public virtual string GetMovieImage { get; set; } = "./div/a/div[@class=\"Image\"]/img";

        /// <summary>
        /// Gets or sets the XPath to the node containing the movie URL.
        /// </summary>
        public virtual string GetMovieUrl { get; set; } = "./div/a";

        /// <summary>
        /// Gets or sets the XPath to the node containing the movie description.
        /// </summary>
        public virtual string GetMovieDescription { get; set; } = "//*[@class=\"Description\"]";

        /// <summary>
        /// Retrieves the total number of pages for movie pagination by scraping the pagination node.
        /// </summary>
        /// <returns>The total number of pages for the movies.</returns>
        public virtual int GetPagination()
        {
            HtmlWeb web = new();
            var htmlDoc = web.Load(ORIGINAL_URI + PagePaginationUri);
            return (int)Convert.ToInt64(htmlDoc.DocumentNode.SelectSingleNode(PagePaginationNode).InnerText);
        }

        /// <summary>
        /// Retrieves a list of movies from the specified page by scraping the movie page list.
        /// </summary>
        /// <param name="page">The page number to retrieve movies from.</param>
        /// <returns>A list of <see cref="MovieWebDto"/> objects containing movie information.</returns>
        public virtual List<MovieWebDto> GetMoviesFromPage(int page)
        {
            List<MovieWebDto> movieList = [];
            HtmlWeb web = new();
            var htmlDoc = web.Load($"{ORIGINAL_URI + PageNumberUri + page}");

            var elements = htmlDoc.DocumentNode.SelectNodes(GetMoviePageList);
            int count = 0;
            foreach (var node in elements)
            {
                count++;
                movieList.Add(GetMovieInfo(node));
                Console.WriteLine($"Movie {count}");
            }
            return movieList;
        }

        /// <summary>
        /// Extracts movie information (name, image, URL, and overview) from a movie node.
        /// </summary>
        /// <param name="node">The HTML node containing the movie information.</param>
        /// <returns>A <see cref="MovieWebDto"/> containing the movie data.</returns>
        private MovieWebDto GetMovieInfo(HtmlNode node)
        {
            var movieName = node.SelectSingleNode(GetMovieName).InnerText;
            var movieUrl = node.SelectSingleNode(GetMovieUrl).GetAttributeValue("href", "ERROR");
            var movieImage = WebUtility.UrlDecode(
                node.SelectSingleNode(GetMovieImage)?
                    .GetAttributeValue("src", "ERROR")
                    .Replace(ReplaceMovieUri, "")
            );

            var movieData = new MovieWebDto
            {
                Name = movieName,
                Img = movieImage,
                Url = movieUrl,
                Overview = GetOverView(movieUrl),
                ScrapPageID = DB_WEB_ID
            };

            return movieData;
        }

        /// <summary>
        /// Retrieves the overview (description) of a movie by scraping the description node.
        /// </summary>
        /// <param name="uri">The URI of the movie to retrieve the description from.</param>
        /// <returns>The movie overview or "Error" if the description cannot be retrieved.</returns>
        private string GetOverView(string uri)
        {
            string? node;

            try
            {
                HtmlWeb web = new();
                var htmlDoc = web.Load(ORIGINAL_URI + uri);
                node = htmlDoc.DocumentNode.SelectSingleNode(GetMovieDescription)?.InnerText;
            }
            catch
            {
                node = "Error";
            }
            return node;
        }
    }
}
