﻿using Entities.DTO;
using Entities.Models;

namespace BL.Abstract
{
    public interface ICompanyService : IGenericService<Company, CompanyPostDto, CompanyGetDto, CompanyPutDto>
    {
        public ServiceResult<CompanyGetDto?> GetByEmail(string email);
        public ServiceResult<List<CompanyGetDto>> GetPaged(int page, int pageSize);
        public ServiceResult<ReportPagedResponse> GetReports(ReportPagedFilter filter);
        public ServiceResult<ReportSelfResponse?> GetSelfReport(ReportPagedFilter filter);
    }
}
