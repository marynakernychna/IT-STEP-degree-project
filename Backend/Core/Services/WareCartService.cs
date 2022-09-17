using Core.Entities;
using Core.Interfaces.CustomService;
using System.Threading.Tasks;
using Core.Interfaces;
using Core.Specifications;
using Core.Exceptions;
using Core.Resources;
using System.Net;
using Core.Helpers;
using Core.DTO.Ware;
using Core.DTO;
using System.Collections.Generic;

namespace Core.Services
{
    public class WareCartService : IWareCartService
    {
        private readonly IRepository<WareCart> _wareCartRepository;

        private readonly IFileService _fileService;

        public WareCartService(
            IRepository<WareCart> wareCartRepository,
            IFileService fileService)
        {
            _wareCartRepository = wareCartRepository;
            _fileService = fileService;
        }

        public async Task CheckForWareDuplicateAsync(
            int cartId,
            int wareId)
        {
            var duplicate = await _wareCartRepository
                .SingleOrDefaultAsync(
                    new WareCartSpecification.GetByIds(cartId, wareId));

            if (duplicate != null)
            {
                throw new HttpException(
                    ErrorMessages.THE_WARE_IS_ALREADY_IN_THE_CART,
                    HttpStatusCode.BadRequest);
            }
        }

        public async Task CheckIfCartIsEmptyAsync(
            int cartId)
        {
            var waresCount = await _wareCartRepository.CountAsync(
                new WareCartSpecification.GetCartWares(cartId));

            if (waresCount == 0)
            {
                throw new HttpException(
                    ErrorMessages.THE_CART_IS_EMPTY,
                    HttpStatusCode.BadRequest);
            }
        }

        public async Task CreateAsync(
            int cartId,
            int wareId)
        {
            await _wareCartRepository.AddAsync(
                new WareCart
                {
                    WareId = wareId,
                    CartId = cartId
                });
        }

        public async Task DeleteAsync(
            int cartId,
            int wareId)
        {
            var wareCart = await _wareCartRepository
                .SingleOrDefaultAsync(
                    new WareCartSpecification.GetByIds(cartId, wareId));

            if (wareCart == null)
            {
                throw new HttpException(
                    ErrorMessages.THE_WARE_NOT_FOUND,
                    HttpStatusCode.BadRequest);
            }

            await _wareCartRepository.DeleteAsync(wareCart);
        }

        public async Task<PaginatedList<WareBriefInfoDTO>> GetPageByCartAsync(
            PaginationFilterDTO paginationFilterDTO,
            int cartId)
        {
            var waresCount = await _wareCartRepository
                .CountAsync(
                    new WareCartSpecification.GetCartWares(
                        cartId, paginationFilterDTO)
                    );

            if (waresCount == 0)
            {
                return null;
            }

            var totalPages = PaginatedList<WareBriefInfoDTO>
                .GetTotalPages(paginationFilterDTO, waresCount);

            var wareCarts = await _wareCartRepository
                .ListAsync(
                    new WareCartSpecification.GetCartWares(
                        cartId,
                        paginationFilterDTO)
                    );

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

        public async Task ReduceAvailableCountAsync(
            int cartId)
        {
            var wareCarts = await _wareCartRepository.ListAsync(
                new WareCartSpecification.GetCartWares(cartId));

            foreach (var wareCart in wareCarts)
            {
                wareCart.Ware.AvailableCount -= 1;
            }

            await _wareCartRepository.UpdateRangeAsync(wareCarts);
        }

        public async Task ReturnAvailableCountAsync(
            int cartId)
        {
            var wareCarts = await _wareCartRepository.ListAsync(
                new WareCartSpecification.GetCartWares(cartId));

            foreach (var wareCart in wareCarts)
            {
                wareCart.Ware.AvailableCount += 1;
            }

            await _wareCartRepository.UpdateRangeAsync(wareCarts);
        }
    }
}
