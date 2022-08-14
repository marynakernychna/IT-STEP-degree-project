using Ardalis.Specification;
using Core.DTO;
using Core.Entities;
using System.Linq;

namespace Core.Specifications
{
    public static class CategorySpecification
    {
        internal class GetByTitle : Specification<Category>,
                                    ISingleResultSpecification<Category>
        {
            public GetByTitle(string categoryTitle)
            {
                Query.Where(c => c.Title == categoryTitle)
                     .AsNoTracking();
            }
        }

        internal class GetAll : Specification<Category>
        {
            public GetAll(PaginationFilterDTO paginationFilter)
            {
                Query.Include(c => c.Wares)
                     .OrderBy(c => c.Title)
                     .Skip((paginationFilter.PageNumber - 1) * paginationFilter.PageSize)
                     .Take(paginationFilter.PageSize)
                     .AsNoTracking();
            }

            public GetAll()
            {
                Query.OrderBy(c => c.Title)
                     .AsNoTracking();
            }
        }
    }
}
