using Core.Application.Interface.Repositories;
using Core.Domain.Entities.UserThings;
using Infraestructure.Persistence.Context;

namespace Infraestructure.Persistence.Repositories
{
    public class ShareListRepository(KhakuContext dbContext) : GenericRepository<ShareList>(dbContext), IShareListRepository
    {
    }
}
