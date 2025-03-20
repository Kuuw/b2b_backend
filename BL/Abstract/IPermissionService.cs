using Entities.DTO;
using Entities.Models;

namespace BL.Abstract
{
    public interface IPermissionService
    {
        public ServiceResult<bool> Insert(PermissionPostDto data);
        public ServiceResult<PermissionGetDto?> GetById(Guid id);
        public ServiceResult<bool> Update(PermissionPutDto data);
        public ServiceResult<bool> Delete(Guid guid);
    }
}
