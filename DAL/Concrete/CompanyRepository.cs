using DAL.Abstract;
using Entities.Models;

namespace DAL.Concrete
{
    class CompanyRepository : GenericRepository<Company>, ICompanyRepository
    {
    }
}
