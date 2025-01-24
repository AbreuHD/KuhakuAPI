using Core.Application.Interface.Repositories;
using Core.Domain.Entities.UserThings;
using Infrastructure.Persistence.Context;

namespace Infrastructure.Persistence.Repositories
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
