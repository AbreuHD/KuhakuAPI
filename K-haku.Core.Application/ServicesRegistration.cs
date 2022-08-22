using K_haku.Core.Application.Inferfaces.Service;
using K_haku.Core.Application.Interface.Services;
using K_haku.Core.Application.Interface.Services.Cuevana;
using K_haku.Core.Application.Services;
using K_haku.Core.Application.Services.Cuevana;
using K_haku.Core.Application.WebsScrapers.GetAll.Cuevana;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Core.Application
{
    public static class ServiceRegistration
    {
        public static void AddK_hakuLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            //services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<ICuevanaMoviesService, CuevanaMoviesService>();
            services.AddTransient<IScrapPagesService, ScrapPagesMoviesService>();
            services.AddTransient<CuevanaGetAllMovies, CuevanaGetAllMovies>();
        }

    }
}
