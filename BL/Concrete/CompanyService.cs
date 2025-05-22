using AutoMapper;
using BL.Abstract;
using DAL.Abstract;
using Entities.Context.Abstract;
using Entities.DTO;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace BL.Concrete
{
    public class CompanyService : GenericService<Company, CompanyPostDto, CompanyGetDto, CompanyPutDto>, ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUserContext _userContext;
        private readonly Mapper mapper = MapperConfig.InitializeAutomapper();

        public CompanyService(ICompanyRepository companyRepository, IUserContext userContext, IUserRepository userRepository)
            : base(companyRepository)
        {
            _companyRepository = companyRepository;
            _userRepository = userRepository;
            _userContext = userContext;
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
                    || x.Users.SelectMany(u => u.Orders).Count() <= filter.Filter.MaxOrder);

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
                    UserCount = _companyRepository.GetUserCount(company.CompanyId, filter.Filter),
                    TotalSpent = _companyRepository.GetTotalSales(company.CompanyId, filter.Filter),
                    TotalOrders = _companyRepository.GetTotalOrders(company.CompanyId, filter.Filter),
                    AverageSpent = _companyRepository.GetAverageSpent(company.CompanyId, filter.Filter),
                    LastOrderDate = _companyRepository.LastOrderDate(company.CompanyId, filter.Filter),
                    Users = company.Users.Select(u => new UserReportDto
                    {
                        UserId = u.UserId,
                        FirstName = u.FirstName,
                        LastName = u.LastName,
                        TotalSpent = _userRepository.GetTotalSales(u.UserId, filter.Filter),
                        TotalOrders = _userRepository.GetTotalOrders(u.UserId, filter.Filter),
                        AverageSpent = _userRepository.GetAverageSpent(u.UserId, filter.Filter),
                        LastOrderDate = _userRepository.LastOrderDate(u.UserId, filter.Filter)
                    }).ToList()
                };
                companyReports.Add(report);
            }

            var response = new ReportPagedResponse
            {
                TotalPages = _companyRepository.GetPageCount(filter.PageSize, q => query),
                PageNumber = filter.PageNumber,
                PageSize = filter.PageSize,
                Items = companyReports,
                MaxSpent = _companyRepository.GetMaxTotalSpentAcrossAllCompanies(filter.Filter),
                MaxOrderCount = _companyRepository.GetMaxOrderCountAcrossAllCompanies(filter.Filter)
            };

            return ServiceResult<ReportPagedResponse>.Ok(response);
        }

        public ServiceResult<ReportSelfResponse?> GetSelfReport(ReportPagedFilter filter)
        {
            var userId = _userContext.UserId;
            var user = _userRepository.GetById(userId);

            filter.Filter.Search = null;

            var company = _companyRepository.GetById(user!.CompanyId);
            var users = _userRepository.GetPaged(filter.PageNumber, filter.PageSize, x => x.Where(x => x.CompanyId == user.CompanyId));

            var userReports = new List<UserReportDto>();
            foreach (var u in users)
            {
                var userReport = new UserReportDto
                {
                    UserId = u.UserId,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    TotalSpent = _userRepository.GetTotalSales(u.UserId, filter.Filter),
                    TotalOrders = _userRepository.GetTotalOrders(u.UserId, filter.Filter),
                    AverageSpent = _userRepository.GetAverageSpent(u.UserId, filter.Filter),
                    LastOrderDate = _userRepository.LastOrderDate(u.UserId, filter.Filter)
                };
                userReports.Add(userReport);
            }

            var report = new CompanyReportDto
            {
                CompanyId = company.CompanyId,
                CompanyName = company.CompanyName,
                UserCount = _companyRepository.GetUserCount(company.CompanyId, filter.Filter),
                TotalSpent = _companyRepository.GetTotalSales(company.CompanyId, filter.Filter),
                TotalOrders = _companyRepository.GetTotalOrders(company.CompanyId, filter.Filter),
                AverageSpent = _companyRepository.GetAverageSpent(company.CompanyId, filter.Filter),
                LastOrderDate = _companyRepository.LastOrderDate(company.CompanyId, filter.Filter),
                Users = userReports
            };

            var result = new ReportSelfResponse
            {
                TotalPages = _userRepository.GetPageCount(filter.PageSize, q => q.Where(x => x.CompanyId == user.CompanyId)),
                TotalUsers = _companyRepository.GetUserCount(company.CompanyId, filter.Filter),
                MonthlyStats = _companyRepository.GetMonthlyStats(company.CompanyId, filter.Filter),
                UsersPageNumber = filter.PageNumber,
                UsersPageSize = filter.PageSize,
                Report = report
            };

            return ServiceResult<ReportSelfResponse?>.Ok(result);
        }
    }
}
