using Core.Application.Interface.Repositories;
using Core.Domain.Entities.WebScraping;
using Infraestructure.Persistence.Context;

namespace Infraestructure.Persistence.Repositories
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
