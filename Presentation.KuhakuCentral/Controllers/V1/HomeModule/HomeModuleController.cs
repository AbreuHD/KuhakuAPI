using Core.Application.Features.SearchMovieModule.Queries.SearchMovies;
using KuhakuCentral.Controllers.General;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;

namespace KuhakuCentral.Controllers.V1.HomeModule
{
    public class HomeModuleController : BaseAPI 
    {
        [HttpGet("GetHome")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
            Summary = "Home Page",
            Description = "Get Home Page Data"
            )]
        public async Task<IActionResult> Home()
        {
            return Ok();
        }
    }
}
