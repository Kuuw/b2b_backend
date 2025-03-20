using DAL.Abstract;

namespace DAL.Concrete
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
    }
}
