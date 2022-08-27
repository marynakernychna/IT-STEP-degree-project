using Core.DTO;
using Core.DTO.Order;
using Core.Helpers;
using System.Threading.Tasks;

namespace Core.Interfaces.CustomService
{
    public interface IOrderService
    {
        Task CreateAsync(string userId, CreateOrderDTO createOrderDTO);
        Task<PaginatedList<OrderInfoDTO>> GetAvailableAsync(
            PaginationFilterDTO paginationFilterDTO);
        Task AssignToOrderAsync(string courierId, int orderId);
    }
}
