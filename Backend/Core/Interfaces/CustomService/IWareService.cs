using Core.DTO;
using Core.DTO.PaginationFilter;
using Core.DTO.Ware;
using Core.Entities;
using Core.Helpers;
using System.Threading.Tasks;

namespace Core.Interfaces.CustomService
{
    public interface IWareService
    {
        Task CheckIfExistsByIdAsync(
            int wareId);
        Task CreateAsync(
            CreateWareDTO createWareDTO, string userId);
        Task<PaginatedList<WareBriefInfoDTO>> GetAllAsync(
            PaginationFilterDTO paginationFilter);
        Task<PaginatedList<WareBriefInfoDTO>> GetByCategoryAsync(
            PaginationFilterWareDTO paginationFilter);
        Task<Ware> GetByIdAsync(
            int id);
        Task<PaginatedList<WareBriefInfoDTO>> GetCreatedByUserAsync(
            string userId, PaginationFilterDTO paginationFilter);
        Task<WareInfoDTO> FormWareInfoDTOByIdAsync(
            int id);
    }
}
