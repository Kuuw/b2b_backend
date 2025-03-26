using Entities.DTO;
using Entities.Models;

namespace BL.Abstract
{
    public interface IAuthService
    {
        public ServiceResult<AuthenticateResponse?> Authenticate(UserLogin userLogin);
    }
}
