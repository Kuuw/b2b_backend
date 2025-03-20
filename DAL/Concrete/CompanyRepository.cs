using DAL.Abstract;

namespace DAL.Concrete
{
    public class CompanyRepository : GenericRepository<Company>, ICompanyRepository
    {
    }
}
