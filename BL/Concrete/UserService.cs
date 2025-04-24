using AutoMapper;
using BL.Abstract;
using DAL.Abstract;
using Entities.Context.Abstract;
using Entities.DTO;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

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
            var user = _userRepository.Where([x => x.CompanyId == companyId]);
            return ServiceResult<List<UserGetDto?>>.Ok(_mapper.Map<List<UserGetDto?>>(user));
        }

        public ServiceResult<UserGetDto> GetSelf()
        {
            return ServiceResult<UserGetDto>.Ok(_mapper.Map<UserGetDto>(_userRepository.GetById(_userContext.UserId)));
        }

        public ServiceResult<bool> UpdateSelf(UserPutDto userPutDto)
        {
            var user = _userRepository.GetById(_userContext.UserId);
            if (user == null)
            {
                return ServiceResult<bool>.NotFound("User not found.");
            }
            user = _mapper.Map(userPutDto, user);
            user.UserId = _userContext.UserId;
            var updatedUser = _userRepository.Update(user);
            if (updatedUser == null)
            {
                return ServiceResult<bool>.InternalServerError("User could not be updated.");
            }
            return ServiceResult<bool>.Ok(true);
        }

        public new ServiceResult<List<UserGetDto>> GetPaged(int page, int pageSize)
        {
            var usersGetDto = _mapper.Map<List<UserGetDto>>(_userRepository.GetPaged(page, pageSize, q => q.Include(x => x.Status).Include(x => x.Role).Include(x => x.Company)));

            return ServiceResult<List<UserGetDto>>.Ok(usersGetDto);
        }

        public ServiceResult<bool> AdminInsert(UserPostDto userPostDto)
        {
            var user = _mapper.Map<User>(userPostDto);
            user.PasswordHash = _bcryptService.HashPassword(userPostDto.PasswordHash);
            if (_userRepository.Insert(user))
            {
                return ServiceResult<bool>.InternalServerError("User could not be registered.");
            }
            return ServiceResult<bool>.Ok(true);
        }
    }
}
