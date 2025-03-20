using DAL.Abstract;
using Entities.Models;

namespace DAL.Concrete
{
    public class RoleRepository : GenericRepository<Role>, IRoleRepository
    {
    }
}
