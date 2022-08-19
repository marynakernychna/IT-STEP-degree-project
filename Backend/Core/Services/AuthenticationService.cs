using AutoMapper;
using Core.Constants;
using Core.DTO.Authentication;
using Core.DTO.User;
using Core.Entities;
using Core.Exceptions;
using Core.Helpers;
using Core.Interfaces;
using Core.Interfaces.CustomService;
using Core.Resources;
using Core.Specifications;
using Microsoft.AspNetCore.Identity;
using System.Net;
using System.Threading.Tasks;

namespace Core.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IRepository<User> _userRepository;
        private readonly IIdentityRoleService _identityRoleService;
        private readonly ITokenService _tokenService;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;
        private readonly IRepository<RefreshToken> _refreshTokenRepository;
        private readonly IEmailService _emailService;

        public AuthenticationService(
                IRepository<User> userRepository,
                IIdentityRoleService identityRoleService,
                ITokenService tokenService,
                UserManager<User> userManager,
                RoleManager<IdentityRole> roleManager,
                IMapper mapper,
                IRepository<RefreshToken> refreshTokenRepository,
                IEmailService emailService
            )
        {
            _userRepository = userRepository;
            _identityRoleService = identityRoleService;
            _tokenService = tokenService;
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
            _refreshTokenRepository = refreshTokenRepository;
            _emailService = emailService;
        }

        public async Task<UserAutorizationDTO> LoginAsync(UserLoginDTO userLoginDTO)
        {
            var user = await _userManager.FindByEmailAsync(userLoginDTO.Email);

            if (user == null ||
                !await _userManager.CheckPasswordAsync(user, userLoginDTO.Password))
            {
                throw new HttpException(
                        ErrorMessages.InvalidCredentials,
                        HttpStatusCode.Unauthorized
                    );
            }

            var userRole = await _identityRoleService.GetUserRoleAsync(user);

            return await _tokenService.GenerateUserTokens(user, userRole);
        }

        public async Task RegisterAsync(UserRegistrationDTO userRegistrationDTO)
        {
            var isAlreadyExists = await _userRepository.AnyAsync(
                new UserSpecification.GetByEmail(userRegistrationDTO.Email));

            if (isAlreadyExists)
            {
                throw new HttpException(
                        ErrorMessages.EmailAlreadyExists,
                        HttpStatusCode.BadRequest
                    );
            }

            var user = _mapper.Map<User>(userRegistrationDTO);
            var createUserResult = await _userManager
                                            .CreateAsync(user, userRegistrationDTO.Password);

            ExtensionMethods.CheckIdentityResultNullCheck(createUserResult);

            var roleName = IdentityRoleNames.User.ToString();
            var userRole = await _roleManager.FindByNameAsync(roleName);

            ExtensionMethods.IdentityRoleNullCheck(userRole);

            var addToRoleResult = await _userManager.AddToRoleAsync(user, userRole.Name);

            ExtensionMethods.CheckIdentityResultNullCheck(addToRoleResult);
        }

        public async Task LogoutAsync(UserLogoutDTO userLogoutDTO)
        {
            var refreshToken = await _refreshTokenRepository.GetBySpecAsync(
                new RefreshTokenSpecification.GetByToken(userLogoutDTO.RefreshToken));

            ExtensionMethods.RefreshTokenNullCheck(refreshToken);

            await _refreshTokenRepository.DeleteAsync(refreshToken);
        }

        public async Task SendConfirmResetPasswordEmail(string email, string callbackUrl)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                throw new HttpException(
                        ErrorMessages.UserNotFound,
                        HttpStatusCode.BadRequest);
            }

            await _emailService.SendConfirmationResetPasswordEmailAsync(user, callbackUrl);
        }

        public async Task ResetPasswordAsync(ResetPasswordDTO resetPasswordDTO)
        {
            var user = await _userManager.FindByEmailAsync(resetPasswordDTO.Email);

            if (await _userManager.CheckPasswordAsync(user, resetPasswordDTO.NewPassword))
            {
                throw new HttpException(
                        ErrorMessages.NewInfoSamePrevious,
                        HttpStatusCode.BadRequest);
            }

            var result = await _userManager.ResetPasswordAsync(
                user,
                resetPasswordDTO.Token,
                resetPasswordDTO.NewPassword);

            if (!result.Succeeded)
            {
                throw new HttpException(
                        ErrorMessages.ChangePasswordFailed,
                        HttpStatusCode.InternalServerError);
            }
        }
    }
}
