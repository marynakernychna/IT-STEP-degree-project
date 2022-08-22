using Core.DTO;
using Core.DTO.PaginationFilter;
using Core.DTO.Ware;
using Core.Entities;
using Core.Helpers;
using System.Threading.Tasks;

namespace Core.Interfaces.CustomService
{
    public interface ICartService
    {
        Task CreateAsync(User user);
        Task AddWareAsync(string userId, int wareId);
        Task DeleteWareAsync(string userId, int wareId);
        Task<PaginatedList<WareBriefInfoDTO>> GetByUserIdAsync(
            string userId, PaginationFilterDTO paginationFilterDTO);
        Task<PaginatedList<WareBriefInfoDTO>> GetByUserIdAsync(
            string userId, PaginationFilterCartDTO paginationFilterCartDTO);
    }
}
