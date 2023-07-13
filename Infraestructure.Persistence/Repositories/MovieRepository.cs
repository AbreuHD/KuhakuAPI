using Core.Application.Interface.Repositories;
using Core.Domain.Entities.GeneralMovie;
using Infraestructure.Persistence.Context;

namespace Infraestructure.Persistence.Repositories
{
    public class MovieRepository : GenericRepository<Movie>, IMovieRepository
    {
        private readonly KhakuContext _dbContext;

        public MovieRepository(KhakuContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
