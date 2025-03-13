using DAL.Abstract;
using Entities.Models;

namespace DAL.Concrete
{
    class RoleRepository : GenericRepository<Role>, IRoleRepository
    {
    }
}
