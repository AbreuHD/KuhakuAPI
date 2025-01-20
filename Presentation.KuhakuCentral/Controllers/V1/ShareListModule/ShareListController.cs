using Core.Application.Features.ShareListModule.AllShareListQuery.Queries;
using Core.Application.Features.ShareListModule.CreateNewList.Commands;
using Core.Application.Features.ShareListModule.LogedUserLists.Queries;
using KuhakuCentral.Controllers.General;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;

namespace KuhakuCentral.Controllers.V1.ShareListModule
{
    public class ShareListController : BaseAPI
    {
        [HttpPost("CreateList")]
        [Authorize]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
                    Summary = "Create List",
                    Description = "Endpoint to create a new Share list of movies and series"
                    )]
        public async Task<IActionResult> CreateList(string name, string img, [FromBody] string? description)
        {
            var USERID = User.FindFirst("uid")!.Value;

            var response = await Mediator.Send(
                new CreateNewListCommand
                {
                    UserId = USERID,
                    Name = name,
                    Img = img,
                    Description = description
                });

            return StatusCode(response.Statuscode, response);
        }

        [HttpPost("LogedUserShareList")]
        [Authorize]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
                    Summary = "See Loged User Share Lists",
                    Description = "Endpoint to all user loged share lists"
                    )]
        public async Task<IActionResult> LogedUserList()
        {
            var USERID = User.FindFirst("uid")!.Value;

            var response = await Mediator.Send(
                new LogedUserQuery
                {
                    UserId = USERID
                });

            return StatusCode(response.Statuscode, response);
        }

        [HttpPost("AllShareList")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
            Summary = "See Loged User Lists",
            Description = "Endpoint to all user loged lists"
            )]
        public async Task<IActionResult> AllShareList()
        {
            var response = await Mediator.Send(new AllShareListQuery { } );
            return StatusCode(response.Statuscode, response);
        }
    }
}
