using Core.Application.Enums;
using KuhakuCentral.Controllers.General;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shomei.Infraestructure.Identity.DTOs.Account;
using Shomei.Infraestructure.Identity.Enums;
using Shomei.Infraestructure.Identity.Features.AuthenticateEmail.Command.AuthEmailWithOtp;
using Shomei.Infraestructure.Identity.Features.AuthenticateEmail.Command.GetDataFromJWT;
using Shomei.Infraestructure.Identity.Features.Email.Commands;
using Shomei.Infraestructure.Identity.Features.ForgotPSW.Commands;
using Shomei.Infraestructure.Identity.Features.Login.Queries.AuthLogin;
using Shomei.Infraestructure.Identity.Features.Password.Commads;
using Shomei.Infraestructure.Identity.Features.Register.Commands.CreateAccount;
using Shomei.Infraestructure.Identity.Features.Register.Commands.SendValidationEmailAgain;
using Shomei.Infraestructure.Identity.Middleware;

namespace KuhakuCentral.Controllers.V1.Account
{
    public class AccountController(IMediator mediator) : BaseApi
    {
        public new IMediator Mediator { get; } = mediator;

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] AuthLoginQuery request)
        {
            var response = await Mediator.Send(request);
            return StatusCode(response.Statuscode, response);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterAccountRequestDto requestDto)
        {
            var request = new CreateAccountCommand(Roles.User.ToString(), VerificationMode.Otp, false)
            {
                Dto = requestDto,
            };
            var response = await Mediator.Send(request);
            return StatusCode(response.Statuscode, response);
        }

        [HttpGet("confirm-email")]
        public async Task<IActionResult> ConfirmEmailWithOtp([FromQuery] AuthEmailWithOtpCommand request)
        {
            var response = await Mediator.Send(request);
            return StatusCode(response.Statuscode, response);
        }

        [HttpPost("resend-confirmation")]
        public async Task<IActionResult> ResendConfirmation([FromBody] SendValidationEmailAgainRequestDto requestDto)
        {
            var request = new SendValidationEmailAgainCommand(VerificationMode.Otp)
            {
                Dto = requestDto,
            };
            var response = await Mediator.Send(request);
            return Ok(response);
        }

        [HttpPut("password")]
        [MultipleSessionAuthorize]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordCommand request)
        {
            var response = await Mediator.Send(request);
            return StatusCode(response.Statuscode, response);
        }

        [HttpPost("email/request-change")]
        [Authorize]
        public async Task<IActionResult> RequestEmailChangeOtp([FromBody] RequestEmailChangeOtpCommand request)
        {
            var response = await Mediator.Send(request);
            return StatusCode(response.Statuscode, response);
        }

        [HttpPut("email/change-with-otp")]
        [Authorize]
        public async Task<IActionResult> ChangeEmailWithOtp([FromBody] ChangeEmailWithOtpCommand request)
        {
            var response = await Mediator.Send(request);
            return StatusCode(response.Statuscode, response);
        }

        [HttpPut("email")]
        [Authorize]
        public async Task<IActionResult> ChangeEmail([FromBody] ChangeEmailCommand request)
        {
            var response = await Mediator.Send(request);
            return StatusCode(response.Statuscode, response);
        }

        [HttpPost("password/reset-otp")]
        [AllowAnonymous]
        public async Task<IActionResult> GeneratePasswordResetOtp([FromBody] string email)
        {
            var response = await Mediator.Send(new GeneratePasswordResetOtpCommand() { Email = email });
            return StatusCode(response.Statuscode, response);
        }

        [HttpGet("me")]
        [MultipleSessionAuthorize]
        public async Task<IActionResult> GetInfo()
        {
            var response = await Mediator.Send(new GetDataFromJwtCommand());
            return StatusCode(response.Statuscode, response);
        }
    }
}
