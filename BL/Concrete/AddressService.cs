using AutoMapper;
using BL.Abstract;
using DAL.Abstract;
using Entities.DTO;
using Entities.Models;

namespace BL.Concrete
{
    public class AddressService : GenericService<Address, AddressPostDto, AddressGetDto, AddressPutDto>, IAddressService
    {
        private readonly IAddressRepository _addressRepository;
        private readonly Mapper _mapper = MapperConfig.InitializeAutomapper();

        public AddressService(IAddressRepository addressRepository) : base(addressRepository)
        {
            _addressRepository = addressRepository;
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
    }
}
