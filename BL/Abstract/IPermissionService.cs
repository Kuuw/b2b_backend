using Entities.DTO;
using Entities.Models;

namespace BL.Abstract
{
    public interface IPermissionService : IGenericService<Permission, PermissionPostDto, PermissionGetDto, PermissionPutDto>
    {
    }
}
