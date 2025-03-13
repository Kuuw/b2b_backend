using DAL.Abstract;
using Entities.Models;

namespace DAL.Concrete
{
    class StatusRepository : GenericRepository<Status>, IStatusRepository
    {
    }
}
