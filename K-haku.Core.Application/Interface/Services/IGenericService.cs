using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace K_haku.Core.Application.Interface.Service
{
    public interface IGenericService<SaveViewModel, ViewModel, Entity>
        where SaveViewModel : class
        where ViewModel : class
        where Entity : class
    {
        Task<List<ViewModel>> GetAllAsync();
        Task<SaveViewModel> Add(SaveViewModel vm);
        Task Update(SaveViewModel vm, int ID);
        Task<SaveViewModel> GetById(int Id);
        Task<List<SaveViewModel>> AddAllAsync(List<SaveViewModel> SaveViewModel);
        Task Delete(int id);
    }
}
