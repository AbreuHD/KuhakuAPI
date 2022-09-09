using K_haku.Core.Application.Dtos.Movie;
using K_haku.Core.Application.Features.Cuevana.Commands.GetCuevanaMovies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace K_haku_API.Controllers.V1
{
    public class ScrapController : BaseApiController
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CuevanaScrap()
        {
            await Mediator.Send(new GetCuevanaMoviesCommand());
            return Ok();
        }
    }
}
