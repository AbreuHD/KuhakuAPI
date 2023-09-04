using Core.Application.Interface.Repositories;
using Core.Domain.Entities.User;
using Infraestructure.Persistence.Context;

namespace Infraestructure.Persistence.Repositories
{
    public class UserEntityRepository : GenericRepository<UserEntity>, IUserEntityRepository
    {
        private readonly KhakuContext _dbContext;

        public UserEntityRepository(KhakuContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
