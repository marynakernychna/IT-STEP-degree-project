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
    }
}
