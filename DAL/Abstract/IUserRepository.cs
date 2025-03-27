using Entities.Models;

namespace DAL.Abstract
{
    public interface IUserRepository : IGenericRepository<User>
    {
        User? GetByEmail(string email);
    }
}
