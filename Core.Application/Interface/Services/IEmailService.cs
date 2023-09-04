using Core.Application.DTOs.Email;

namespace Core.Application.Interface.Services
{
    public interface IEmailService
    {
        Task SendAsync(EmailRequest request);
    }
}
