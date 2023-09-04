using Core.Application.Interface.Repositories;
using Infraestructure.Persistence.Context;
using Infraestructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infraestructure.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceInfraestructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<KhakuContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"), m =>
                    m.MigrationsAssembly(typeof(KhakuContext).Assembly.FullName)));

            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddTransient<IScrapPageRepository, ScrapPageRepository>();
            services.AddTransient<IGenre_MovieRepository, Genre_MovieRepository>();
            services.AddTransient<IGenreRepository, GenreRepository>();
            services.AddTransient<IMovie_MovieWebRepository, Movie_MovieWebRepository>();
            services.AddTransient<IMovieList_MovieRepository, MovieList_MovieRepository>();
            services.AddTransient<IMovieListRepository, MovieListRepository>();
            services.AddTransient<IMovieRepository, MovieRepository>();
            services.AddTransient<IMovieWebRepository, MovieWebRepository>();
            services.AddTransient<IRecentsRepository, RecentsRepository>();
            services.AddTransient<IScrapPageRepository, ScrapPageRepository>();
            services.AddTransient<IUserEntityRepository, UserEntityRepository>();
        }
    }
}