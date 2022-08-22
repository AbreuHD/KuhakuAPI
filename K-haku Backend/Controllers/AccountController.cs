using K_haku.Core.Application.DTOS.Account;
using K_haku.Core.Application.Helpers;
using K_haku.Core.Application.Inferfaces.Service;
using K_haku.Core.Application.ViewModels.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using K_haku.WebApp.Middleware;

namespace K_haku_Backend.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;
        private readonly IHttpContextAccessor _httpContext;
        private readonly AuthenticationResponse user;

        public AccountController(IUserService userService)
        {
            _userService = userService;

        }
        [ServiceFilter(typeof(LoginAuthorize))]
        public IActionResult Index()
        {
            return View(new LoginViewModel());
        }
        [ServiceFilter(typeof(LoginAuthorize))]
        [HttpPost]
        public async Task<IActionResult> Index(LoginViewModel login)
        {
            if (!ModelState.IsValid)
            {
                return View(login);

            }
            AuthenticationResponse user = await _userService.Login(login);
            if(user != null && !user.HasError)
            {
                HttpContext.Session.Set<AuthenticationResponse>("user",user);
                return RedirectToRoute(new { controller = "General", action = "Index" });
            }
            login.HasError = user.HasError;
            login.Error = user.Error;
            return View(login);

        }
        public async Task<IActionResult> LogOut()
        {
            await _userService.SignOut();
            HttpContext.Session.Remove("user");
            return RedirectToRoute(new { controller = "Account", action = "Index" });
        }
        [ServiceFilter(typeof(LoginAuthorize))]
        public IActionResult Register()
        {

            return View(new UserSaveViewModel());
        }
        [ServiceFilter(typeof(LoginAuthorize))]
        [HttpPost]
        public async Task<IActionResult> Register(UserSaveViewModel userSaveView)
        {
            
            if (!ModelState.IsValid)
            {
                return View(userSaveView);
            }
            var origin = Request.Headers["origin"];
            RegisterResponse response = await _userService.Regiter(userSaveView,origin);

            if (response.HasError)
            {
                userSaveView.HasError = response.HasError;
                userSaveView.Error = response.Error;
                return View(userSaveView);
            }
            return RedirectToRoute(new {controller="Account", action="Index"});
        }

        [ServiceFilter(typeof(LoginAuthorize))]
        public async Task<IActionResult> EmailConfirm(string userId, string token)
        {
            string response = await _userService.EmailConfirm(userId, token);
            return View("EmailConfirm",response);
        }
        [ServiceFilter(typeof(LoginAuthorize))]
        public IActionResult ForgotPassword()
        {
            return View(new ForgotPasswordViewModel());
        }
        [HttpPost]
        [ServiceFilter(typeof(LoginAuthorize))]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel forgot)
        {
            if (!ModelState.IsValid)
            {
                return View(forgot);
            }
            var origin = Request.Headers["origin"];
            GenericResponse response = await _userService.ForgotPassword(forgot, origin);
            if (response.HasError)
            {
                forgot.HasError = response.HasError;
                forgot.Error = response.Error;
                return View(forgot);
            }
            return RedirectToRoute(new { controller = "Account", action = "Index" });

        }
        [ServiceFilter(typeof(LoginAuthorize))]
        public IActionResult ResetPassword(string token)
        {
            return View(new ResetPasswordViewModel {Token = token});
        }
        [ServiceFilter(typeof(LoginAuthorize))]
        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel reset)
        {
            if (!ModelState.IsValid)
            {
                return View(reset);
            }
            GenericResponse response = await _userService.ResetPassword(reset);
            if (response.HasError)
            {
                reset.HasError = response.HasError;
                reset.Error = response.Error;
                return View(reset);
            }
            return RedirectToRoute(new { controller = "Account", action = "Index" });

        }

    }
}
