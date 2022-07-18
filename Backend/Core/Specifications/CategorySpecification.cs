using Ardalis.Specification;
using Core.DTO;
using Core.Entities;
using System.Linq;

namespace Core.Specifications
{
    public static class CategorySpecification
    {
        internal class GetByTitle : Specification<Category>, ISingleResultSpecification<Category>
        {
            public GetByTitle(string categoryTitle)
            {
                Query.Where(c => categoryTitle.Contains(c.Title));
            }
        }

        internal class GetAll : Specification<Category>
        {
            public GetAll(PaginationFilterDTO paginationFilter)
            {
                Query.Skip((paginationFilter.PageNumber - 1) * paginationFilter.PageSize)
                     .Take(paginationFilter.PageSize);
            }
        }
    }
}
