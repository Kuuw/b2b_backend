using AutoMapper;
using BL.Abstract;
using DAL.Abstract;
using Entities.DTO;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace BL.Concrete
{
    public class CompanyService : GenericService<Company, CompanyPostDto, CompanyGetDto, CompanyPutDto>, ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly Mapper mapper = MapperConfig.InitializeAutomapper();

        public CompanyService(ICompanyRepository companyRepository)
            : base(companyRepository)
        {
            _companyRepository = companyRepository;
        }

        public ServiceResult<CompanyGetDto?> GetByEmail(string email)
        {
            var company = _companyRepository.GetByEmail(email);
            return ServiceResult<CompanyGetDto?>.Ok(mapper.Map<CompanyGetDto?>(company));
        }

        public ServiceResult<List<CompanyGetDto>> GetPaged(int page, int pageSize)
        {
            var companies = _companyRepository.GetPaged(page, pageSize, q => q.Include(x => x.Status).Include(x => x.Address));
            return ServiceResult<List<CompanyGetDto>>.Ok(mapper.Map<List<CompanyGetDto>>(companies));
        }
    }
}
