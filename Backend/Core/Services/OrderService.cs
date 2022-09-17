using Core.Interfaces.CustomService;
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
using Core.Constants;

namespace Core.Services
{
    public class OrderService : IOrderService
    {
        private readonly IRepository<Order> _orderRepository;

        private readonly ICartService _cartService;
        private readonly IIdentityRoleService _identityRoleService;
        private readonly IUserService _userService;
        private readonly IWareCartService _wareCartService;

        public OrderService(
            IRepository<Order> orderRepository,
            ICartService cartService,
            IIdentityRoleService identityRoleService,
            IUserService userService,
            IWareCartService wareCartService)
        {
            _orderRepository = orderRepository;
            _cartService = cartService;
            _identityRoleService = identityRoleService;
            _userService = userService;
            _wareCartService = wareCartService;
        }

        public async Task AssignAsync(
            string courierId,
            int orderId)
        {
            var courier = await _userService.GetByIdAsync(courierId);

            var order = await _orderRepository.GetByIdAsync(orderId);

            ExtensionMethods.OrderNullCheck(order);

            order.Courier = courier;

            await _orderRepository.UpdateAsync(order);
        }

        public async Task ConfirmDeliveryAsync(
            string userId,
            int orderId)
        {
            var userRole = await _identityRoleService
                .GetByUserIdAsync(userId);

            if (userRole == IdentityRoleNames.Client.ToString())
            {
                var order = await GetByCreatorIdAndIdAsync(userId, orderId);

                if (order.CourierId == null)
                {
                    throw new HttpException(
                        ErrorMessages.ORDER_WAS_NOT_PICKED,
                        HttpStatusCode.BadRequest);
                }

                if (order.IsAcceptedByClient)
                {
                    throw new HttpException(
                        ErrorMessages.THE_ORDER_ALREADY_CONFIRMED,
                        HttpStatusCode.BadRequest);
                }

                await ChangeIsAcceptedAsync(userRole, order);
            }
            else if (userRole == IdentityRoleNames.Courier.ToString())
            {
                var order = await _orderRepository.SingleOrDefaultAsync(
                    new OrderSpecification.GetByCourierIdAndId(userId, orderId));

                if (order.IsAcceptedByCourier)
                {
                    throw new HttpException(
                        ErrorMessages.THE_ORDER_ALREADY_CONFIRMED,
                        HttpStatusCode.BadRequest);
                }

                await ChangeIsAcceptedAsync(userRole, order);
            }
            else
            {
                throw new HttpException(HttpStatusCode.Forbidden);
            }
        }

        public async Task CreateAsync(
            string userId,
            OrderDTO createOrderDTO)
        {
            var user = await _userService.GetByIdAsync(userId);

            var cart = await _cartService.GetByUserIdAsync(user.Id);

            await _wareCartService.CheckIfCartIsEmptyAsync(cart.Id);

            var order = await _orderRepository.AddAsync(
                new Order
                {
                    Address = createOrderDTO.Address,
                    City = createOrderDTO.City,
                    Country = createOrderDTO.Country,
                    CartId = cart.Id
                });

            await _cartService.SetOrderIdAsync(order.Id, cart);

            await _cartService.CreateAsync(user);

            await _wareCartService.ReduceAvailableCountAsync(cart.Id);
        }

        public async Task DeleteAsync(
            string userId,
            int orderId)
        {
            var order = await _orderRepository.SingleOrDefaultAsync(
                new OrderSpecification.GetByCreatorIdAndId(userId, orderId));

            if (order == null)
            {
                throw new HttpException(
                    ErrorMessages.THE_ORDER_NOT_FOUND,
                    HttpStatusCode.BadRequest);
            }

            await _orderRepository.DeleteAsync(order);

            await _wareCartService.ReturnAvailableCountAsync(order.CartId);
        }

        public async Task<PaginatedList<UserOrderInfoDTO>> GetPageByClientAsync(
            string userId,
            PaginationFilterDTO paginationFilterDTO)
        {
            var ordersCount = await _orderRepository.CountAsync(
                new OrderSpecification.GetUndeliveredByClient(
                    userId,
                    paginationFilterDTO));

            if (ordersCount == 0)
            {
                return null;
            }

            var ordersList = await _orderRepository.ListAsync(
                new OrderSpecification.GetUndeliveredByClient(
                    userId,
                    paginationFilterDTO));

            var orders = new List<UserOrderInfoDTO>();

            foreach (var order in ordersList)
            {
                orders.Add(new UserOrderInfoDTO
                {
                    Id = order.Id,
                    Address = order.Address,
                    City = order.City,
                    Country = order.Country,
                    PhoneNumber = order.Cart.Creator.PhoneNumber,
                    WaresCount = order.Cart.WareCarts.Count,
                    IsPicked = order.CourierId != null,
                    IsAcceptedByClient = order.IsAcceptedByClient,
                    IsAcceptedByCourier = order.IsAcceptedByCourier
                });
            }

            return PaginatedList<UserOrderInfoDTO>.Evaluate(
                orders,
                paginationFilterDTO.PageNumber,
                ordersCount,
                PaginatedList<OrderInfoDTO>
                    .GetTotalPages(paginationFilterDTO, ordersCount));
        }

