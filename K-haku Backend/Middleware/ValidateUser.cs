using K_haku.Core.Application.DTOS.Account;
using K_haku.Core.Application.Helpers;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace K_haku.WebApp.Middleware
{
    public class ValidateUser
    {
        private readonly IHttpContextAccessor _httpContext;
        public ValidateUser(IHttpContextAccessor httpContext)
        {
            _httpContext = httpContext;
        }
        public bool HasUser()
        {
            AuthenticationResponse user = _httpContext.HttpContext.Session.Get<AuthenticationResponse>("user");
            return user == null ? false : true;
        }
    }
}
