using DAL.Abstract;
using Entities.Models;

namespace DAL.Concrete
{
    class LogRepository : GenericRepository<Log>, ILogRepository
    {
    }
}
