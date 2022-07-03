using Core.Entities;
using Core.Exceptions;
using Core.Interfaces.CustomService;
using Core.Resources;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Core.Services
{
    public class IdentityRoleService : IIdentityRoleService
    {
        private readonly UserManager<User> _userManager;

        public IdentityRoleService(
                UserManager<User> userManager
            )
        {
            _userManager = userManager;
        }

        public async Task<string> GetUserRoleAsync(User user)
        {
            var userRoles = await _userManager.GetRolesAsync(user);

            // will only work if there is an error in adding to the database
            if (userRoles.Count == 0)
            {
                throw new HttpException(
                                ErrorMessages.IdentityRoleNotFound,
                                HttpStatusCode.NotFound
                            );
            }

            return userRoles.First(); // because currently a user can only have one role
        }
    }
}
