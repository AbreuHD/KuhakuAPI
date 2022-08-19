using K_haku.Core.Application.Interface.Services;
using K_haku.Core.Domain.Settings;
using K_haku.Infraestructure.Shared.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Infraestructure.Shared
{
    public static class ServiceRegistration
    {
        public static void AddSharedInfraestructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<MailSettings>(configuration.GetSection("MailSettings"));
            services.AddTransient<IEmailService, EmailService>();
        }

    }
}
