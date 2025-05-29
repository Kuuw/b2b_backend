using AutoMapper;
using BL.Abstract;
using DAL.Abstract;
using Entities.Context.Abstract;
using Entities.DTO;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

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
            var query = BuildCompanyReportQuery(filter.Filter);
            var companies = _companyRepository.GetPaged(filter.PageNumber, filter.PageSize, q => query);
            var companyReports = companies.Select(company => CreateCompanyReport(company, filter.Filter)).ToList();

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
            var company = _companyRepository.GetById(user!.CompanyId);

            filter.Filter.Search = null;

            var users = _userRepository.GetPaged(filter.PageNumber, filter.PageSize,
                x => x.Where(x => x.CompanyId == user.CompanyId));

            var userReports = users.Select(u => CreateUserReport(u, filter.Filter)).ToList();
            var companyReport = CreateCompanyReport(company, filter.Filter, userReports);

            var result = new ReportSelfResponse
            {
                TotalPages = _userRepository.GetPageCount(filter.PageSize, q => q.Where(x => x.CompanyId == user.CompanyId)),
                TotalUsers = _companyRepository.GetUserCount(company.CompanyId, filter.Filter),
                MonthlyStats = _companyRepository.GetMonthlyStats(company.CompanyId, filter.Filter),
                UsersPageNumber = filter.PageNumber,
                UsersPageSize = filter.PageSize,
                Report = companyReport
            };

            return ServiceResult<ReportSelfResponse?>.Ok(result);
        }

        private IQueryable<Company> BuildCompanyReportQuery(ReportFilter filter)
        {
            var query = _companyRepository.Queryable()
                .Include(x => x.Status)
                .Include(x => x.Address)
                .Where(x => x.CompanyName.Contains(filter.Search ?? ""));

            query = ApplyDateFilters(query, filter);
            query = ApplySpentFilters(query, filter);
            query = ApplyOrderFilters(query, filter);

            if (filter.Users.Count != 0)
                query = query.Where(x => x.Users.Any(u => filter.Users.Contains(u.UserId)));

            return query;
        }

        private IQueryable<Company> ApplyDateFilters(IQueryable<Company> query, ReportFilter filter)
        {
            if (filter.StartDate != null)
                query = query.Where(x => x.Users.SelectMany(u => u.Orders).Any(o => o.CreatedAt >= filter.StartDate));

            if (filter.EndDate != null)
                query = query.Where(x => x.Users.SelectMany(u => u.Orders).Any(o => o.CreatedAt <= filter.EndDate));

            return query;
        }

        private IQueryable<Company> ApplySpentFilters(IQueryable<Company> query, ReportFilter filter)
        {
            if (filter.MinSpent != null)
                query = query.Where(x => x.Users.SelectMany(u => u.Orders)
                    .SelectMany(o => o.OrderItems)
                    .Sum(oi => oi.Product.Price * oi.Quantity) >= filter.MinSpent);

            if (filter.MaxSpent != null)
                query = query.Where(x => x.Users.SelectMany(u => u.Orders)
                    .SelectMany(o => o.OrderItems)
                    .Sum(oi => oi.Product.Price * oi.Quantity) <= filter.MaxSpent);

            return query;
        }

        private IQueryable<Company> ApplyOrderFilters(IQueryable<Company> query, ReportFilter filter)
        {
            if (filter.MinOrder != null)
                query = query.Where(x => x.Users.SelectMany(u => u.Orders).Count() >= filter.MinOrder);

            if (filter.MaxOrder != null)
                query = query.Where(x => x.Users.SelectMany(u => u.Orders).Count() <= filter.MaxOrder);

            return query;
        }

        private CompanyReportDto CreateCompanyReport(Company company, ReportFilter filter, List<UserReportDto>? userReports = null)
        {
            return new CompanyReportDto
            {
                CompanyId = company.CompanyId,
                CompanyName = company.CompanyName,
                UserCount = _companyRepository.GetUserCount(company.CompanyId, filter),
                TotalSpent = _companyRepository.GetTotalSales(company.CompanyId, filter),
                TotalOrders = _companyRepository.GetTotalOrders(company.CompanyId, filter),
                AverageSpent = _companyRepository.GetAverageSpent(company.CompanyId, filter),
                LastOrderDate = _companyRepository.LastOrderDate(company.CompanyId, filter),
                Users = userReports ?? company.Users.Select(u => CreateUserReport(u, filter)).ToList()
            };
        }

        private UserReportDto CreateUserReport(User user, ReportFilter filter)
        {
            return new UserReportDto
            {
                UserId = user.UserId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                TotalSpent = _userRepository.GetTotalSales(user.UserId, filter),
                TotalOrders = _userRepository.GetTotalOrders(user.UserId, filter),
                AverageSpent = _userRepository.GetAverageSpent(user.UserId, filter),
                LastOrderDate = _userRepository.LastOrderDate(user.UserId, filter)
            };
        }
    }
}