using Core.DTO;
using Core.DTO.Order;
using Core.Entities;
using Core.Helpers;
using System.Threading.Tasks;

namespace Core.Interfaces.CustomService
{
    public interface IOrderService
    {
        Task CreateAsync(string userId, OrderDTO createOrderDTO);
        Task<PaginatedList<OrderInfoDTO>> GetAvailableAsync(
            PaginationFilterDTO paginationFilterDTO);
        Task<PaginatedList<UserOrderInfoDTO>> GetByUserAsync(
            string userId, PaginationFilterDTO paginationFilterDTO);
        Task AssignToOrderAsync(string courierId, int orderId);
        Task RejectSelectedOrderAsync(int orderId, string courierId);
        Task<PaginatedList<OrderInfoDTO>> GetByCourierAsync(
            string courierId, PaginationFilterDTO paginationFilterDTO);
        Task ChangeInfoAsync(
            ChangeOrderInfoDTO changeOrderInfoDTO, string userId);
        Task DeleteOrderAsync(string userId, int orderId);
    }
}
