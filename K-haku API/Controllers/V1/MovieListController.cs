using K_haku.Core.Application.Dtos.Movie;
using K_haku.Core.Application.Enum;
using K_haku.Core.Application.Features.MovieList.Queries.GetAll;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Net.Mime;
using System.Security.Claims;
using System.Threading.Tasks;

namespace K_haku_API.Controllers.V1
{
    [SwaggerTag("Movie List from Database")]
    public class MovieListController : BaseESApiController
    {
        [Authorize(Roles = "Owner")]
        [HttpGet("GetMovieList")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<MovieListResponse>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
            Summary = "Movie List",
            Description = "Get All Movie List from Database"
            )]
        public async Task<IActionResult> Get([FromQuery] GetAllMovieListParameters parameters, string Bearer)
        {
            var movieList = await Mediator.Send(new GetAllMovieListQuery
            {
                MovieName = parameters.MovieName,
                ReleaseDate = parameters.ReleaseDate,
                Skip = parameters.Skip
            });

            //string userName = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;



            if (movieList == null || movieList.Count == 0)
            {
                return NotFound();
            }
            return Ok(movieList);
        }
    }
}
