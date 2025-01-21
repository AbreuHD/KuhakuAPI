using Core.Application.Features.ShareListModule.Commands;
using Core.Application.Features.ShareListModule.Queries;
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
        public async Task<IActionResult> CreateList([FromBody]CreateNewListCommand command)
        {
            command.UserId = User.FindFirst("uid")!.Value;
            var response = await Mediator.Send(command);
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

        [HttpGet("SearchShareList")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
            Summary = "Search for Share Lists",
            Description = "Endpoint to Search for Share Lists and Home List Page"
            )]
        public async Task<IActionResult> AllShareList(string? name)
        {
            var response = await Mediator.Send(new SearchShareListQuery { Name = name ?? string.Empty } );
            return StatusCode(response.Statuscode, response);
        }

        [HttpPut("EditShareList")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
            Summary = "Edit Share List",
            Description = "Endpoint to edit a loged user share list"
            )]
        public async Task<IActionResult> EditShareList([FromBody] EditUserShareListCommand command)
        {
            command.UserId = User.FindFirst("uid")!.Value;
            var response = await Mediator.Send(command);
            return StatusCode(response.Statuscode, response);
        }

        [HttpDelete("DelShareList")]
        [Authorize]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
            Summary = "Delete Share List",
            Description = "Endpoint to delete a loged user share list"
            )]
        public async Task<IActionResult> DelShareList(DelShareListCommand command)
        {
            command.UserId = User.FindFirst("uid")!.Value;
            var response = await Mediator.Send(command);
            return StatusCode(response.Statuscode, response);
        }
    }
}
