using Entities.Models;

namespace DAL.Abstract
{
    public interface ICompanyRepository : IGenericRepository<Company>
    {
        public Company? GetByEmail(string email);
    }
}
