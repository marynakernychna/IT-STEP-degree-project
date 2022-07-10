using Ardalis.Specification;
using Core.Entities;
using System.Linq;

namespace Core.Specifications
{
    public class UserSpecification
    {
        internal class GetByEmail : Specification<User>, ISingleResultSpecification<User>
        {
            public GetByEmail(string email)
            {
                Query.Where(u => u.Email == email);
            }
        }
    }
}
