using Core.Application.Interface.Repositories;
using Core.Domain.Entities.WebScraping;
using Infraestructure.Persistence.Context;

namespace Infraestructure.Persistence.Repositories
{
    public class MovieWebRepository : GenericRepository<MovieWeb>, IMovieWebRepository
    {
        private readonly KhakuContext _dbContext;

        public MovieWebRepository(KhakuContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
