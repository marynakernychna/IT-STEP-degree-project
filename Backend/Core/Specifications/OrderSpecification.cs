using Ardalis.Specification;
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

        internal class GetByUser : Specification<Order>
        {
            public GetByUser(string userId, PaginationFilterDTO paginationFilterDTO)
            {
                Query.Where(o => o.Cart.CreatorId == userId)
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

        internal class GetListByCourier : Specification<Order>
        {
            public GetListByCourier(
                string courierId, PaginationFilterDTO paginationFilterDTO)
            {
                Query.Where(o => o.CourierId == courierId)
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

        internal class GetByClientConfirmedDelivery : Specification<Order>,
                                             ISingleResultSpecification<Order>
        {
            public GetByClientConfirmedDelivery(string creatorId, int orderId)
            {
                Query.Where(o => o.Id == orderId
                            && o.Cart.CreatorId == creatorId
                            && o.IsAcceptedByClient == true)
                     .AsNoTracking();
            }
        }

        internal class GetByCourierConfirmedDelivery : Specification<Order>,
                                             ISingleResultSpecification<Order>
        {
            public GetByCourierConfirmedDelivery(string courierId, int orderId)
            {
                Query.Where(o => o.Id == orderId
                            && o.CourierId == courierId
                            && o.IsAcceptedByCourier == true)
                     .AsNoTracking();
            }
        }
    }
}
