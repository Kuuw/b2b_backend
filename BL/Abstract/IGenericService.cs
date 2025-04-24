using Entities.Models;

namespace BL.Abstract
{
    public interface IGenericService<TModel, TPostDto, TGetDto, TPutDto>
    {
        ServiceResult<bool> Insert(TPostDto data);
        ServiceResult<TGetDto?> GetById(Guid id);
        ServiceResult<List<TGetDto>> GetPaged(int page, int pageSize);
        ServiceResult<bool> Delete(Guid id);
        ServiceResult<bool> Update(TPutDto data);
        ServiceResult<List<TGetDto>> GetAll();
    }
}