using Core.Application.DTOs.Account;
using Core.Application.DTOs.General;

namespace Core.Application.Interface.Services
{
    public interface IUserService
    {
        Task<GenericApiResponse<AuthenticationResponse>> Login(AuthenticationRequest login);
        Task SignOut();
        Task<GenericApiResponse<RegisterResponse>> Register(RegisterRequest viewModel, string origin);
        Task UpdateUser(string Id, RegisterRequest viewModel);
        Task<string> EmailConfirm(string userId, string token);
        Task<GenericApiResponse<String>> ForgotPassword(ForgotPasswordRequest request, string origin);
        Task<GenericApiResponse<String>> ResetPassword(ResetPasswordRequest request);
    }
}
