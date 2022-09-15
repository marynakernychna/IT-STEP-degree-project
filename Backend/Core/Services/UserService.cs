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
using Core.DTO.Authentication;

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

        public async Task ChangePasswordAsync(
            string currentPassword,
            string newPassword,
            string userId)
        {
            var user = await GetByIdAsync(userId);

            var identityResult = await _userManager
                .ChangePasswordAsync(
                    user,
                    currentPassword,
                    newPassword
                 );

            if (!identityResult.Succeeded)
            {
                throw new HttpException(
                        ErrorMessages.CHANGE_PASSWORD_FAILED,
                        HttpStatusCode.InternalServerError);
            }
        }

        public async Task<bool> CheckIfExistsByEmailAsync(
            string email)
        {
            return await _userRepository.AnyAsync(
                new UserSpecification.GetByEmail(email));
        }

        public async Task CheckIfExistsByIdAsync(
            string userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);

            ExtensionMethods.UserNullCheck(user);
        }

        public async Task<bool> CheckPasswordAsync(
            User user, string password)
        {
            return await _userManager
                .CheckPasswordAsync(user, password);
        }

        public async Task<User> CreateAsync(
            UserRegistrationDTO userRegistrationDTO,
            string roleName)
        {
            var isEmailBusy = await CheckIfExistsByEmailAsync(
                userRegistrationDTO.Email);

            if (isEmailBusy)
            {
                throw new HttpException(
                        ErrorMessages.THE_EMAIL_ALREADY_EXISTS,
                        HttpStatusCode.BadRequest);
            }

            var user = _mapper.Map<User>(userRegistrationDTO);

            var createUserResult = await _userManager
                .CreateAsync(user, userRegistrationDTO.Password);

            ExtensionMethods.CheckIdentityResultNullCheck(createUserResult);

            var userRole = await _identityRoleManager
                .FindByNameAsync(roleName);

            ExtensionMethods.IdentityRoleNullCheck(userRole);

            var addToRoleResult = await _userManager
                .AddToRoleAsync(user, userRole.Name);

            ExtensionMethods.CheckIdentityResultNullCheck(addToRoleResult);

            return user;
        }

        public async Task<User> GetByEmailAsync(
            string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        public async Task<User> GetByIdAsync(
            string userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);

            ExtensionMethods.UserNullCheck(user);

            return user;
        }

        public static string GetCurrentUserIdentifier(
            ClaimsPrincipal currentUser)
        {
            return currentUser.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        public async Task<string> GetIdByEmailAsync(
            string email)
        {
            var user = await GetByEmailAsync(email);

            ExtensionMethods.UserNullCheck(user);

            return user.Id;
        }

        public async Task<PaginatedList<UserProfileInfoDTO>> GetPageOfClientsAsync(
            PaginationFilterDTO paginationFilter)
        {
            var role = await _identityRoleManager.FindByNameAsync(IdentityRoleNames.Client.ToString());

            ExtensionMethods.IdentityRoleNullCheck(role);

            var clients = await GetUsersAsync(paginationFilter, role);

            return clients;
        }

        public async Task<PaginatedList<UserProfileInfoDTO>> GetPageOfCouriersAsync(
            PaginationFilterDTO paginationFilter)
        {
            var role = await _identityRoleManager.FindByNameAsync(IdentityRoleNames.Courier.ToString());

            ExtensionMethods.IdentityRoleNullCheck(role);

            var couriers = await GetUsersAsync(paginationFilter, role);

            return couriers;
        }

        public async Task<UserProfileInfoDTO> GetProfileAsync(
            string userId)
        {
            var user = await GetByIdAsync(userId);

            return _mapper.Map<UserProfileInfoDTO>(user);
        }

        public async Task ResetPasswordAsync(
            string userEmail, string resetToken, string newPassword)
        {
            var user = await GetByEmailAsync(userEmail);

            if (await CheckPasswordAsync(user, newPassword))
            {
                throw new HttpException(
                        ErrorMessages.THE_NEW_INFO_IS_THE_SAME_AS_PREVIOUS,
                        HttpStatusCode.BadRequest);
            }

            if (!await _userManager.VerifyUserTokenAsync(
                user,
                _userManager.Options.Tokens.PasswordResetTokenProvider,
                "ResetPassword",
                resetToken))
            {
                throw new HttpException(
                        ErrorMessages.INVALID_TOKEN,
                        HttpStatusCode.BadRequest);
            }

            var identityResult = await _userManager.ResetPasswordAsync(
                user,
                resetToken,
                newPassword);

            ExtensionMethods.CheckIdentityResultNullCheck(identityResult);
        }

        public async Task UpdateProfileAsync(
            UserEditProfileInfoDTO newUserInfo, string userId, string callbackUrl)
        {
            var user = await GetByIdAsync(userId);

            if (user.Name == newUserInfo.Name &&
                user.Surname == newUserInfo.Surname &&
                user.PhoneNumber == newUserInfo.PhoneNumber &&
                user.Email == newUserInfo.Email)
            {
                throw new HttpException(
                    ErrorMessages.THE_NEW_INFO_IS_THE_SAME_AS_PREVIOUS,
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
                        ErrorMessages.THE_MAIL_SENDING_ERROR,
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

        private async Task<PaginatedList<UserProfileInfoDTO>> GetUsersAsync(
            PaginationFilterDTO paginationFilter, IdentityRole role)
        {
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
