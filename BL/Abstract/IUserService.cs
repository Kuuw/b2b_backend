using Entities.DTO;
using Entities.Models;

namespace BL.Abstract
{
    public interface IUserService : IGenericService<User, UserPostDto, UserGetDto, UserPutDto>
    {
    }
}
