using Core.Application.DTOs.Account;
using Core.Application.Interface.Services;
using KuhakuCentral.Controllers.General;
using Microsoft.AspNetCore.Mvc;

namespace KuhakuCentral.Controllers.V1.Account
{
    public class AccountController : BaseAPI
    {
        private readonly IUserService _userService;
        private readonly IHttpContextAccessor _httpContext;
        private readonly AuthenticationResponse user;

        public AccountController(IUserService userService)
        {
            _userService = userService;

        }

        [HttpPost("Login")]
        public async Task<IActionResult> Authentication(AuthenticationRequest request)
        {
            return Ok(await _userService.Login(request));
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            var origin = Request.Headers["Origin"];
            return Ok(await _userService.Register(request, origin));
        }
    }
}
