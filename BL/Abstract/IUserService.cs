using Entities.DTO;
using Entities.Models;

namespace BL.Abstract
{
    public interface IUserService : IGenericService<User, UserPostDto, UserGetDto, UserPutDto>
    {
        ServiceResult<bool> Register(UserRegister userRegister);
        ServiceResult<List<UserGetDto?>> GetByCompanyId(Guid companyId);
        ServiceResult<UserGetDto> GetSelf();
        ServiceResult<bool> UpdateSelf(UserPutDto userPutDto);
        new ServiceResult<List<UserGetDto>> GetPaged(int page, int pageSize);
        ServiceResult<bool> AdminInsert(UserPostDto userPostDto);
    }
}
