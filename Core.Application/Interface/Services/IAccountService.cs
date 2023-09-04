
using Core.Application.DTOs.Account;
using Core.Application.DTOs.General;

namespace Core.Application.Interface.Services
{
    public interface IAccountService
    {
        Task<GenericApiResponse<AuthenticationResponse>> Authentication(AuthenticationRequest request);
        Task<string> ConfirmEmail(string userId, string token);
        Task<GenericApiResponse<String>> ForgotPassword(ForgotPasswordRequest request, string origin);
        Task<GenericApiResponse<RegisterResponse>> Register(RegisterRequest request, string origin);
        Task<GenericApiResponse<String>> UpdateUser(string userId, RegisterRequest request);
        Task<GenericApiResponse<String>> ResetPassword(ResetPasswordRequest request);
        Task SignOut();
    }
}
