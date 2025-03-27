using AutoMapper;
using BL.Abstract;
using DAL.Abstract;
using Entities.Context.Abstract;
using Entities.DTO;
using Entities.Models;

namespace BL.Concrete
{
    public class UserService : GenericService<User, UserPostDto, UserGetDto, UserPutDto>, IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IBcryptService _bcryptService;
        private readonly Mapper _mapper = MapperConfig.InitializeAutomapper();
        private readonly IUserContext _userContext;

        public UserService(IUserRepository userRepository, IBcryptService bcryptService, IUserContext userContext) : base(userRepository)
        {
            _userRepository = userRepository;
            _bcryptService = bcryptService;
            _userContext = userContext;
        }

        public ServiceResult<bool> Register(UserRegister userRegister)
        {
            var user = _mapper.Map<User>(userRegister);
            user.PasswordHash = _bcryptService.HashPassword(userRegister.Password);

            var userData = _userRepository.Insert(user);

            if (userData == null)
            {
                return ServiceResult<bool>.InternalServerError("User could not be registered.");
            }
            return ServiceResult<bool>.Ok(true);
        }

        public ServiceResult<List<UserGetDto?>> GetByCompanyId(Guid companyId)
        {
            var user = _userRepository.Where(x => x.CompanyId == companyId);
            return ServiceResult<List<UserGetDto?>>.Ok(_mapper.Map<List<UserGetDto?>>(user));
        }

        public ServiceResult<UserGetDto> GetSelf()
        {
            return ServiceResult<UserGetDto>.Ok(_mapper.Map<UserGetDto>(_userRepository.GetById(_userContext.UserId)));
        }
    }
}
