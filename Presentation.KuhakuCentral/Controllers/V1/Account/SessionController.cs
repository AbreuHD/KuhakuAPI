using KuhakuCentral.Controllers.General;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shomei.Infraestructure.Identity.Features.UserSessions.Commands;
using Shomei.Infraestructure.Identity.Features.UserSessions.Queries;
using Shomei.Infraestructure.Identity.Middleware;

namespace KuhakuCentral.Controllers.V1.Account
{
    public class SessionController(IMediator mediator, ILogger<AccountController> logger) : BaseApi
    {
        public new IMediator Mediator { get; } = mediator;
        private readonly ILogger<AccountController> _logger = logger;

        [HttpDelete("LogoutFromAllSessions")]
        [MultipleSessionAuthorize]
        public async Task<IActionResult> LogoutFromAllSessions()
        {
            var response = await Mediator.Send(new LogoutAllSessionsCommand());
            return StatusCode(response.Statuscode, response);
        }

        [HttpDelete("LogoutCurrentSession")]
        [MultipleSessionAuthorize]
        public async Task<IActionResult> LogoutCurrentSession()
        {
            var response = await Mediator.Send(new LogoutCurrentSessionCommand());
            return StatusCode(response.Statuscode, response);
        }

        [HttpDelete("LogoutSessionById")]
        [MultipleSessionAuthorize]
        public async Task<IActionResult> LogoutSessionById(LogoutSessionByIdCommand request)
        {
            var response = await Mediator.Send(request);
            return StatusCode(response.Statuscode, response);
        }

        [HttpGet("GetAllUserSessions")]
        [MultipleSessionAuthorize]
        public async Task<IActionResult> GetAllUserSessions()
        {
            var response = await Mediator.Send(new GetAllUserSessionsQuery());
            return StatusCode(response.Statuscode, response);
        }
    }
}
