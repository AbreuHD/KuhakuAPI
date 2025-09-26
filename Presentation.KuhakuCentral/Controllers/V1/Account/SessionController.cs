using KuhakuCentral.Controllers.General;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shomei.Infraestructure.Identity.DTOs.Generic;
using Shomei.Infraestructure.Identity.Features.UserSessions.Commands;
using Shomei.Infraestructure.Identity.Features.UserSessions.Queries;
using Shomei.Infraestructure.Identity.Middleware;

namespace KuhakuCentral.Controllers.V1.Account
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class SessionController(IMediator mediator, ILogger<SessionController> logger) : BaseApi
    {
        public new IMediator Mediator { get; } = mediator;

        [HttpDelete("current")]
        [MultipleSessionAuthorize]
        public async Task<IActionResult> LogoutCurrentSession()
        {
            var response = await Mediator.Send(new LogoutCurrentSessionCommand());
            return StatusCode(response.Statuscode, response);
        }

        [HttpDelete("all")]
        [MultipleSessionAuthorize]
        public async Task<IActionResult> LogoutFromAllSessions()
        {
            var response = await Mediator.Send(new LogoutAllSessionsCommand());
            return StatusCode(response.Statuscode, response);
        }

        [HttpDelete("{id:int}")]
        [MultipleSessionAuthorize]
        public async Task<IActionResult> LogoutSessionById([FromRoute] int id)
        {
            var response = await Mediator.Send(new LogoutSessionByIdCommand { Id = id });
            return StatusCode(response.Statuscode, response);
        }

        [HttpGet]
        [MultipleSessionAuthorize]
        public async Task<IActionResult> GetAllUserSessions()
        {
            var response = await Mediator.Send(new GetAllUserSessionsQuery());
            return StatusCode(response.Statuscode, response);
        }
        [HttpGet("valid")]
        [Authorize]
        public async Task<IActionResult> ValidSession()
        {
            var response = new GenericApiResponse<bool>()
            {
                Message = "All ok",
                Statuscode = StatusCodes.Status200OK,
            };
            return StatusCode(response.Statuscode, response);
        }

        [HttpGet("valid-profile")]
        [MultipleSessionAuthorize]
        public async Task<IActionResult> ValidProfileSession()
        {
            var response = new GenericApiResponse<bool>()
            {
                Message = "All ok",
                Statuscode = StatusCodes.Status200OK,
            };
            return StatusCode(response.Statuscode, response);
        }
    }
}
