using Core.Application.Enums;
using KuhakuCentral.Controllers.General;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shomei.Infraestructure.Identity.DTOs.Account;
using Shomei.Infraestructure.Identity.DTOs.Generic;
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
    public class AccountController(IMediator mediator, ILogger<AccountController> logger) : BaseApi
    {
        public new IMediator Mediator { get; } = mediator;
        private readonly ILogger<AccountController> _logger = logger;

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] AuthLoginQuery request)
        {
            var response = await Mediator.Send(request);
            return StatusCode(response.Statuscode, response);
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterAccountRequestDto requestDto)
        {
            var request = new CreateAccountCommand(Roles.User.ToString(), VerificationMode.Otp, false)
            {
                Dto = requestDto,
            };
            var response = await Mediator.Send(request);
            return StatusCode(response.Statuscode, response);
        }

        [HttpGet("ConfirmEmai")]
        public async Task<IActionResult> ConfirmEmailWithOtp([FromQuery] AuthEmailWithOtpCommand request)
        {
            var response = await Mediator.Send(request);
            return StatusCode(response.Statuscode, response);
        }

        [HttpPost("ResentConfirmation")]
        public async Task<IActionResult> ResentConfirmation([FromBody] SendValidationEmailAgainRequestDto requestDto)
        {

            var request = new SendValidationEmailAgainCommand(VerificationMode.Otp)
            {
                Dto = requestDto,
            };
            var response = await Mediator.Send(request);
            return Ok(response);
        }

        [HttpPut("ChangePassword")]
        [MultipleSessionAuthorize]
        public async Task<IActionResult> ChangePassword(ChangePasswordCommand request)
        {
            var response = await Mediator.Send(request);
            return StatusCode(response.Statuscode, response);
        }

        [HttpPut("ChangeEmail")]
        [Authorize]
        public async Task<IActionResult> ChangeEmail(ChangeEmailCommand request)
        {
            var response = await Mediator.Send(request);
            return StatusCode(response.Statuscode, response);
        }

        [HttpPut("RequestEmailChangeOtp")]
        [Authorize]
        public async Task<IActionResult> RequestEmailChangeOtp(RequestEmailChangeOtpCommand request)
        {
            var response = await Mediator.Send(request);
            return StatusCode(response.Statuscode, response);
        }

        [HttpPut("ChangeEmailWithOtp")]
        [Authorize]
        public async Task<IActionResult> ChangeEmailWithOtp(ChangeEmailWithOtpCommand request)
        {
            var response = await Mediator.Send(request);
            return StatusCode(response.Statuscode, response);
        }

        [HttpPut("GeneratePasswordResetOtp")]
        [Authorize]
        public async Task<IActionResult> GeneratePasswordResetOtp([FromBody] string email)
        {
            var response = await Mediator.Send(new GeneratePasswordResetOtpCommand() { Email = email });
            return StatusCode(response.Statuscode, response);
        }

        [HttpGet("ValidSession")]
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

        [HttpGet("ValidProfileSession")]
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

        [HttpGet("GetInfo")]
        [MultipleSessionAuthorize]
        public async Task<IActionResult> GetInfo()
        {
            var response = await Mediator.Send(new GetDataFromJwtCommand());
            return StatusCode(response.Statuscode, response);
        }
    }
}
