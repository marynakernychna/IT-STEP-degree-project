using Ardalis.Specification;
using Core.Entities;
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
        }
    }
}
