using AutoMapper;
using BL.Abstract;
using DAL.Abstract;
using Entities.DTO;
using Entities.Models;

namespace BL.Concrete
{
    public class PermissionService : IPermissionService
    {
        private readonly IPermissionRepository _permissionRepository;
        private readonly Mapper mapper = MapperConfig.InitializeAutomapper();

        public PermissionService(IPermissionRepository permissionRepository)
        {
            _permissionRepository = permissionRepository;
        }

        public ServiceResult<bool> Insert(PermissionPostDto data)
        {
            var permission = mapper.Map<Permission>(data);
            var result = _permissionRepository.Insert(permission);
            return ServiceResult<bool>.Ok(result);
        }

        public ServiceResult<PermissionGetDto?> GetById(Guid id)
        {
            var permission = _permissionRepository.GetById(id);
            if (permission == null)
            {
                return ServiceResult<PermissionGetDto?>.NotFound("Permission not found");
            }
            return ServiceResult<PermissionGetDto?>.Ok(mapper.Map<PermissionGetDto?>(permission!));
        }

        public ServiceResult<bool> Update(PermissionPutDto data)
        {
            var permission = mapper.Map<Permission>(data);
            var result = _permissionRepository.Update(permission);
            return ServiceResult<bool>.Ok(result);
        }

        public ServiceResult<bool> Delete(Guid guid)
        {
            Permission? permission = _permissionRepository.GetById(guid);
            if (permission == null)
            {
                return ServiceResult<bool>.NotFound("Permission not found");
            }
            return ServiceResult<bool>.Ok(_permissionRepository.Delete(permission));
        }
    }
}
