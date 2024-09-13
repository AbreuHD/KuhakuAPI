using Core.Application.Features.SearchMovieModule.Queries.SearchMovieModule.SearchMoviePages;
using Core.Application.Features.SearchMovieModule.Queries.SearchMovieModule.SearchMovies;
using KuhakuCentral.Controllers.General;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;

namespace KuhakuCentral.Controllers.V1.MovieSearchModule
{
    public class MovieSearchModuleController : BaseAPI
    {
        [HttpGet("Search")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
            Summary = "Movie List",
            Description = "Get All Movie List from Database"
            )]
        public async Task<IActionResult> Search(string Title, List<int> Values)
        {
            return Ok(await Mediator.Send(new SearchMoviesQuery { Title = Title, Values = Values }));
        }

        [HttpGet("Info")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
        Summary = "MovieData",
        Description = "Get All Movie Links from Database"
        )]
        public async Task<IActionResult> Info(int MovieId)
        {
            return Ok(await Mediator.Send(new SearchMovieInfoQuery { MovieId = MovieId }));
        }
    }
}
