using Entities.DTO;
using Entities.Models;

namespace BL.Abstract
{
    public interface ILogService : IGenericService<Log, LogPostDto, LogGetDto, LogGetDto>
    {
        public ServiceResult<bool> InsertType(LogTypePostDto data);

        public ServiceResult<LogTypeGetDto?> GetTypeById(Guid id);

        public ServiceResult<List<LogTypeGetDto>> GetTypesPaged(int page, int pageSize);

        public ServiceResult<bool> UpdateType(LogTypePutDto data);

        public ServiceResult<bool> DeleteType(Guid id);
    }
}
