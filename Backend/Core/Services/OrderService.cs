﻿using Core.Interfaces.CustomService;
using System.Threading.Tasks;
using Core.Interfaces;
using Core.Entities;
using Core.DTO.Order;
using Core.Helpers;
using Core.Specifications;
using Core.Exceptions;
using Core.Resources;
using System.Net;
using Core.DTO;
using System.Collections.Generic;

namespace Core.Services
{
    public class OrderService : IOrderService
    {
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Order> _orderRepository;
        private readonly IRepository<Cart> _cartRepository;
        private readonly IRepository<WareCart> _wareCartRepository;
        private readonly ICartService _cartService;

        public OrderService(
            IRepository<User> userRepository,
            IRepository<Order> orderRepository,
            IRepository<Cart> cartRepository,
            IRepository<WareCart> wareCartRepository,
            ICartService cartService)
        {
            _userRepository = userRepository;
            _orderRepository = orderRepository;
            _cartRepository = cartRepository;
            _wareCartRepository = wareCartRepository;
            _cartService = cartService;
        }

        public async Task CreateAsync(string userId, CreateOrderDTO createOrderDTO)
        {
            var user = await _userRepository.GetByIdAsync(userId);

            ExtensionMethods.UserNullCheck(user);

            var cart = await _cartRepository.SingleOrDefaultAsync(
                new CartSpecification.GetByCreatorId(user.Id));

            if (cart == null)
            {
                throw new HttpException(
                    ErrorMessages.CartNotFound,
                    HttpStatusCode.InternalServerError);
            }

            var waresCount = await _wareCartRepository.CountAsync(
                new WareCartSpecification.GetCartWares(cart.Id));

            if (waresCount == 0)
            {
                throw new HttpException(
                    ErrorMessages.CartIsEmpty,
                    HttpStatusCode.BadRequest);
            }

            var order = await _orderRepository.AddAsync(
                new Order
                {
                    Address = createOrderDTO.Address,
                    City = createOrderDTO.City,
                    Country = createOrderDTO.Country,
                    CartId = cart.Id
                });

            cart.OrderId = order.Id;

            await _cartRepository.UpdateAsync(cart);

            await _cartService.CreateAsync(user);
        }

        public async Task<PaginatedList<OrderInfoDTO>> GetAvailableAsync(
            PaginationFilterDTO paginationFilterDTO)
        {
            var ordersCount = await _orderRepository.CountAsync(
                new OrderSpecification.GetAvailable(paginationFilterDTO));

            if (ordersCount == 0)
            {
                return null;
            }

            var totalPages = PaginatedList<OrderInfoDTO>
                .GetTotalPages(paginationFilterDTO, ordersCount);

            var ordersList = await _orderRepository.ListAsync(
                new OrderSpecification.GetAvailable(paginationFilterDTO));

            var orders = new List<OrderInfoDTO>();

            foreach (var order in ordersList)
            {
                var user = order.Cart.Creator;

                orders.Add(new OrderInfoDTO
                {
                    Address = order.Address,
                    City = order.City,
                    Country = order.Country,
                    ClientFullName = user.Name + ' ' + user.Surname,
                    ClientPhoneNumber = user.PhoneNumber,
                    WaresCount = order.Cart.WareCarts.Count
                });
            }

            return PaginatedList<OrderInfoDTO>.Evaluate(
                orders,
                paginationFilterDTO.PageNumber,
                ordersCount,
                totalPages);
        }

        public async Task AsignToOrderAsync(string courierId, int orderId)
        {
            var courier = await _userRepository.GetByIdAsync(courierId);

            ExtensionMethods.UserNullCheck(courier);

            var order = await _orderRepository.GetByIdAsync(orderId);

            if (order == null)
            {
                throw new HttpException(
                    ErrorMessages.OrderNotFound,
                    HttpStatusCode.InternalServerError);
            }

            order.Courier = courier;

            await _orderRepository.UpdateAsync(order);
        }
    }
}
