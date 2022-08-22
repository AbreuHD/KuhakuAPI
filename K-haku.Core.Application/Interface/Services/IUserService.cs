using K_haku.Core.Application.DTOS.Account;
using K_haku.Core.Application.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace K_haku.Core.Application.Inferfaces.Service
{
    public interface IUserService
    {
        Task<AuthenticationResponse> Login(LoginViewModel login);
        Task SignOut();
        Task<RegisterResponse> Regiter(UserSaveViewModel viewModel, string origin);
        Task UpdateUser(string Id, UserSaveViewModel viewModel);
        Task<string> EmailConfirm(string userId, string token);
        Task<GenericResponse> ForgotPassword(ForgotPasswordViewModel request, string origin);
        Task<GenericResponse> ResetPassword(ResetPasswordViewModel request);
    }
}
