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

        private readonly IUserService _userService;

        public IdentityRoleService(
                UserManager<User> userManager,
                IUserService userService)
        {
            _userManager = userManager;
            _userService = userService;
        }

        public async Task<string> GetByUserAsync(
            User user)
        {
            var userRoles = await _userManager.GetRolesAsync(user);

            // because according to the logic of the project,
            // a user can have only one role

            if (userRoles.Count != 1)
            {
                throw new HttpException(
                    ErrorMessages.ROLE_FIND_ERROR,
                    HttpStatusCode.InternalServerError);
            }

            return userRoles.First();
        }

        public async Task<string> GetByUserIdAsync(
            string userId)
        {
            return await GetByUserAsync(
                await _userService.GetByIdAsync(userId));
        }
    }
}