        public async Task<PaginatedList<OrderInfoDTO>> GetPageOfAssignedByCourierAsync(
            string courierId,
            PaginationFilterDTO paginationFilterDTO)
        {
            await _userService.CheckIfExistsByIdAsync(courierId);

            var ordersCount = await _orderRepository.CountAsync(
                new OrderSpecification.GetUndeliveredByCourier(
                    courierId,
                    paginationFilterDTO));

            if (ordersCount == 0)
            {
                return null;
            }

            var orders = FormOrderInfoDTOList(
                await _orderRepository.ListAsync(
                    new OrderSpecification.GetUndeliveredByCourier(
                        courierId,
                        paginationFilterDTO)
                    )
                );

            return PaginatedList<OrderInfoDTO>.Evaluate(
                orders,
                paginationFilterDTO.PageNumber,
                ordersCount,
                PaginatedList<OrderInfoDTO>
                    .GetTotalPages(paginationFilterDTO, ordersCount));
        }

        public async Task<PaginatedList<OrderInfoDTO>> GetPageOfAvailvableAsync(
            PaginationFilterDTO paginationFilterDTO)
        {
            var ordersCount = await _orderRepository.CountAsync(
                new OrderSpecification.GetAvailable(paginationFilterDTO));

            if (ordersCount == 0)
            {
                return null;
            }

            var orders = FormOrderInfoDTOList(
                await _orderRepository.ListAsync(
                    new OrderSpecification.GetAvailable(paginationFilterDTO)));

            return PaginatedList<OrderInfoDTO>.Evaluate(
                orders,
                paginationFilterDTO.PageNumber,
                ordersCount,
                PaginatedList<OrderInfoDTO>
                    .GetTotalPages(paginationFilterDTO, ordersCount));
        }

        public async Task<PaginatedList<DeliveredOrderDTO>> GetPageOfDeliveredAsync(
            string userId,
            PaginationFilterDTO paginationFilterDTO)
        {
            var userRole = await _identityRoleService.GetByUserIdAsync(userId);

            if (userRole == IdentityRoleNames.Client.ToString())
            {
                var ordersCount = await _orderRepository.CountAsync(
                    new OrderSpecification.GetClientDeliveredOrders(
                        userId,
                        paginationFilterDTO)
                    );

                if (ordersCount == 0)
                {
                    return null;
                }

                var orders = FormDeliveredOrderDTOList(
                    await _orderRepository.ListAsync(
                        new OrderSpecification.GetClientDeliveredOrders(
                            userId,
                            paginationFilterDTO)
                        )
                    );

                return PaginatedList<DeliveredOrderDTO>.Evaluate(
                    orders,
                    paginationFilterDTO.PageNumber,
                    ordersCount,
                    PaginatedList<OrderInfoDTO>
                        .GetTotalPages(paginationFilterDTO, ordersCount));
            }
            else if (userRole == IdentityRoleNames.Courier.ToString())
            {
                var ordersCount = await _orderRepository.CountAsync(
                    new OrderSpecification.GetCourierDeliveredOrders(
                        userId,
                        paginationFilterDTO)
                    );

                if (ordersCount == 0)
                {
                    return null;
                }

                var orders = FormDeliveredOrderDTOList(
                    await _orderRepository.ListAsync(
                        new OrderSpecification.GetCourierDeliveredOrders(
                            userId,
                            paginationFilterDTO)
                        )
                    );

                return PaginatedList<DeliveredOrderDTO>.Evaluate(
                    orders,
                    paginationFilterDTO.PageNumber,
                    ordersCount,
                    PaginatedList<OrderInfoDTO>
                        .GetTotalPages(paginationFilterDTO, ordersCount));
            }
            else
            {
                throw new HttpException(HttpStatusCode.Forbidden);
            }
        }

        public async Task RejectAsync(
            int orderId,
            string courierId)
        {
            var order = await _orderRepository.SingleOrDefaultAsync(
                new OrderSpecification.GetByCourier(orderId, courierId));

            ExtensionMethods.OrderNullCheck(order);

            order.Courier = null;
            order.CourierId = null;

            await _orderRepository.UpdateAsync(order);
        }

