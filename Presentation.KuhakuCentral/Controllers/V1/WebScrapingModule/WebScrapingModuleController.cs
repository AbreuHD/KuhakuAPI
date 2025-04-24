using Core.Application.DTOs.General;
using Core.Application.Features.Scraping.Cuevana.Cuevana3.ch.Commands.GetAllCuevanaMovies;
using Core.Application.Features.Scraping.PelisPlusLat.Commands.GetPelisPlusLatMovies;
using KuhakuCentral.Controllers.General;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;
using System.Net.Mime;

namespace KuhakuCentral.Controllers.V1.WebScrapingModule
{
    public class WebScrapingModuleController : BaseApi
    {

        [HttpGet("ScrapPage")]
        [Authorize(Roles = "Owner")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
        Summary = "Web Scraping",
        Description = "Get all movies from Page and send them to the Database"
        )]
        public async Task<IActionResult> ScrapPage(int Id)
        {
            switch (Id)
            {
                case 1:
                    await Mediator.Send(new GetAllCuevanaMoviesCommand());
                    break;
                case 2:
                    await Mediator.Send(new GetPelisPlusLatMoviesCommand());
                    break;
            }
            return Ok(new GenericApiResponse<string>
            {
                Payload = "Done",
                Message = HttpStatusCode.Accepted.ToString(),
                Statuscode = 200,
                Success = true
            });
        }
    }
}
