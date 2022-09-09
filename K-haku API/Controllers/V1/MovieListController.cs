using K_haku.Core.Application.Dtos.Movie;
using K_haku.Core.Application.Features.MovieList.Queries.GetAll;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace K_haku_API.Controllers.V1
{
    public class MovieListController : BaseApiController
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<MovieListResponse>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get([FromQuery] GetAllMovieListParameters parameters)
        {
            var movieList = await Mediator.Send(new GetAllMovieListQuery
            {
                MovieName = parameters.MovieName,
                ReleaseDate = parameters.ReleaseDate,
                Skip = parameters.Skip
            });

            if(movieList.Count == 0 || movieList == null)
            {
                return NotFound();
            }
            return Ok(movieList);
        }
    }
}