        public async Task RejectDeliveryAsync(
            string userId,
            int orderId)
        {
            var userRole = await _identityRoleService.GetByUserIdAsync(userId);

            if (userRole == IdentityRoleNames.Client.ToString())
            {
                var order = await GetByCreatorIdAndIdAsync(userId, orderId);

                ExtensionMethods.OrderNullCheck(order);
                ExtensionMethods.OrderNotConfirmedClientCheck(order);

                await ChangeIsAcceptedAsync(userRole, order);

            }
            else if (userRole == IdentityRoleNames.Courier.ToString())
            {
                var order = await _orderRepository.SingleOrDefaultAsync(
                    new OrderSpecification.GetByCourierIdAndId(userId, orderId));

                ExtensionMethods.OrderNullCheck(order);
                ExtensionMethods.OrderNotConfirmedCourierCheck(order);

                await ChangeIsAcceptedAsync(userRole, order);
            }
            else
            {
                throw new HttpException(HttpStatusCode.Forbidden);
            }
        }

        public async Task UpdateAsync(
            ChangeOrderInfoDTO changeOrderInfoDTO,
            string userId)
        {
            var newInfo = changeOrderInfoDTO.OrderInfo;

            var order = await GetByCreatorIdAndIdAsync(
                userId,
                changeOrderInfoDTO.OrderId);

            if (order.IsAcceptedByCourier || order.IsAcceptedByClient)
            {
                throw new HttpException(
                    ErrorMessages.DELIVERY_ALREADY_CONFIRMED,
                    HttpStatusCode.InternalServerError);
            }

            if (newInfo.Address == order.Address &&
                newInfo.City == order.City &&
                newInfo.Country == order.Country)
            {
                throw new HttpException(
                    ErrorMessages.THE_PREVIOUS_INFO_IS_THE_SAME,
                    HttpStatusCode.BadRequest);
            }

            order.Address = newInfo.Address;
            order.City = newInfo.City;
            order.Country = newInfo.Country;

            order.CourierId = null;

            await _orderRepository.UpdateAsync(order);
        }

        private async Task ChangeIsAcceptedAsync(
            string userRole,
            Order order)
        {
            if (userRole == IdentityRoleNames.Client.ToString())
            {
                order.IsAcceptedByClient = !order.IsAcceptedByClient;

                await _orderRepository.UpdateAsync(order);

            }
            else if (userRole == IdentityRoleNames.Courier.ToString())
            {
                order.IsAcceptedByCourier = !order.IsAcceptedByCourier;

                await _orderRepository.UpdateAsync(order);
            }
            else
            {
                throw new HttpException(HttpStatusCode.Forbidden);
            }
        }

        private async Task<Order> GetByCreatorIdAndIdAsync(
            string userId,
            int orderId)
        {
            var order = await _orderRepository
                .SingleOrDefaultAsync(
                    new OrderSpecification.GetByCreatorIdAndId(
                        userId,
                        orderId)
                    );

            ExtensionMethods.OrderNullCheck(order);

            return order;
        }

        private static List<DeliveredOrderDTO> FormDeliveredOrderDTOList(
            List<Order> ordersList)
        {
            var orders = new List<DeliveredOrderDTO>();

            foreach (var order in ordersList)
            {
                var creator = order.Cart.Creator;

                orders.Add(new DeliveredOrderDTO
                {
                    Id = order.Id,
                    Address = order.Address,
                    City = order.City,
                    Country = order.Country,
                    FullName = creator.Name + ' ' + creator.Surname,
                    PhoneNumber = creator.PhoneNumber,
                    WaresCount = order.Cart.WareCarts.Count,
                    IsAcceptedByClient = order.IsAcceptedByClient,
                    IsAcceptedByCourier = order.IsAcceptedByCourier
                });
            }

            return orders;
        }

        private static List<OrderInfoDTO> FormOrderInfoDTOList(
            List<Order> ordersList)
        {
            var orders = new List<OrderInfoDTO>();

            foreach (var order in ordersList)
            {
                var user = order.Cart.Creator;

                orders.Add(new OrderInfoDTO
                {
                    Id = order.Id,
                    Address = order.Address,
                    City = order.City,
                    Country = order.Country,
                    ClientFullName = user.Name + ' ' + user.Surname,
                    ClientPhoneNumber = user.PhoneNumber,
                    WaresCount = order.Cart.WareCarts.Count,
                    IsAcceptedByClient = order.IsAcceptedByClient,
                    IsAcceptedByCourier = order.IsAcceptedByCourier
                });
            }

            return orders;
        }
    }
}
