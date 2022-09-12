﻿using Core.Interfaces.CustomService;
using System.Threading.Tasks;
using Core.Interfaces;
using Core.Entities;
using Core.Specifications;
using Core.Helpers;
using Core.Exceptions;
using Core.Resources;
using System.Net;
using System.Collections.Generic;
using Core.DTO.Ware;
using Core.DTO;
using Core.DTO.PaginationFilter;

namespace Core.Services
{
    public class CartService : ICartService
    {
        private readonly IRepository<Cart> _cartRepository;
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Ware> _wareRepository;
        private readonly IRepository<WareCart> _wareCartRepository;

        private readonly IFileService _fileService;
        private readonly IUserService _userService;

        public CartService(
            IRepository<Cart> cartRepository,
            IRepository<User> userRepository,
            IRepository<Ware> wareRepository,
            IRepository<WareCart> wareCartRepository,
            IFileService fileService,
            IUserService userService)
        {
            _cartRepository = cartRepository;
            _userRepository = userRepository;
            _wareRepository = wareRepository;
            _wareCartRepository = wareCartRepository;
            _fileService = fileService;
            _userService = userService;
        }

        public async Task AddWareAsync(
            string userId, int wareId)
        {
            var ware = await _wareRepository.GetByIdAsync(wareId);

            ExtensionMethods.WareNullCheck(ware);

            var cart = await _cartRepository.SingleOrDefaultAsync(
                new CartSpecification.GetByCreatorId(userId));

            if (cart == null)
            {
                throw new HttpException(
                    ErrorMessages.THE_CART_NOT_FOUND,
                    HttpStatusCode.InternalServerError);
            }

            var duplicate = await _wareCartRepository.SingleOrDefaultAsync(
                new WareCartSpecification.GetByIds(cart.Id, wareId));

            if (duplicate != null)
            {
                throw new HttpException(
                    ErrorMessages.THE_WARE_IS_ALREADY_IN_THE_CART,
                    HttpStatusCode.BadRequest);
            }

            await _wareCartRepository.AddAsync(
                new WareCart
                {
                    WareId = wareId,
                    CartId = cart.Id
                });
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
            string userId, int wareId)
        {
            var cart = await _cartRepository.SingleOrDefaultAsync(
                new CartSpecification.GetByCreatorId(userId));

            if (cart == null)
            {
                throw new HttpException(
                    ErrorMessages.THE_CART_NOT_FOUND,
                    HttpStatusCode.InternalServerError);
            }

            var wareCart = await _wareCartRepository.SingleOrDefaultAsync(
                new WareCartSpecification.GetByIds(cart.Id, wareId));

            if (wareCart == null)
            {
                throw new HttpException(
                    ErrorMessages.THE_WARE_NOT_FOUND,
                    HttpStatusCode.BadRequest);
            }

            await _wareCartRepository.DeleteAsync(wareCart);
        }

        public async Task<PaginatedList<WareBriefInfoDTO>> GetPageByClientAsync(
            PaginationFilterCartDTO paginationFilterCartDTO)
        {
            var userId = await _userService.GetIdByEmailAsync(
                paginationFilterCartDTO.UserEmail);

            var paginationFilterDTO = new PaginationFilterDTO()
            {
                PageSize = paginationFilterCartDTO.PageSize,
                PageNumber = paginationFilterCartDTO.PageNumber
            };

            return await GetPageByClientAsync(userId, paginationFilterDTO);
        }

        public async Task<PaginatedList<WareBriefInfoDTO>> GetPageByClientAsync(
            string userId, PaginationFilterDTO paginationFilterDTO)
        {
            var user = await _userRepository.GetByIdAsync(userId);

            ExtensionMethods.UserNullCheck(user);

            var cart = await _cartRepository.SingleOrDefaultAsync(
                new CartSpecification.GetByCreatorId(userId));

            if (cart == null)
            {
                throw new HttpException(
                    ErrorMessages.THE_CART_NOT_FOUND,
                    HttpStatusCode.InternalServerError);
            }

            var waresCount = await _wareCartRepository.CountAsync(
                new WareCartSpecification.GetCartWares(cart.Id, paginationFilterDTO));

            if (waresCount == 0)
            {
                return null;
            }

            var totalPages = PaginatedList<WareBriefInfoDTO>
                .GetTotalPages(paginationFilterDTO, waresCount);

            var wareCarts = await _wareCartRepository.ListAsync(
                new WareCartSpecification.GetCartWares(cart.Id, paginationFilterDTO));

            var wares = new List<WareBriefInfoDTO>();

            foreach (var wareCart in wareCarts)
            {
                var ware = wareCart.Ware;

                wares.Add(new WareBriefInfoDTO
                {
                    Id = ware.Id,
                    Title = ware.Title,
                    Cost = ware.Cost,
                    PhotoBase64 = _fileService.GenereteBase64(ware.PhotoLink),
                    CategoryTitle = ware.Category.Title
                });
            }

            return PaginatedList<WareBriefInfoDTO>.Evaluate(
                wares,
                paginationFilterDTO.PageNumber,
                waresCount,
                totalPages);
        }
    }
}
