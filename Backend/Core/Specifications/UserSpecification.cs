using Ardalis.Specification;
using Core.Entities;
using System.Linq;

using System.Collections.Generic;

namespace Core.Specifications
{
    public static class UserSpecification
    {
        internal class GetByUsersIds : Specification<User>
        {
            public GetByUsersIds(List<string> usersIds)
            {
                Query.Where(u => usersIds.Contains(u.Id));
            }
          
            public GetByEmail(string email)
            {
                Query.Where(u => u.Email == email);
            }
        }
    }
}
