using K_haku.Core.Application.Features.MovieInfo.Queries.GetVideoList;
using K_haku.Core.Application.Features.MovieInfo.Queries.GetVideoSource;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace K_haku_API.Controllers.V1
{
    public class VideosController : BaseApiController
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetVideoListParamaeters))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetVideos([FromQuery] GetVideoListParamaeters parameters)
        {
            return Ok(await Mediator.Send(new GetVideoListQuery() { MovieLink = parameters.MovieLink }));
        }

        [HttpGet("GetVideoSource")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetVideoSourceParameters))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetVideoSource([FromQuery] GetVideoSourceParameters parameters)
        {
            return Ok(await Mediator.Send(new GetVideoSourceQuery() { MovieLink = parameters.MovieLink, Type = parameters.Type }));
        }

    }
}
