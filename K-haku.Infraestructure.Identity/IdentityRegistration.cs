using K_haku.Infrastructure.Identity.Context;
using K_haku.Infrastructure.Identity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using K_haku.Core.Application.Inferfaces.Service;
using K_haku.Infrastructure.Identity.Services;

namespace K_haku.Infrastructure.Identity
{
    public static class IdentityRegistration
    {
        public static void AddIdentityInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {

            if (configuration.GetValue<bool>("InMemoryDatabase"))
            {
                services.AddDbContext<IdentityContext>(options => options.UseInMemoryDatabase("IdentityDb"));            }
            else
            {
                services.AddDbContext<IdentityContext>(options =>
                {
                    options.EnableSensitiveDataLogging();
                    options.UseSqlServer(configuration.GetConnectionString("IdentityConnection"),
                    m => m.MigrationsAssembly(typeof(IdentityContext).Assembly.FullName));
                });
            }


            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<IdentityContext>().AddDefaultTokenProviders();

            services.ConfigureApplicationCookie(option =>
            {
                option.LoginPath = "/Account";
                option.AccessDeniedPath = "/User/AccessDenied";
            });

            services.AddAuthentication();


            services.AddTransient<IAccountService,AccountService>();

        }
    }
}
