using Core.Application.Interface.Repositories;
using Core.Domain.Entities.WebScraping;
using Infrastructure.Persistence.Context;

namespace Infrastructure.Persistence.Repositories
{
    public class ScrapPageRepository : GenericRepository<ScrapPage>, IScrapPageRepository
    {
        private readonly KhakuContext _dbContext;

        public ScrapPageRepository(KhakuContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
