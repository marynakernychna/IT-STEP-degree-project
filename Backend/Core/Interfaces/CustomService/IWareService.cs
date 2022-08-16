using Core.DTO.PaginationFilter;
using Core.DTO.Ware;
using Core.Helpers;
using System.Threading.Tasks;

namespace Core.Interfaces.CustomService
{
    public interface IWareService
    {
        Task CreateAsync(CreateWareDTO createWareDTO, string userId);
        Task<PaginatedList<WareInfoDTO>> GetByCategoryAsync(
            PaginationFilterWareDTO paginationFilter);
    }
}
