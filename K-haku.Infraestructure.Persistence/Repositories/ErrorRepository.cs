using K_haku.Core.Application.Interface.Repositories;
using K_haku.Core.Application.Interface.Repositories.Cuevana;
using K_haku.Core.Domain.Entities;
using K_haku.Core.Domain.Entities.Cuevana;
using K_haku.Infraestructure.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace K_haku.Infraestructure.Persistence.Repositories
{
    public class ErrorRepository : GenericRepository<Error>, IErrorRepository
    {
        private readonly K_hakuContext _dbContext;

        public ErrorRepository(K_hakuContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
