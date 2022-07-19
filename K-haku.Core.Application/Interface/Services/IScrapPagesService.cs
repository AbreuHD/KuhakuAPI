using K_haku.Core.Application.Interface.Service;
using K_haku.Core.Application.ViewModels.ScrapPages;
using K_haku.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace K_haku.Core.Application.Interface.Services
{
    public interface IScrapPagesService : IGenericService<ScrapPagesViewModel, ScrapPagesInfoViewModel, ScrapPages>
    {
        Task<List<ScrapPagesInfoViewModel>> GetAllViewModelWhitInclude();
    }
}
