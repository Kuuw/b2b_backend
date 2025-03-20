using DAL.Abstract;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Concrete
{
    public class CompanyRepository : GenericRepository<Company>, ICompanyRepository
    {
        private readonly B2bContext _context = new B2bContext();
        private readonly DbSet<Company> _company;

        public CompanyRepository(B2bContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _company = context.Set<Company>();
        }

        public Company? GetByEmail(string email)
        {
            return _company.FirstOrDefault(c => c.Email == email);
        }
    }
}
