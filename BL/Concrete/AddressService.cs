using AutoMapper;
using BL.Abstract;
using DAL.Abstract;
using Entities.Context.Abstract;
using Entities.DTO;
using Entities.Models;

namespace BL.Concrete
{
    public class AddressService : GenericService<Address, AddressPostDto, AddressGetDto, AddressPutDto>, IAddressService
    {
        private readonly IAddressRepository _addressRepository;
        private readonly Mapper _mapper = MapperConfig.InitializeAutomapper();
        private readonly IUserContext _userContext;

        public AddressService(IAddressRepository addressRepository, IUserContext userContext) : base(addressRepository)
        {
            _addressRepository = addressRepository;
            _userContext = userContext;
        }

        public ServiceResult<List<AddressGetDto>?> GetByUserId(Guid userId)
        {
            var address = _addressRepository.Where(x => x.UserId == userId);
            return ServiceResult<List<AddressGetDto>?>.Ok(_mapper.Map<List<AddressGetDto>?>(address));
        }

        public ServiceResult<List<AddressGetDto>?> GetByCompanyId(Guid companyId)
        {
            var address = _addressRepository.Where(x => x.CompanyId == companyId);
            return ServiceResult<List<AddressGetDto>?>.Ok(_mapper.Map<List<AddressGetDto>?>(address));
        }

        public ServiceResult<List<AddressGetDto>?> GetSelfAddresses()
        {
            var address = _addressRepository.Where(x => x.UserId == _userContext.UserId);
            return ServiceResult<List<AddressGetDto>?>.Ok(_mapper.Map<List<AddressGetDto>?>(address));
        }
    }
}
