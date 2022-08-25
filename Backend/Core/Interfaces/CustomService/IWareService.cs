using Core.DTO;
using Core.DTO.PaginationFilter;
using Core.DTO.Ware;
using Core.Helpers;
using System.Threading.Tasks;

namespace Core.Interfaces.CustomService
{
    public interface IWareService
    {
        Task CreateAsync(CreateWareDTO createWareDTO, string userId);
        Task<PaginatedList<WareBriefInfoDTO>> GetAllAsync(
            PaginationFilterDTO paginationFilter);
        Task<PaginatedList<WareBriefInfoDTO>> GetByCategoryAsync(
            PaginationFilterWareDTO paginationFilter);
        Task<WareInfoDTO> GetByIdAsync(int id);
        Task<PaginatedList<WareBriefInfoDTO>> GetCreatedByUserAsync(
            string userId, PaginationFilterDTO paginationFilter);
    }
}
