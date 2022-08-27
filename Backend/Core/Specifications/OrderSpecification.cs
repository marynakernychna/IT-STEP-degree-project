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
    }
}
