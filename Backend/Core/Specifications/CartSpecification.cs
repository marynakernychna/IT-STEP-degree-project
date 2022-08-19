using Ardalis.Specification;
using Core.Entities;
using System.Linq;

namespace Core.Specifications
{
    public static class CartSpecification
    {
        internal class GetByCreatorId : Specification<Cart>,
                                        ISingleResultSpecification<Cart>
        {
            public GetByCreatorId(string creatorId)
            {
                Query.Where(c => c.CreatorId == creatorId &&
                            c.OrderId == null)
                     .AsNoTracking();
            }
        }
    }
}
