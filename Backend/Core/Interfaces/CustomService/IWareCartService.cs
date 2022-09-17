using Core.DTO;
using Core.DTO.Ware;
using Core.Helpers;
using System.Threading.Tasks;

namespace Core.Interfaces.CustomService
{
    public interface IWareCartService
    {
        Task CheckForWareDuplicateAsync(
            int cartId,
            int wareId);
        Task CheckIfCartIsEmptyAsync(
            int cartId);
        Task CreateAsync(
            int cartId,
            int wareId);
        Task DeleteAsync(
            int cartId,
            int wareId);
        Task<PaginatedList<WareBriefInfoDTO>> GetPageByCartAsync(
            PaginationFilterDTO paginationFilterDTO,
            int cartId);
        Task ReduceAvailableCountAsync(
            int cartId);
        Task ReturnAvailableCountAsync(
            int cartId);
    }
}
