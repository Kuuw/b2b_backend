using DAL.Abstract;
using Entities.Models;

namespace DAL.Concrete
{
    public class LogRepository : GenericRepository<Log>, ILogRepository
    {
    }
}
