using Core.Application.Interface.Repositories;
using Core.Domain.Entities.UserThings;
using Infraestructure.Persistence.Context;

namespace Infraestructure.Persistence.Repositories
{
    public class RecentsRepository : GenericRepository<Recents>, IRecentsRepository
    {
        private readonly KhakuContext _dbContext;

        public RecentsRepository(KhakuContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
