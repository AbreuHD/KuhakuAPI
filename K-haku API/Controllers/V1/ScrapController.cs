using K_haku.Core.Application.Dtos.Movie;
using K_haku.Core.Application.Enum;
using K_haku.Core.Application.Features.Cuevana.Commands.GetCuevanaMovies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace K_haku_API.Controllers.V1
{
    [Authorize(Roles = "Owner")]
    public class ScrapController : BaseGeneralApiController
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
