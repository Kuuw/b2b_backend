using DAL.Abstract;
using Entities.Models;

namespace DAL.Concrete
{
    public class PermissionRepository : GenericRepository<Permission>, IPermissionRepository
    {
    }
}
