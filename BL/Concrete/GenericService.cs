using AutoMapper;
using BL.Abstract;
using DAL.Abstract;
using Entities.Models;

namespace BL.Concrete
{
    public class GenericService<TModel, TPostDto, TGetDto, TPutDto> : IGenericService<TModel, TPostDto, TGetDto, TPutDto>
        where TModel : class
    {
        private readonly IGenericRepository<TModel> _repository;
        private readonly Mapper _mapper = MapperConfig.InitializeAutomapper();

        public GenericService(IGenericRepository<TModel> repository)
        {
            _repository = repository;
        }

        public ServiceResult<bool> Insert(TPostDto data)
        {
            var model = _mapper.Map<TModel>(data);
            return ServiceResult<bool>.Ok(_repository.Insert(model));
        }

        public ServiceResult<TGetDto?> GetById(Guid id)
        {
            var model = _repository.GetById(id);
            return ServiceResult<TGetDto?>.Ok(_mapper.Map<TGetDto?>(model));
        }

        public ServiceResult<List<TGetDto>> GetPaged(int page, int pageSize)
        {
            var models = _repository.GetPaged(page, pageSize);
            return ServiceResult<List<TGetDto>>.Ok(_mapper.Map<List<TGetDto>>(models));
        }

        public ServiceResult<bool> Delete(Guid id)
        {
            var model = _repository.GetById(id);
            if (model == null)
            {
                return ServiceResult<bool>.NotFound($"{typeof(TModel).Name} not found");
            }
            return ServiceResult<bool>.Ok(_repository.Delete(model));
        }

        public ServiceResult<bool> Update(TPutDto data)
        {
            var model = _mapper.Map<TModel>(data);
            return ServiceResult<bool>.Ok(_repository.Update(model));
        }

        public ServiceResult<List<TGetDto>> GetAll()
        {
            var models = _mapper.Map<List<TGetDto>>(_repository.List());
            return ServiceResult<List<TGetDto>>.Ok(models);
        }
    }
}