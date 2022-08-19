using Ardalis.Specification;
using Core.DTO;
using Core.Entities;
using System.Linq;

namespace Core.Specifications
{
    public static class WareCartSpecification
    {
        internal class GetCartWares : Specification<WareCart>
        {
            public GetCartWares(int cartId, PaginationFilterDTO paginationFilterDTO)
            {
                Query.Where(wc => wc.CartId == cartId)
                     .Include(wc => wc.Ware)
                     .ThenInclude(w => w.Category)
                     .Skip((paginationFilterDTO.PageNumber - 1) * paginationFilterDTO.PageSize)
                     .Take(paginationFilterDTO.PageSize)
                     .AsNoTracking();
            }
        }
    }
}
