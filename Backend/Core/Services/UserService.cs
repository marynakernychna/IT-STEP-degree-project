using AutoMapper;
using Core.DTO;
using Core.DTO.User;
using Core.Entities;
using Core.Exceptions;
using Core.Helpers;
using Core.Interfaces;
using Core.Interfaces.CustomService;
using Core.Resources;
using Core.Specifications;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Core.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _userRepository;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;

        public UserService(
            IRepository<User> userRepository,
            IMapper mapper,
            IEmailService emailService)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _emailService = emailService;
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

        public async Task UserEditProfileInfoAsync(UserEditProfileInfoDTO userEditProfileInfo, string userId, string callbackUrl)
        {
            var updateUser = await _userRepository.GetByIdAsync(userId);

            ExtensionMethods.UserNullCheck(updateUser);

            updateUser.Name = userEditProfileInfo.Name;
            updateUser.Surname = userEditProfileInfo.Surname;

            if (!userEditProfileInfo.Email.Equals(updateUser.Email))
            {
                if (await _userRepository.AnyAsync(new UserSpecification.GetByEmail(userEditProfileInfo.Email)))
                {
                    throw new HttpException(ErrorMessages.FailedSendEmail, HttpStatusCode.BadRequest);
                }

                updateUser.Email = userEditProfileInfo.Email;
                updateUser.UserName = userEditProfileInfo.Email;
                updateUser.NormalizedEmail = userEditProfileInfo.Email.ToUpper();
                updateUser.NormalizedUserName = userEditProfileInfo.Email.ToUpper();
                updateUser.EmailConfirmed = false;

                await _emailService.SendConfirmationEmailAsync(updateUser, callbackUrl);
            }

            await _userRepository.UpdateAsync(updateUser);
        }
    }
}
