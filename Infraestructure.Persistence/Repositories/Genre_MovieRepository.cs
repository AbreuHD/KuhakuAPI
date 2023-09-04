using Core.Application.Interface.Repositories;
using Core.Domain.Entities.Relations;
using Infraestructure.Persistence.Context;

namespace Infraestructure.Persistence.Repositories
{
    public class Genre_MovieRepository : GenericRepository<Genre_Movie>, IGenre_MovieRepository
    {
        private readonly KhakuContext _dbContext;

        public Genre_MovieRepository(KhakuContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
