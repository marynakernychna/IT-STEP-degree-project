using Ardalis.Specification;
using Core.DTO;
using Core.DTO.PaginationFilter;
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

        internal class GetAll : Specification<Ware>
        {
            public GetAll(PaginationFilterDTO paginationFilter)
            {
                Query.Include(w => w.Category)
                     .OrderBy(w => w.Title)
                     .Skip((paginationFilter.PageNumber - 1) * paginationFilter.PageSize)
                     .Take(paginationFilter.PageSize)
                     .AsNoTracking();
            }
        }

        internal class GetByCategory : Specification<Ware>
        {
            public GetByCategory(PaginationFilterWareDTO paginationFilter)
            {
                Query.Where(w => w.Category.Title == paginationFilter.CategoryTitle)
                     .Include(c => c.Category)
                     .OrderBy(w => w.Title)
                     .Skip((paginationFilter.PageNumber - 1) * paginationFilter.PageSize)
                     .Take(paginationFilter.PageSize)
                     .AsNoTracking();
            }
        }

        internal class GetById : Specification<Ware>,
                                 ISingleResultSpecification<Ware>
        {
            public GetById(int id)
            {
                Query.Where(w => w.Id == id)
                     .Include(w => w.Creator)
                     .Include(w => w.Category)
                     .Include(w => w.Characteristics)
                     .AsNoTracking();
            }
        }
    }
}
