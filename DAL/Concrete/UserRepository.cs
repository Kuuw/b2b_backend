using DAL.Abstract;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Concrete
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly B2bContext _context = new B2bContext();
        private readonly DbSet<User> _user;

        public UserRepository(B2bContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _user = context.Set<User>();
        }

        public User? GetByEmail(string email)
        {
            return _user
                .Include(x => x.Role)
                .ThenInclude(x => x.Permissions)
                .FirstOrDefault(x => x.Email == email);
        }
    }
}
