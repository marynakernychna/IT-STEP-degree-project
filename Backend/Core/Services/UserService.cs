using AutoMapper;
using Core.Constants;
using Core.DTO;
using Core.DTO.User;
using Core.Entities;
using Core.Exceptions;
using Core.Helpers;
using Core.Interfaces;
using Core.Interfaces.CustomService;
using Core.Resources;
using System.Net;
using Core.Specifications;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Core.Services
{
    public class UserService : IUserService
    {
        private readonly RoleManager<IdentityRole> _identityRoleManager;
        private readonly UserManager<User> _userManager;

        private readonly IRepository<IdentityUserRole<string>> _identityUserRoleRepository;
        private readonly IRepository<User> _userRepository;

        private readonly IMapper _mapper;

        private readonly IEmailService _emailService;

        public UserService(
            RoleManager<IdentityRole> identityRoleManager,
            UserManager<User> userManager,
            IRepository<IdentityUserRole<string>> identityUserRoleRepository,
            IRepository<User> userRepository,
            IMapper mapper,
            IEmailService emailService)
        {
            _identityRoleManager = identityRoleManager;
            _userManager = userManager;
            _identityUserRoleRepository = identityUserRoleRepository;
            _userRepository = userRepository;
            _mapper = mapper;
            _emailService = emailService;
        }

        public static string GetCurrentUserIdentifier(
            ClaimsPrincipal currentUser)
        {
            return currentUser.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        public async Task<string> GetIdByEmailAsync(
            string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            ExtensionMethods.UserNullCheck(user);

            return user.Id;
        }

        public async Task<PaginatedList<UserProfileInfoDTO>> GetPageOfClientsAsync(
            PaginationFilterDTO paginationFilter)
        {
            var role = await _identityRoleManager.FindByNameAsync(IdentityRoleNames.Client.ToString());

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

        public async Task<UserProfileInfoDTO> GetProfileAsync(
            string userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);

            ExtensionMethods.UserNullCheck(user);

            return _mapper.Map<UserProfileInfoDTO>(user);
        }

        public async Task UpdateProfileAsync(
            UserEditProfileInfoDTO newUserInfo, string userId, string callbackUrl)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            ExtensionMethods.UserNullCheck(user);

            if (user.Name == newUserInfo.Name &&
                user.Surname == newUserInfo.Surname &&
                user.PhoneNumber == newUserInfo.PhoneNumber &&
                user.Email == newUserInfo.Email)
            {
                throw new HttpException(
                    ErrorMessages.NewInfoSamePrevious,
                    HttpStatusCode.BadRequest);
            }

            user.Name = newUserInfo.Name;
            user.Surname = newUserInfo.Surname;
            user.PhoneNumber = newUserInfo.PhoneNumber;

            if (!newUserInfo.Email.Equals(user.Email))
            {
                if (await _userRepository.AnyAsync(
                    new UserSpecification.GetByEmail(newUserInfo.Email)))
                {
                    throw new HttpException(
                        ErrorMessages.FailedSendEmail,
                        HttpStatusCode.BadRequest);
                }

                user.Email = newUserInfo.Email;
                user.UserName = newUserInfo.Email;
                user.NormalizedEmail = newUserInfo.Email.ToUpper();
                user.NormalizedUserName = newUserInfo.Email.ToUpper();
                user.EmailConfirmed = false;

                await _emailService.SendConfirmationEmailAsync(user, callbackUrl);
            }

            await _userRepository.UpdateAsync(user);
        }
    }
}
