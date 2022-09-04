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
        Task AddWareAsync(
            string userId, int wareId);
        Task CreateAsync(
            User user);
        Task DeleteWareAsync(
            string userId, int wareId);
        Task<PaginatedList<WareBriefInfoDTO>> GetPageByClientAsync(
            PaginationFilterCartDTO paginationFilterCartDTO);
        Task<PaginatedList<WareBriefInfoDTO>> GetPageByClientAsync(
            string userId, PaginationFilterDTO paginationFilterDTO);
    }
}
