using Entities.DTO;
using Entities.Models;

namespace BL.Abstract
{
    public interface ILogService
    {
        public ServiceResult<bool> Insert(LogPostDto data);

        public ServiceResult<LogGetDto?> GetById(Guid id);

        public ServiceResult<List<LogGetDto>> GetPaged(int page, int pageSize);

        public ServiceResult<bool> Delete(Guid id);

        public ServiceResult<bool> InsertType(LogTypePostDto data);

        public ServiceResult<LogTypeGetDto?> GetTypeById(Guid id);

        public ServiceResult<List<LogTypeGetDto>> GetTypesPaged(int page, int pageSize);

        public ServiceResult<bool> UpdateType(LogTypePutDto data);

        public ServiceResult<bool> DeleteType(Guid id);
    }
}
