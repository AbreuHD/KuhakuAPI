using K_haku.Core.Application.Dtos.Account;
using K_haku.Core.Application.DTOS.Account;
using K_haku.Core.Application.Enum;
using K_haku.Core.Application.Helpers;
using K_haku.Core.Application.Inferfaces.Service;
using K_haku.Core.Application.ViewModels.User;
using K_haku.Infrastructure.Identity.Services;
using K_haku_API.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;

namespace K_haku_Backend.Controllers
{
    public class AccountController : BaseGeneralApiController
    {
        private readonly IUserService _userService;
        private readonly IHttpContextAccessor _httpContext;
        private readonly AuthenticationResponse user;

        public AccountController(IUserService userService)
        {
            _userService = userService;

        }

        [HttpPost("Login")]
        public async Task<IActionResult> Authentication(LoginViewModel request)
        {
            return Ok(await _userService.Login(request));
        }

        [HttpPost("ChangePassWord")]
        public async Task<IActionResult> Register(string Id, UserSaveViewModel newPassword)
        {
            await _userService.UpdateUser(Id, newPassword);
            return Ok();
        }

        /*[HttpPost("Register")]
        public async Task<IActionResult> Register(UserSaveViewModel userSaveView)
        {
            var origin = Request.Headers["origin"];
            RegisterResponse response = await _userService.Regiter(userSaveView, origin);
            return Ok(response);
        }

        [HttpPost("EmailConfirm")]
        public async Task<IActionResult> Register(string userId, string token)
        {
            string response = await _userService.EmailConfirm(userId, token);
            return Ok(response);
        }

        [HttpPost("ForgotPassword")]
        [ServiceFilter(typeof(LoginResponse))]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel forgot)
        {
            var origin = Request.Headers["origin"];
            GenericResponse response = await _userService.ForgotPassword(forgot, origin);
            return Ok(response);
        }

        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel reset)
        {
            GenericResponse response = await _userService.ResetPassword(reset);
            return Ok(response);
        }*/

    }
}
