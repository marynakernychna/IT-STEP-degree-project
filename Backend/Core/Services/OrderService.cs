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
        private readonly IRepository<Cart> _cartRepository;
        private readonly IRepository<Order> _orderRepository;
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<WareCart> _wareCartRepository;

        private readonly ICartService _cartService;
        private readonly IIdentityRoleService _identityRoleService;

        public OrderService(
            IRepository<Cart> cartRepository,
            IRepository<Order> orderRepository,
            IRepository<User> userRepository,
            IRepository<WareCart> wareCartRepository,
            ICartService cartService,
            IIdentityRoleService identityRoleService)
        {
            _cartRepository = cartRepository;
            _userRepository = userRepository;
            _orderRepository = orderRepository;
            _wareCartRepository = wareCartRepository;
            _cartService = cartService;
            _identityRoleService = identityRoleService;
        }

        public async Task AssignAsync(
            string courierId, int orderId)
        {
            var courier = await CheckCourierIdAsync(courierId);

            var order = await _orderRepository.GetByIdAsync(orderId);

            ExtensionMethods.OrderNullCheck(order);

            order.Courier = courier;

            await _orderRepository.UpdateAsync(order);
        }

        public async Task ConfirmDeliveryAsync(
            string userId, int orderId)
        {
            var user = await _userRepository.GetByIdAsync(userId);

            ExtensionMethods.UserNullCheck(user);

            var userRole = await _identityRoleService.GetByUserAsync(user);

            if (userRole == IdentityRoleNames.Client.ToString())
            {
                var order = await _orderRepository.SingleOrDefaultAsync(
                    new OrderSpecification.GetByCreatorIdAndId(userId, orderId));

                ExtensionMethods.OrderNullCheck(order);

                if (order.IsAcceptedByClient)
                {
                    throw new HttpException(
                        ErrorMessages.OrderAlreadyConfirmed,
                        HttpStatusCode.BadRequest);
                }

                await ChangeIsAcceptedAsync(userRole, order);
            }
            else if (userRole == IdentityRoleNames.Courier.ToString())
            {
                var order = await _orderRepository.SingleOrDefaultAsync(
                    new OrderSpecification.GetByCourierIdAndId(userId, orderId));

                ExtensionMethods.OrderNullCheck(order);

                if (order.IsAcceptedByCourier)
                {
                    throw new HttpException(
                        ErrorMessages.OrderAlreadyConfirmed,
                        HttpStatusCode.BadRequest);
                }

                await ChangeIsAcceptedAsync(userRole, order);
            }
        }

        public async Task CreateAsync(
            string userId, OrderDTO createOrderDTO)
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

        public async Task DeleteAsync(
            string userId, int orderId)
        {
            var order = await _orderRepository.SingleOrDefaultAsync(
                new OrderSpecification.GetByCreatorIdAndId(userId, orderId));

            if (order == null)
            {
                throw new HttpException(
                    ErrorMessages.OrderNotFound,
                    HttpStatusCode.BadRequest);
            }

            await _orderRepository.DeleteAsync(order);
        }

        public async Task<PaginatedList<UserOrderInfoDTO>> GetPageByClientAsync(
            string userId, PaginationFilterDTO paginationFilterDTO)
        {
            var ordersCount = await _orderRepository.CountAsync(
                new OrderSpecification.GetByUser(userId, paginationFilterDTO));

            if (ordersCount == 0)
            {
                return null;
            }

            var totalPages = PaginatedList<OrderInfoDTO>
                .GetTotalPages(paginationFilterDTO, ordersCount);

            var ordersList = await _orderRepository.ListAsync(
                new OrderSpecification.GetByUser(userId, paginationFilterDTO));

            var orders = new List<UserOrderInfoDTO>();

            foreach (var order in ordersList)
            {
                var user = order.Cart.Creator;

                orders.Add(new UserOrderInfoDTO
                {
                    Id = order.Id,
                    Address = order.Address,
                    City = order.City,
                    Country = order.Country,
                    PhoneNumber = user.PhoneNumber,
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
                totalPages);
        }

        public async Task<PaginatedList<OrderInfoDTO>> GetPageOfAssignedByCourierAsync(
            string courierId, PaginationFilterDTO paginationFilterDTO)
        {
            var courier = await CheckCourierIdAsync(courierId);

            var ordersCount = await _orderRepository.CountAsync(
                new OrderSpecification.GetListByCourier(courierId, paginationFilterDTO));

            if (ordersCount == 0)
            {
                return null;
            }

            var totalPages = PaginatedList<OrderInfoDTO>
                .GetTotalPages(paginationFilterDTO, ordersCount);

            var ordersList = await _orderRepository.ListAsync(
                new OrderSpecification.GetListByCourier(courierId, paginationFilterDTO));

            var orders = FormOrderInfoDTOList(ordersList);

            return PaginatedList<OrderInfoDTO>.Evaluate(
                orders,
                paginationFilterDTO.PageNumber,
                ordersCount,
                totalPages);
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

            var totalPages = PaginatedList<OrderInfoDTO>
                .GetTotalPages(paginationFilterDTO, ordersCount);

            var ordersList = await _orderRepository.ListAsync(
                new OrderSpecification.GetAvailable(paginationFilterDTO));

            var orders = FormOrderInfoDTOList(ordersList);

            return PaginatedList<OrderInfoDTO>.Evaluate(
                orders,
                paginationFilterDTO.PageNumber,
                ordersCount,
                totalPages);
        }

        public async Task<PaginatedList<DeliveredOrderDTO>> GetPageOfDeliveredAsync(
            string userId, PaginationFilterDTO paginationFilterDTO)
        {
            var user = await _userRepository.GetByIdAsync(userId);

            ExtensionMethods.UserNullCheck(user);

            var userRole = await _identityRoleService.GetByUserAsync(user);

            if (userRole == IdentityRoleNames.Client.ToString())
            {
                var ordersCount = await _orderRepository.CountAsync(
                    new OrderSpecification.GetClientDeliveredOrders(userId, paginationFilterDTO));

                if (ordersCount == 0)
                {
                    return null;
                }

                var totalPages = PaginatedList<OrderInfoDTO>
                    .GetTotalPages(paginationFilterDTO, ordersCount);

                var ordersList = await _orderRepository.ListAsync(
                    new OrderSpecification.GetClientDeliveredOrders(userId, paginationFilterDTO));

                var orders = FormDeliveredOrderDTOList(ordersList);

                return PaginatedList<DeliveredOrderDTO>.Evaluate(
                    orders,
                    paginationFilterDTO.PageNumber,
                    ordersCount,
                    totalPages);
            }
            else if (userRole == IdentityRoleNames.Courier.ToString())
            {
                var ordersCount = await _orderRepository.CountAsync(
                    new OrderSpecification.GetCourierDeliveredOrders(userId, paginationFilterDTO));

                if (ordersCount == 0)
                {
                    return null;
                }

                var totalPages = PaginatedList<OrderInfoDTO>
                    .GetTotalPages(paginationFilterDTO, ordersCount);

                var ordersList = await _orderRepository.ListAsync(
                    new OrderSpecification.GetCourierDeliveredOrders(userId, paginationFilterDTO));

                var orders = FormDeliveredOrderDTOList(ordersList);

                return PaginatedList<DeliveredOrderDTO>.Evaluate(
                    orders,
                    paginationFilterDTO.PageNumber,
                    ordersCount,
                    totalPages);
            }

            return null;
        }

        public async Task RejectAsync(
            int orderId, string courierId)
        {
            var order = await _orderRepository.SingleOrDefaultAsync(
                new OrderSpecification.GetByCourier(orderId, courierId));

            ExtensionMethods.OrderNullCheck(order);

            order.Courier = null;
            order.CourierId = null;

            await _orderRepository.UpdateAsync(order);
        }

        public async Task RejectDeliveryAsync(
            string userId, int orderId)
        {
            var user = await _userRepository.GetByIdAsync(userId);

            ExtensionMethods.UserNullCheck(user);

            var userRole = await _identityRoleService.GetByUserAsync(user);

            if (userRole == IdentityRoleNames.Client.ToString())
            {
                var order = await _orderRepository.SingleOrDefaultAsync(
                    new OrderSpecification.GetByCreatorIdAndId(userId, orderId));

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
        }

        public async Task UpdateAsync(
            ChangeOrderInfoDTO changeOrderInfoDTO, string userId)
        {
            var newInfo = changeOrderInfoDTO.OrderInfo;

            var order = await _orderRepository.SingleOrDefaultAsync(
                new OrderSpecification.GetByCreatorIdAndId(userId, changeOrderInfoDTO.OrderId));

            ExtensionMethods.OrderNullCheck(order);

            if (newInfo.Address == order.Address &&
                newInfo.City == order.City &&
                newInfo.Country == order.Country)
            {
                throw new HttpException(
                    ErrorMessages.PreviousInfoIsTheSame,
                    HttpStatusCode.BadRequest);
            }

            order.Address = newInfo.Address;
            order.City = newInfo.City;
            order.Country = newInfo.Country;

            order.CourierId = null;

            await _orderRepository.UpdateAsync(order);
        }

        private async Task<User> CheckCourierIdAsync(
            string courierId)
        {
            var courier = await _userRepository.GetByIdAsync(courierId);

            ExtensionMethods.UserNullCheck(courier);

            return courier;
        }

        private async Task ChangeIsAcceptedAsync(
            string userRole, Order order)
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
                    WaresCount = order.Cart.WareCarts.Count
                });
            }

            return orders;
        }
    }
}
