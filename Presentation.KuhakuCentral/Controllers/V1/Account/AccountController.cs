using Auth.Infraestructure.Identity.Features.AuthenticateEmail.Command.AuthEmail;
using Auth.Infraestructure.Identity.Features.Login.Queries.AuthLogin;
using Auth.Infraestructure.Identity.Features.Register.Commands.CreateAccount;
using Auth.Infraestructure.Identity.Features.Register.Commands.SendValidationEmailAgain;
using KuhakuCentral.Controllers.General;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace KuhakuCentral.Controllers.V1.Account
{
    public class AccountController(IMediator mediator, ILogger<AccountController> logger) : BaseApi
    {
        public new IMediator Mediator { get; } = mediator;
        private readonly ILogger<AccountController> _logger = logger;

        [HttpPost("Login")]
        public async Task<IActionResult> AuthLogin([FromBody] AuthLoginQuery request)
        {
            var data = await Mediator.Send(request);
            return StatusCode(data.Statuscode, data);
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] CreateAccountCommand request)
        {
            var data = await Mediator.Send(request);
            return StatusCode(data.Statuscode, data);
        }

        [HttpPost("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail([FromQuery] AuthEmailCommand request)
        {
            var data = await Mediator.Send(request);
            return StatusCode(data.Statuscode, data);
        }

        [HttpPost("ResentConfirmation")]
        public async Task<IActionResult> ResentConfirmation([FromBody] SendValidationEmailAgainCommand request)
        {
            var data = await Mediator.Send(request);
            return StatusCode(data.Statuscode, data);
        }
    }
}
