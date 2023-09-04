using Core.Application.Interface.Repositories;
using Core.Domain.Entities.GeneralMovie;
using Infraestructure.Persistence.Context;

namespace Infraestructure.Persistence.Repositories
{
    public class GenreRepository : GenericRepository<Genre>, IGenreRepository
    {
        private readonly KhakuContext _dbContext;

        public GenreRepository(KhakuContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
