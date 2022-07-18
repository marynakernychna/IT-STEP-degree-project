using Ardalis.Specification;
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
    }
}
