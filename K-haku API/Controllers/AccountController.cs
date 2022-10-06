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
    }
}
