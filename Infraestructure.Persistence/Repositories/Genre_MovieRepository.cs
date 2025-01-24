using Core.Application.Interface.Repositories;
using Core.Domain.Entities.Relations;
using Infrastructure.Persistence.Context;

namespace Infrastructure.Persistence.Repositories
{
    public class Genre_MovieRepository : GenericRepository<GenreMovie>, IGenre_MovieRepository
    {
        private readonly KhakuContext _dbContext;

        public Genre_MovieRepository(KhakuContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
