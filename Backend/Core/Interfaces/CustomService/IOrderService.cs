using Core.DTO;
using Core.DTO.Order;
using Core.Helpers;
using System.Threading.Tasks;

namespace Core.Interfaces.CustomService
{
    public interface IOrderService
    {
        Task AssignAsync(
            string courierId, int orderId);
        Task ConfirmDeliveryAsync(
            string userId, int orderId);
        Task CreateAsync(
            string userId, OrderDTO createOrderDTO);
        Task DeleteAsync(
            string userId, int orderId);
        Task<PaginatedList<UserOrderInfoDTO>> GetPageByClientAsync(
            string userId, PaginationFilterDTO paginationFilterDTO);
        Task<PaginatedList<OrderInfoDTO>> GetPageOfAssignedByCourierAsync(
            string courierId, PaginationFilterDTO paginationFilterDTO);
        Task<PaginatedList<OrderInfoDTO>> GetPageOfAvailvableAsync(
            PaginationFilterDTO paginationFilterDTO);
        Task<PaginatedList<DeliveredOrderDTO>> GetPageOfDeliveredAsync(
            string userId, PaginationFilterDTO paginationFilterDTO);
        Task RejectAsync(
            int orderId, string courierId);
        Task RejectDeliveryAsync(
            string userId, int orderId);
        Task UpdateAsync(
            ChangeOrderInfoDTO changeOrderInfoDTO, string userId);
    }
}
