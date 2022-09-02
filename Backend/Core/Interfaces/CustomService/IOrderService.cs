﻿using Core.DTO;
using Core.DTO.Order;
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
        Task AssignToOrderAsync(string courierId, EntityIdDTO idDTO);
        Task RejectSelectedOrderAsync(EntityIdDTO idDTO, string courierId);
        Task<PaginatedList<OrderInfoDTO>> GetByCourierAsync(
            string courierId, PaginationFilterDTO paginationFilterDTO);
        Task ChangeInfoAsync(
            ChangeOrderInfoDTO changeOrderInfoDTO, string userId);
    }
}
