using Entities.DTO;
using Entities.Models;

namespace BL.Abstract
{
    public interface IRoleService : IGenericService<Role, RolePostDto, RoleGetDto, RolePutDto>
    {
    }
}
