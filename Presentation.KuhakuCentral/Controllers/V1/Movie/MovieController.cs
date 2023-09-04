using Core.Application.DTOs.Account;
using Core.Application.Enum;
using Core.Application.Features.Movies.Queries;
using KuhakuCentral.Controllers.V1.General;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;

namespace KuhakuCentral.Controllers.V1.Movie
{
    public class MovieController : BaseAPI
    {
        [HttpGet]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
            Summary = "Movie List",
            Description = "Get All Movie List from Database"
            )]
        public async Task<IActionResult> Search(string Title, List<int> Values)
        {
            return Ok(await Mediator.Send(new SearchMoviesQuery { Title = Title, Values = Values}));
        }
    }
}
