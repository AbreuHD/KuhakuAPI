using Core.Application.Features.SearchMovieModule.Queries.SearchMovieModule.SearchMovieInfo;
using Core.Application.Features.SearchMovieModule.Queries.SearchMovieModule.SearchMovies;
using KuhakuCentral.Controllers.General;
using Microsoft.AspNetCore.Mvc;
using Shomei.Infraestructure.Identity.Middleware;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;

namespace KuhakuCentral.Controllers.V1.MovieSearchModule
{
    [ApiController]
    public class MovieSearchModuleController : BaseApi
    {
        [HttpGet]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
            Summary = "Movie List",
            Description = "Get a list of movies from the database with filters and pagination"
        )]
        public async Task<IActionResult> Search(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 30,
            [FromQuery] string? title = null,
            [FromQuery] List<int>? values = null)
        {
            var response = await Mediator.Send(new SearchMoviesQuery
            {
                Title = title,
                Values = values ?? [],
                PageNumber = pageNumber,
                PageSize = pageSize
            });

            return StatusCode(response.Statuscode, response);
        }

        [HttpGet("{movieId:int}")]
        [MultipleSessionAuthorize]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
            Summary = "Movie Data",
            Description = "Get detailed movie info and links from the database"
        )]
        public async Task<IActionResult> Info([FromRoute] int movieId)
        {
            var response = await Mediator.Send(new SearchMovieInfoQuery { MovieId = movieId });
            return StatusCode(response.Statuscode, response);
        }
    }
}
