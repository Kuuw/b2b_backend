using Entities.DTO;
using Entities.Models;

namespace BL.Abstract
{
    public interface ICategoryService : IGenericService<Category, CategoryPostDto, CategoryGetDto, CategoryPutDto>
    {
    }
}
