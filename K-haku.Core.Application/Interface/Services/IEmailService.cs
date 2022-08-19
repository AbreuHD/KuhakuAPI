using K_haku.Core.Application.Dtos.Email;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace K_haku.Core.Application.Interface.Services
{
    public interface IEmailService
    {
        Task SendAsync(EmailRequest request);
    }
}
