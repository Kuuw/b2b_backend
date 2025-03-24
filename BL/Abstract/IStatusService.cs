using Entities.DTO;
using Entities.Models;

namespace BL.Abstract
{
    public interface IStatusService : IGenericService<Status, StatusPostDto, StatusGetDto, StatusPutDto>
    {
    }
}
