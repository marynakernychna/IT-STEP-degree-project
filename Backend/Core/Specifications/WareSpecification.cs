using Ardalis.Specification;
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
    }
}
