using Core.Interfaces.CustomService;
using System.Threading.Tasks;
using Core.Interfaces;
using Core.Entities;
using Core.Specifications;
using Core.Helpers;
using Core.Exceptions;
using Core.Resources;
using System.Net;
using Core.DTO.Ware;
using Core.DTO;
using Core.DTO.PaginationFilter;

namespace Core.Services
{
    public class CartService : ICartService
    {
        private readonly IRepository<Cart> _cartRepository;

        private readonly IUserService _userService;
        private readonly IWareService _wareService;
        private readonly IWareCartService _wareCartService;

        public CartService(
            IRepository<Cart> cartRepository,
            IUserService userService,
            IWareService wareService,
            IWareCartService wareCartService)
        {
            _cartRepository = cartRepository;
            _userService = userService;
            _wareService = wareService;
            _wareCartService = wareCartService;
        }

        public async Task AddWareAsync(
            string userId,
            int wareId)
        {
            await _wareService.CheckIfExistsByIdAsync(wareId);

            var cart = await _cartRepository
                .SingleOrDefaultAsync(
                    new CartSpecification.GetByCreatorId(userId));

            if (cart == null)
            {
                throw new HttpException(
                    ErrorMessages.THE_CART_NOT_FOUND,
                    HttpStatusCode.InternalServerError);
            }

            await _wareCartService
                .CheckForWareDuplicateAsync(cart.Id, wareId);

            await _wareCartService.CreateAsync(cart.Id, wareId);
        }

        public async Task CreateAsync(
            User user)
        {
            ExtensionMethods.UserNullCheck(user);

            await _cartRepository.AddAsync(
                new Cart
                {
                    CreatorId = user.Id
                });
        }

        public async Task DeleteWareAsync(
            string userId,
            int wareId)
        {
            var cart = await _cartRepository
                .SingleOrDefaultAsync(
                    new CartSpecification.GetByCreatorId(userId));

            if (cart == null)
            {
                throw new HttpException(
                    ErrorMessages.THE_CART_NOT_FOUND,
                    HttpStatusCode.InternalServerError);
            }

            await _wareCartService.DeleteAsync(cart.Id, wareId);
        }

        public async Task<Cart> GetByUserIdAsync(
            string userId)
        {
            var cart = await _cartRepository.SingleOrDefaultAsync(
                new CartSpecification.GetByCreatorId(userId));

            if (cart == null)
            {
                throw new HttpException(
                    ErrorMessages.THE_CART_NOT_FOUND,
                    HttpStatusCode.InternalServerError);
            }

            return cart;
        }

        public async Task<PaginatedList<WareBriefInfoDTO>> GetPageByClientAsync(
            PaginationFilterCartDTO paginationFilterCartDTO)
        {
            var userId = await _userService
                .GetIdByEmailAsync(paginationFilterCartDTO.UserEmail);

            var paginationFilterDTO = new PaginationFilterDTO()
            {
                PageSize = paginationFilterCartDTO.PageSize,
                PageNumber = paginationFilterCartDTO.PageNumber
            };

            return await GetPageByClientAsync(userId, paginationFilterDTO);
        }

        public async Task<PaginatedList<WareBriefInfoDTO>> GetPageByClientAsync(
            string userId,
            PaginationFilterDTO paginationFilterDTO)
        {
            await _userService.CheckIfExistsByIdAsync(userId);

            var cart = await _cartRepository
                .SingleOrDefaultAsync(
                    new CartSpecification.GetByCreatorId(userId));

            if (cart == null)
            {
                throw new HttpException(
                    ErrorMessages.THE_CART_NOT_FOUND,
                    HttpStatusCode.InternalServerError);
            }

            return await _wareCartService
                .GetPageByCartAsync(
                    paginationFilterDTO,
                    cart.Id
                );
        }

        public async Task SetOrderIdAsync(
            int orderId,
            Cart cart)
        {
            cart.OrderId = orderId;

            await _cartRepository.UpdateAsync(cart);
        }
    }
}
