using DAL.Abstract;
using Entities.Models;

namespace DAL.Concrete
{
    public class LogTypeRepository : GenericRepository<LogType>, ILogTypeRepository
    {
    }
}
