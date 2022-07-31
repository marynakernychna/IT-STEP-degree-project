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
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<IdentityUserRole<string>> _identityUserRoleRepository;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        private readonly UserManager<User> _userManager;

        public UserService(
            RoleManager<IdentityRole> roleManager,
            IRepository<User> userRepository,
            IEmailService emailService,
            IRepository<IdentityUserRole<string>> identityUserRoleRepository,
            IMapper mapper,
            UserManager<User> userManager)
        {
            _roleManager = roleManager;
            _userRepository = userRepository;
            _identityUserRoleRepository = identityUserRoleRepository;
            _mapper = mapper;
            _emailService = emailService;
            _userManager = userManager;
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

        public async Task UserEditProfileInfoAsync(
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

        public async Task<string> GetUserIdByEmailAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            ExtensionMethods.UserNullCheck(user);

            return user.Id;
        }

        public async Task ChangePasswordAsync(ChangePasswordDTO changePasswordDTO, string userId)
        {
            if (changePasswordDTO.CurrentPassword == changePasswordDTO.NewPassword)
            {
                throw new HttpException(
                        ErrorMessages.NewInfoSamePrevious,
                        HttpStatusCode.BadRequest);
            }

            var user = await _userRepository.GetByIdAsync(userId);

            if (!await _userManager.CheckPasswordAsync(
                user,
                changePasswordDTO.CurrentPassword))
            {
                throw new HttpException(
                        ErrorMessages.InvalidPassword,
                        HttpStatusCode.BadRequest);
            }

            var result = await _userManager.ChangePasswordAsync(
                user,
                changePasswordDTO.CurrentPassword,
                changePasswordDTO.NewPassword);

            if (!result.Succeeded)
            {
                throw new HttpException(
                        ErrorMessages.ChangePasswordFailed,
                        HttpStatusCode.InternalServerError);
            }
        }
    }
}
