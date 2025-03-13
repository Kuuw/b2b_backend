using DAL.Abstract;
using Entities.Models;

namespace DAL.Concrete
{
    class PermissionRepository : GenericRepository<Permission>, IPermissionRepository
    {
    }
}
