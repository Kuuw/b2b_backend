using BL.Abstract;
using DAL.Abstract;
using Entities.DTO;
using Entities.Models;

namespace BL.Concrete
{
    public class StatusService : GenericService<Status, StatusPostDto, StatusGetDto, StatusPutDto>, IStatusService
    {
        private readonly IStatusRepository _statusRepository;

        public StatusService(IStatusRepository statusRepository) : base(statusRepository)
        {
            _statusRepository = statusRepository;
        }
    }
}
