using Ardalis.Specification;
using Core.DTO;
using Core.Entities;
using System.Linq;

namespace Core.Specifications
{
    public static class WareSpecification
    {
        internal class GetByTitleAndCreatorId : Specification<Ware>,
                                                ISingleResultSpecification<Ware>
        {
            public GetByTitleAndCreatorId(string wareTitle, string creatorId)
            {
                Query.Where(w => w.Title == wareTitle &&
                            w.CreatorId == creatorId)
                     .AsNoTracking();
            }
        }

        internal class GetByCategory : Specification<Ware>
        {
            public GetByCategory(PaginationFilterDTO paginationFilter, string categoryTitle)
            {
                Query.Where(w => w.Category.Title == categoryTitle)
                     .Include(c => c.Category)
                     .OrderBy(c => c.Title)
                     .Skip((paginationFilter.PageNumber - 1) * paginationFilter.PageSize)
                     .Take(paginationFilter.PageSize)
                     .AsNoTracking();
            }
        }
    }
}
