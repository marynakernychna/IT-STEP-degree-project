using Ardalis.Specification;
using Core.Constants;
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
                Query.Where(c => c.Title == categoryTitle &&
                            c.Title != Constants.Constants.NO_CATEGORY_TITLE)
                     .AsNoTracking();
            }
        }

        internal class GetAll : Specification<Category>
        {
            public GetAll(PaginationFilterDTO paginationFilter)
            {
                Query.Where(c => c.Title != Constants.Constants.NO_CATEGORY_TITLE)
                     .Include(c => c.Wares)
                     .OrderBy(c => c.Title)
                     .Skip((paginationFilter.PageNumber - 1) * paginationFilter.PageSize)
                     .Take(paginationFilter.PageSize)
                     .AsNoTracking();
            }

            public GetAll()
            {
                Query.Where(c => c.Title != Constants.Constants.NO_CATEGORY_TITLE)
                     .OrderBy(c => c.Title)
                     .AsNoTracking();
            }
        }
    }
}
