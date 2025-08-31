using Core.Application.Helpers.TMDB;
using Core.Application.Interface.Scraping;
using Core.Application.Services.WebScrapers.Common;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Core.Application
{
    public static class ServiceRegistration
    {
        public static void AddKhakuLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            services.AddTransient<GetTmdbData, GetTmdbData>();
            services.AddTransient<IScrapingService, ScrapingService>();
        }
    }
}