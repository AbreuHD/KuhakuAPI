using Core.Application.Features.GenreModule.Commands.GetAllGenres;
using Core.Application.Features.SearchMovieModule.Queries.HomeModule.GetHomePageData;
using KuhakuCentral.Controllers.General;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;

namespace KuhakuCentral.Controllers.V1.GenreModule
{
    public class GenreModuleController : BaseAPI
    {
        [HttpGet("GetAllGenres")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
            Summary = "Get Genres",
            Description = "Get All Genres from TMDB API"
            )]
        public async Task<IActionResult> Index()
        {
            return Ok(await Mediator.Send(new GetAllGenresCommand()));
        }
    }
}
