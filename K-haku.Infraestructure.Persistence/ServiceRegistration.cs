using K_haku.Core.Application.Interface.Repositories;
using K_haku.Core.Application.Interface.Repositories.Cuevana;
using K_haku.Infraestructure.Persistence.Contexts;
using K_haku.Infraestructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace K_haku.Infraestructure.Persistence
{
    public static class ServiceRegistration 
    { 
        public static void AddPersistenceInfraestructure(this IServiceCollection services, IConfiguration configuration)
        {
            if (configuration.GetValue<bool>("UseInMemoryDatabase"))
            {
                services.AddDbContext<K_hakuContext>(options =>
                        options.UseInMemoryDatabase("K_hakuDB"));
            }
            else
            {
                services.AddDbContext<K_hakuContext>(options =>
                        options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"), m =>
                            m.MigrationsAssembly(typeof(K_hakuContext).Assembly.FullName)));
            }

            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddTransient<IScrapPagesRepository, ScrapPagesRepository>();
            services.AddTransient<ICuevanaMoviesRepository, CuevanaMoviesRepository>();
        }
    }
}
