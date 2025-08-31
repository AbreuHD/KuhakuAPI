using Core.Application.Interface.Repositories;
using Core.Domain.Entities.WebScraping;
using Infrastructure.Persistence.Context;

namespace Infrastructure.Persistence.Repositories
{
    public class ScrapPageRepository(KhakuContext dbContext) : GenericRepository<ScrapPage>(dbContext), IScrapPageRepository
    {
        private readonly KhakuContext _dbContext = dbContext;
    }
}
