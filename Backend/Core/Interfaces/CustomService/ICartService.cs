using Core.DTO;
using Core.DTO.Ware;
using Core.Entities;
using Core.Helpers;
using System.Threading.Tasks;

namespace Core.Interfaces.CustomService
{
    public interface ICartService
    {
        Task CreateAsync(User user);
        Task<PaginatedList<WareBriefInfoDTO>> GetByUserIdAsync(
            string userId, PaginationFilterDTO paginationFilterDTO);
    }
}
