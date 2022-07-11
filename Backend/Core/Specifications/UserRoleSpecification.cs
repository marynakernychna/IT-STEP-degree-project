using Ardalis.Specification;
using Core.DTO;
using Microsoft.AspNetCore.Identity;

namespace Core.Specifications
{
    public static class UserRoleSpecification
    {
        internal class GetByUsersByRoleId : Specification<IdentityUserRole<string>>
        {
            public GetByUsersByRoleId(PaginationFilterDTO paginationFilter, string roleId)
            {
                Query.Where(ur => ur.RoleId == roleId)
                     .Skip((paginationFilter.PageNumber - 1) * paginationFilter.PageSize)
                     .Take(paginationFilter.PageSize);
            }
        }
    }
}
