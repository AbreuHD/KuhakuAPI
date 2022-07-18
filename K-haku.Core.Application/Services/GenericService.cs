using AutoMapper;
using K_haku.Core.Application.Interface.Repositories;
using K_haku.Core.Application.Interface.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace K_haku.Core.Application.Services
{
    public class GenericService<SaveViewModel, ViewModelInfo, Entity> : IGenericService<SaveViewModel,ViewModelInfo,Entity>
        where SaveViewModel : class
        where ViewModelInfo : class
        where Entity : class
    {
        private readonly IGenericRepository<Entity> _repository;
        private readonly IMapper  _mapper;
        public GenericService(IGenericRepository<Entity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public virtual async Task<SaveViewModel> Add(SaveViewModel vm)
        {
            Entity entity = _mapper.Map<Entity>(vm);
            entity = await _repository.AddAsync(entity);
            SaveViewModel SaveVM = _mapper.Map<SaveViewModel>(entity);
            return SaveVM;
        }

        public virtual async Task Delete(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            await _repository.DeleteAsync(entity);
        }

        public virtual async Task<List<ViewModelInfo>> GetAllAsync()
        {
            var entityList = await _repository.GetAllAsync();
            return _mapper.Map<List<ViewModelInfo>>(entityList);
        }

        public virtual async Task<SaveViewModel> GetById(int Id)
        {
            Entity entity = await _repository.GetByIdAsync(Id);
            SaveViewModel vm = _mapper.Map<SaveViewModel>(entity);
            return vm;
        }

        public virtual async Task Update(SaveViewModel vm, int ID)
        {
            Entity comment = _mapper.Map<Entity>(vm);
            await _repository.UpdateAsync(comment, ID);
        }
    }
}
