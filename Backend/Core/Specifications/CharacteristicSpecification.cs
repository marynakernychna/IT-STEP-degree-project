using Ardalis.Specification;
using Core.Entities;

namespace Core.Specifications
{
    public static class CharacteristicSpecification
    {
        internal class GetByWareId : Specification<Characteristic>
        {
            public GetByWareId(int wareId)
            {
                Query.Where(c => c.WareId == wareId)
                      .AsNoTracking();
            }
        }
    }
}
