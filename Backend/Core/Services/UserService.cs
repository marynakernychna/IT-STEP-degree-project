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

        public async Task UserEditProfileInfoAsync(
            UserEditProfileInfoDTO newUserInfo, string userId, string callbackUrl)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            ExtensionMethods.UserNullCheck(user);

            if (user.Name == newUserInfo.Name &&
                user.Surname == newUserInfo.Surname &&
                user.PhoneNumber == newUserInfo.PhoneNumber &&
                user.Email == newUserInfo.Email )
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
