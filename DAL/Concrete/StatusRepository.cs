using DAL.Abstract;
using Entities.Models;

namespace DAL.Concrete
{
    public class StatusRepository : GenericRepository<Status>, IStatusRepository
    {
    }
}
