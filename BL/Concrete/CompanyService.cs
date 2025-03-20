using AutoMapper;
using BL.Abstract;
using DAL.Abstract;
using Entities.DTO;
using Entities.Models;

namespace BL.Concrete
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly Mapper mapper = MapperConfig.InitializeAutomapper();

        public CompanyService(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        public ServiceResult<bool> Insert(CompanyPostDto data)
        {
            var company = mapper.Map<Company>(data);

            return ServiceResult<bool>.Ok(_companyRepository.Insert(company));
        }

        public ServiceResult<CompanyGetDto?> GetByEmail(string email)
        {
            var company = _companyRepository.GetByEmail(email);
            return ServiceResult<CompanyGetDto?>.Ok(mapper.Map<CompanyGetDto?>(company));
        }

        public ServiceResult<CompanyGetDto?> GetById(Guid id)
        {
            var company = _companyRepository.GetById(id);
            return ServiceResult<CompanyGetDto?>.Ok(mapper.Map<CompanyGetDto?>(company));
        }

        public ServiceResult<bool> Update(CompanyPutDto data)
        {
            var company = mapper.Map<Company>(data);
            return ServiceResult<bool>.Ok(_companyRepository.Update(company));
        }

        public ServiceResult<bool> Delete(Guid guid)
        {
            Company? company = _companyRepository.GetById(guid);

            if (company == null)
            {
                return ServiceResult<bool>.NotFound("Company not found");
            }
            return ServiceResult<bool>.Ok(_companyRepository.Delete(company));
        }
    }
}
