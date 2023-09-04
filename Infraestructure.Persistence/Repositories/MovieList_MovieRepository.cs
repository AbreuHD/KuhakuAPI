using Core.Application.Interface.Repositories;
using Core.Domain.Entities.Relations;
using Infraestructure.Persistence.Context;

namespace Infraestructure.Persistence.Repositories
{
    public class MovieList_MovieRepository : GenericRepository<MovieList_Movie>, IMovieList_MovieRepository
    {
        private readonly KhakuContext _dbContext;

        public MovieList_MovieRepository(KhakuContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
