using Core.Application.DTOs.Account;

namespace Core.Application.Interface.Services
{
    public interface IUserService
    {
        Task<AuthenticationResponse> Login(AuthenticationRequest login);
        Task SignOut();
        Task<RegisterResponse> Register(RegisterRequest viewModel, string origin);
        Task UpdateUser(string Id, RegisterRequest viewModel);
        Task<string> EmailConfirm(string userId, string token);
        Task<GenericResponse> ForgotPassword(ForgotPasswordRequest request, string origin);
        Task<GenericResponse> ResetPassword(ResetPasswordRequest request);
    }
}
