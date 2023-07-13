using Core.Application.Interface.Repositories;
using Core.Domain.Entities.Relations;
using Infraestructure.Persistence.Context;

namespace Infraestructure.Persistence.Repositories
{
    public class Movie_MovieWebRepository : GenericRepository<Movie_MovieWeb>, IMovie_MovieWebRepository
    {
        private readonly KhakuContext _dbContext;

        public Movie_MovieWebRepository(KhakuContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
