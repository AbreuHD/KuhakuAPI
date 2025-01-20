using Core.Application.Features.ShareListModule.CreateNewList.Commands;
using KuhakuCentral.Controllers.General;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;

namespace KuhakuCentral.Controllers.V1.ShareListModule
{
    public class ShareListController : BaseAPI
    {
        [HttpPost("CreateList")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
                    Summary = "Create List",
                    Description = "Endpoint to create a new List of movies and series"
                    )]
        public async Task<IActionResult> CreateList(string name, string img, [FromBody] string? description)
        {
            var response = await Mediator.Send(
                new CreateNewListCommand
                {
                    Name = name,
                    Img = img,
                    Description = description
                });

            return StatusCode(response.Statuscode, response);
        }
    }
}
