using Auth.Core.Application.Features.Login.Queries.AuthLogin;
using Auth.Infraestructure.Identity.Features.Register.Commands.CreateAccount;
using KuhakuCentral.Controllers.General;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace KuhakuCentral.Controllers.V1.Account
{
    public class AccountController(IMediator mediator, ILogger<AccountController> logger) : BaseAPI
    {
        public IMediator Mediator { get; } = mediator;
        private readonly ILogger<AccountController> _logger = logger;

        [HttpPost("Login")]
        public async Task<IActionResult> AuthLogin([FromBody] AuthLoginQuery request)
        {
            var data = await Mediator.Send(request);
            return Ok(data);
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] CreateAccountCommand request)
        {
            var data = await Mediator.Send(request);
            return Ok(data);
        }
    }
}
