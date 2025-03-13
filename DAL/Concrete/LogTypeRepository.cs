using DAL.Abstract;
using Entities.Models;

namespace DAL.Concrete
{
    class LogTypeRepository : GenericRepository<LogType>, ILogTypeRepository
    {
    }
}
