﻿using Ardalis.Specification;
using Core.DTO;
using Core.Entities;
using System.Linq;

namespace Core.Specifications
{
    public static class OrderSpecification
    {
        internal class GetAvailable : Specification<Order>
        {
            public GetAvailable(PaginationFilterDTO paginationFilterDTO)
            {
                Query.Where(o => o.CourierId == null)
                     .Include(o => o.Cart)
                     .ThenInclude(c => c.Creator)
                     .Include(o => o.Cart)
                     .ThenInclude(c => c.WareCarts)
                     .Skip((paginationFilterDTO.PageNumber - 1) * paginationFilterDTO.PageSize)
                     .Take(paginationFilterDTO.PageSize)
                     .AsNoTracking();
            }
        }

        internal class GetUndeliveredByClient : Specification<Order>
        {
            public GetUndeliveredByClient(string userId, PaginationFilterDTO paginationFilterDTO)
            {
                Query.Where(o => o.Cart.CreatorId == userId)
                     .Where(o => !o.IsAcceptedByClient || !o.IsAcceptedByCourier)
                     .Include(o => o.Cart)
                     .ThenInclude(c => c.Creator)
                     .Include(o => o.Cart)
                     .ThenInclude(c => c.WareCarts)
                     .Skip((paginationFilterDTO.PageNumber - 1) * paginationFilterDTO.PageSize)
                     .Take(paginationFilterDTO.PageSize)
                     .AsNoTracking();
            }
        }

        internal class GetByCourier : Specification<Order>,
                                      ISingleResultSpecification<Order>
        {
            public GetByCourier(int orderId, string courierId)
            {
                Query.Where(o => o.Id == orderId && o.CourierId == courierId)
                     .AsNoTracking();
            }
        }

        internal class GetUndeliveredByCourier : Specification<Order>
        {
            public GetUndeliveredByCourier(
                string courierId, PaginationFilterDTO paginationFilterDTO)
            {
                Query.Where(o => o.CourierId == courierId)
                     .Where(o => !o.IsAcceptedByClient || !o.IsAcceptedByCourier)
                     .Include(o => o.Cart)
                     .ThenInclude(c => c.Creator)
                     .Include(o => o.Cart)
                     .ThenInclude(c => c.WareCarts)
                     .Skip((paginationFilterDTO.PageNumber - 1) * paginationFilterDTO.PageSize)
                     .Take(paginationFilterDTO.PageSize)
                     .AsNoTracking();
            }
        }

        internal class GetByCreatorIdAndId : Specification<Order>,
                                             ISingleResultSpecification<Order>
        {
            public GetByCreatorIdAndId(string creatorId, int orderId)
            {
                Query.Where(o => o.Id == orderId && o.Cart.CreatorId == creatorId)
                     .AsNoTracking();
            }
        }

        internal class GetByCourierIdAndId : Specification<Order>,
                                             ISingleResultSpecification<Order>
        {
            public GetByCourierIdAndId(string courierId, int orderId)
            {
                Query.Where(o => o.Id == orderId && o.CourierId == courierId)
                     .AsNoTracking();
            }
        }

        internal class GetClientDeliveredOrders : Specification<Order>
        {
            public GetClientDeliveredOrders(string userId, PaginationFilterDTO paginationFilterDTO)
            {
                Query.Where(o => o.Cart.CreatorId == userId
                            && o.IsAcceptedByClient
                            && o.IsAcceptedByCourier)
                     .Include(o => o.Cart)
                     .ThenInclude(c => c.Creator)
                     .Include(o => o.Cart)
                     .ThenInclude(c => c.WareCarts)
                     .Skip((paginationFilterDTO.PageNumber - 1) * paginationFilterDTO.PageSize)
                     .Take(paginationFilterDTO.PageSize)
                     .AsNoTracking();
            }
        }

        internal class GetCourierDeliveredOrders : Specification<Order>
        {
            public GetCourierDeliveredOrders(
                string courierId, PaginationFilterDTO paginationFilterDTO)
            {
                Query.Where(o => o.CourierId == courierId
                            && o.IsAcceptedByClient
                            && o.IsAcceptedByCourier)
                     .Include(o => o.Cart)
                     .ThenInclude(c => c.Creator)
                     .Include(o => o.Cart)
                     .ThenInclude(c => c.WareCarts)
                     .Skip((paginationFilterDTO.PageNumber - 1) * paginationFilterDTO.PageSize)
                     .Take(paginationFilterDTO.PageSize)
                     .AsNoTracking();
            }
        }
    }
}
