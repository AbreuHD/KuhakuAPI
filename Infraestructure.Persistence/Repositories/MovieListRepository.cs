using Core.Application.Interface.Repositories;
using Core.Domain.Entities.UserThings;
using Infraestructure.Persistence.Context;

namespace Infraestructure.Persistence.Repositories
{
    public class MovieListRepository : GenericRepository<ShareList>, IMovieListRepository
    {
        private readonly KhakuContext _dbContext;

        public MovieListRepository(KhakuContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
