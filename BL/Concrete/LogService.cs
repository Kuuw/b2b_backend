using AutoMapper;
using BL.Abstract;
using DAL.Abstract;
using Entities.DTO;
using Entities.Models;

namespace BL.Concrete
{
    public class LogService : GenericService<Log, LogPostDto, LogGetDto, LogGetDto>, ILogService
    {
        private readonly ILogRepository _logRepository;
        private readonly ILogTypeRepository _logTypeRepository;
        private readonly Mapper mapper = MapperConfig.InitializeAutomapper();

        public LogService(ILogRepository logRepository, ILogTypeRepository logTypeRepository)
            : base(logRepository) // Pass the logRepository to the base class constructor
        {
            _logRepository = logRepository;
            _logTypeRepository = logTypeRepository;
        }

        public ServiceResult<bool> InsertType(LogTypePostDto data)
        {
            var checkIfExists = _logTypeRepository.Where([x => x.LogTypeName == data.LogTypeName]);
            if (checkIfExists.Count > 0)
            {
                return ServiceResult<bool>.Conflict("Log type already exists");
            }
            var logType = mapper.Map<LogType>(data);
            return ServiceResult<bool>.Ok(_logTypeRepository.Insert(logType));
        }

        public ServiceResult<LogTypeGetDto?> GetTypeById(Guid id)
        {
            var logType = _logTypeRepository.GetById(id);
            return ServiceResult<LogTypeGetDto?>.Ok(mapper.Map<LogTypeGetDto?>(logType));
        }

        public ServiceResult<List<LogTypeGetDto>> GetTypesPaged(int page, int pageSize)
        {
            var logTypes = _logTypeRepository.GetPaged(page, pageSize);
            return ServiceResult<List<LogTypeGetDto>>.Ok(mapper.Map<List<LogTypeGetDto>>(logTypes));
        }

        public ServiceResult<bool> UpdateType(LogTypePutDto data)
        {
            var logType = mapper.Map<LogType>(data);
            return ServiceResult<bool>.Ok(_logTypeRepository.Update(logType));
        }

        public ServiceResult<bool> DeleteType(Guid id)
        {
            LogType logType = _logTypeRepository.GetById(id);
            if (logType == null)
            {
                return ServiceResult<bool>.NotFound("Log type not found");
            }
            return ServiceResult<bool>.Ok(_logTypeRepository.Delete(logType));
        }
    }
}
