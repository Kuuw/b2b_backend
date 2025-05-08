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

        public ServiceResult<List<CompanyReportDto>> GetReports(int page, int pageSize)
        {
            var companies = _companyRepository.GetPaged(page, pageSize, q => q.Include(x => x.Status).Include(x => x.Address));
            var companyReports = companies.Select(company => new CompanyReportDto
            {
                CompanyId = company.CompanyId,
                CompanyName = company.CompanyName,
                UserCount = _companyRepository.getUserCount(company.CompanyId),
                AverageSpent = _companyRepository.getAverageSpent(company.CompanyId),
                TotalSpent = _companyRepository.getTotalSales(company.CompanyId),
                TotalOrders = _companyRepository.getTotalOrders(company.CompanyId),
                LastOrderDate = _companyRepository.lastOrderDate(company.CompanyId),
            }).ToList();
            return ServiceResult<List<CompanyReportDto>>.Ok(companyReports);
        }
    }
}
