using AutoMapper;
using Core.Constants;
using Core.DTO;
using Core.DTO.User;
using Core.Entities;
using Core.Helpers;
using Core.Interfaces;
using Core.Interfaces.CustomService;
using Core.Specifications;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Core.Services
{
    public class UserService : IUserService
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<IdentityUserRole<string>> _identityUserRoleRepository;
        private readonly IMapper _mapper;

        public UserService(
            RoleManager<IdentityRole> roleManager,
            IRepository<User> userRepository,
            IRepository<IdentityUserRole<string>> identityUserRoleRepository,
            IMapper mapper)
        {
            _roleManager = roleManager;
            _userRepository = userRepository;
            _identityUserRoleRepository = identityUserRoleRepository;
            _mapper = mapper;
        }

        public string GetCurrentUserNameIdentifier(ClaimsPrincipal currentUser)
        {
            return currentUser.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        public async Task<UserProfileInfoDTO> GetUserProfileInfoAsync(string userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);

            ExtensionMethods.UserNullCheck(user);

            return _mapper.Map<UserProfileInfoDTO>(user);
        }

        public async Task<PaginatedList<UserProfileInfoDTO>> GetUsersProfileInfoAsync(
            PaginationFilterDTO paginationFilter)
        {
            var role = await _roleManager.FindByNameAsync(IdentityRoleNames.User.ToString());

            ExtensionMethods.IdentityRoleNullCheck(role);

            var userRoles = await _identityUserRoleRepository.ListAsync(
                new UserRoleSpecification.GetByUsersByRoleId(paginationFilter, role.Id));

            var userIds = new List<string>();

            foreach (var userRole in userRoles)
            {
                userIds.Add(userRole.UserId);
            }

            var usersCount = await _identityUserRoleRepository.CountAsync(
                new UserRoleSpecification.GetByUsersByRoleId(paginationFilter, role.Id));

            int totalPages = PaginatedList<UserProfileInfoDTO>
                .GetTotalPages(paginationFilter, usersCount);

            if (totalPages == 0)
            {
                return null;
            }

            var users = await _userRepository.ListAsync(
                new UserSpecification.GetByUsersIds(userIds));

            return PaginatedList<UserProfileInfoDTO>.Evaluate(
                _mapper.Map<List<UserProfileInfoDTO>>(users),
                paginationFilter.PageNumber,
                usersCount,
                totalPages);
        }
    }
}
