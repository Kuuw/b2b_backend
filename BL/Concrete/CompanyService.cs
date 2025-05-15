using AutoMapper;
using BL.Abstract;
using DAL.Abstract;
using Entities.DTO;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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

        public ServiceResult<ReportPagedResponse> GetReports(ReportPagedFilter filter)
        {
            IQueryable<Company> query = _companyRepository.Queryable()
                .Include(x => x.Status)
                .Include(x => x.Address)
                .Where(x => x.CompanyName.Contains(filter.Filter.Search ?? ""))
                .Where(x => filter.Filter.StartDate == null
                    || x.Users.SelectMany(u => u.Orders).Any(o => o.CreatedAt >= filter.Filter.StartDate))
                .Where(x => filter.Filter.EndDate == null
                    || x.Users.SelectMany(u => u.Orders).Any(o => o.CreatedAt <= filter.Filter.EndDate))
                .Where(x => filter.Filter.MinSpent == null
                    || x.Users.SelectMany(u => u.Orders)
                        .SelectMany(o => o.OrderItems)
                        .Sum(oi => oi.Product.Price * oi.Quantity) >= filter.Filter.MinSpent)
                .Where(x => filter.Filter.MaxSpent == null
                    || x.Users.SelectMany(u => u.Orders)
                        .SelectMany(o => o.OrderItems)
                        .Sum(oi => oi.Product.Price * oi.Quantity) <= filter.Filter.MaxSpent)
                .Where(x => filter.Filter.MinOrder == null
                    || x.Users.SelectMany(u => u.Orders).Count() >= filter.Filter.MinOrder)
                .Where(x => filter.Filter.MaxOrder == null
                    || x.Users.SelectMany(u => u.Orders).Count() >= filter.Filter.MaxOrder);

            if (filter.Filter.Users.Count != 0) { query = query.Where(x => x.Users.Any(u => filter.Filter.Users.Contains(u.UserId))); }

            var companies = _companyRepository.GetPaged(
                    filter.PageNumber,
                    filter.PageSize,
                    q => query
            );

            var companyReports = new List<CompanyReportDto>();
            foreach (var company in companies)
            {
                var report = new CompanyReportDto
                {
                    CompanyId = company.CompanyId,
                    CompanyName = company.CompanyName,
                    UserCount = _companyRepository.getUserCount(company.CompanyId, filter.Filter),
                    TotalSpent = _companyRepository.getTotalSales(company.CompanyId, filter.Filter),
                    TotalOrders = _companyRepository.getTotalOrders(company.CompanyId, filter.Filter),
                    AverageSpent = _companyRepository.getAverageSpent(company.CompanyId, filter.Filter),
                    LastOrderDate = _companyRepository.lastOrderDate(company.CompanyId, filter.Filter)
                };
                companyReports.Add(report);
            }

            var response = new ReportPagedResponse
            {
                TotalPages = _companyRepository.GetPageCount(filter.PageSize, q => query),
                PageNumber = filter.PageNumber,
                PageSize = filter.PageSize,
                Items = companyReports
            };

            return ServiceResult<ReportPagedResponse>.Ok(response);
        }
    }
}
