using AutoMapper;
using BL.Abstract;
using DAL.Abstract;
using Entities.DTO;
using Entities.Models;

namespace BL.Concrete
{
    public class UserService : GenericService<User, UserPostDto, UserGetDto, UserPutDto>, IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IBcryptService _bcryptService;
        private readonly Mapper _mapper = MapperConfig.InitializeAutomapper();

        public UserService(IUserRepository userRepository, IBcryptService bcryptService) : base(userRepository)
        {
            _userRepository = userRepository;
            _bcryptService = bcryptService;
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
    }
}
