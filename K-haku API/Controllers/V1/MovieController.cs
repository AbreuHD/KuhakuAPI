using K_haku.Core.Application.Dtos.Movie;
using K_haku.Core.Application.Enum;
using K_haku.Core.Application.Features.MovieInfo.Queries.GetMovieInfo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading.Tasks;

namespace K_haku_API.Controllers.V1
{
    [SwaggerTag("Movie")]
    [Authorize(Roles = "Owner, User")]
    public class MovieController : BaseESApiController
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MovieInfoResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
            Summary = "GetMovie",
            Description = "Get Movie From TMDB ID"
            )]
        public async Task<IActionResult> Movie([FromQuery] GetMovieInfoParameters parameters)
        {
            return Ok(await Mediator.Send(new GetMovieInfoQuery
            {
                TMBDId = parameters.TMBDId
            }));
        }
    }
}
