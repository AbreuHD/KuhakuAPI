using Core.Application.DTOs.General;
using Core.Application.Features.Scraping.Cuevana.Cuevana3.ch.Commands.GetAllCuevanaMovies;
using Core.Application.Features.Scraping.PelisPlusLat.Commands.GetPelisPlusLatMovies;
using KuhakuCentral.Controllers.General;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;

namespace KuhakuCentral.Controllers.V1.WebScraping
{
    public class WebScrapingModuleController : BaseAPI
    {

        [HttpGet("Cuevana3.ch")]
        [Authorize(Roles = "Owner")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
        Summary = "Web Scraping at Cuevana3.ch",
        Description = "Get all movies from Cuevana3.ch and send them to the Database"
        )]
        public async Task<IActionResult> ScrapCuevana3CH()
        {
            await Mediator.Send(new GetAllCuevanaMoviesCommand());
            return Ok(new GenericApiResponse<String>
            {
                Message = "Done",
                Statuscode = 200,
                Success = true
            });
        }


        [HttpGet("Pelisplushd.lat")]
        [Authorize(Roles = "Owner")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
            Summary = "Web Scraping at PelisPlus.lat",
            Description = "Get all movies from PelisPlus.lat and send them to the Database"
        )]
        public async Task<IActionResult> ScrapPelisPlusLat()
        {
            await Mediator.Send(new GetPelisPlusLatMoviesCommand());
            return Ok(new GenericApiResponse<String>
            {
                Message = "Done",
                Statuscode = 200,
                Success = true
            });
        }
    }
}
