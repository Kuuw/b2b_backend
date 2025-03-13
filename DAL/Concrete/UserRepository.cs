using DAL.Abstract;
using Entities.Models;

namespace DAL.Concrete
{
    class UserRepository : GenericRepository<User>, IUserRepository
    {
    }
}
