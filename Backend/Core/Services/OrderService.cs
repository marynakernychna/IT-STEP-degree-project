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
    }
}
