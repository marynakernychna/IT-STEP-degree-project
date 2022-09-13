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
        private readonly RoleManager<IdentityRole> _identityRoleManager;
        private readonly UserManager<User> _userManager;

        private readonly IRepository<RefreshToken> _refreshTokenRepository;
        private readonly IRepository<User> _userRepository;

        private readonly IMapper _mapper;

        private readonly ICartService _cartService;
        private readonly IEmailService _emailService;
        private readonly IIdentityRoleService _identityRoleService;
        private readonly ITokenService _tokenService;

        public AuthenticationService(
                RoleManager<IdentityRole> identityRoleManager,
                UserManager<User> userManager,
                IRepository<RefreshToken> refreshTokenRepository,
                IRepository<User> userRepository,
                IMapper mapper,
                ICartService cartService,
                IEmailService emailService,
                IIdentityRoleService identityRoleService,
                ITokenService tokenService)
        {
            _userRepository = userRepository;
            _identityRoleService = identityRoleService;
            _tokenService = tokenService;
            _userManager = userManager;
            _identityRoleManager = identityRoleManager;
            _mapper = mapper;
            _refreshTokenRepository = refreshTokenRepository;
            _emailService = emailService;
            _cartService = cartService;
        }

        /// <summary>
        ///     Changes the user's password.
        /// </summary>
        /// <param name="changePasswordDTO">
        ///     DTO with two fields: current and new password.
        /// </param>
        /// <returns>
        ///     The System.Threading.Tasks.Task that represents the asynchronous operation.
        /// </returns>
        /// <exception cref="HttpException">
        ///     Returns InternalServerError if the password change failed.
        /// </exception>
        public async Task ChangePasswordAsync(
            ChangePasswordDTO changePasswordDTO, string userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);

            var identityResult = await _userManager
                .ChangePasswordAsync(
                    user,
                    changePasswordDTO.CurrentPassword,
                    changePasswordDTO.NewPassword
                 );

            if (!identityResult.Succeeded)
            {
                throw new HttpException(
                        ErrorMessages.CHANGE_PASSWORD_FAILED,
                        HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        ///     Gives a user access to the system.
        /// </summary>
        /// <param name="userLoginDTO">
        ///     DTO with two fields: email and password.
        /// </param>
        /// <returns>
        ///     The System.Threading.Tasks.Task that represents the asynchronous operation and
        ///     DTO with two tokens: access and refresh.
        /// </returns>
        /// <exception cref="HttpException">
        ///     Returns BadRequest if credetials are invalid.
        /// </exception>
        public async Task<UserAutorizationDTO> LoginAsync(
            UserLoginDTO userLoginDTO)
        {
            var user = await _userManager.FindByEmailAsync(userLoginDTO.Email);

            if (user == null ||
                !await _userManager.CheckPasswordAsync(user, userLoginDTO.Password))
            {
                throw new HttpException(
                        ErrorMessages.INVALID_CREDENTIALS,
                        HttpStatusCode.BadRequest);
            }

            var userRole = await _identityRoleService.GetByUserAsync(user);

            return await _tokenService.GenerateForUserAsync(user, userRole);
        }

        /// <summary>
        ///     Removes access from the user to the system.
        /// </summary>
        /// <param name="userLogoutDTO">
        ///     DTO with refresh token.
        /// </param>
        /// <returns>
        ///     The System.Threading.Tasks.Task that represents the asynchronous operation.
        /// </returns>
        public async Task LogoutAsync(
            UserLogoutDTO userLogoutDTO)
        {
            var refreshToken = await _refreshTokenRepository.SingleOrDefaultAsync(
                new RefreshTokenSpecification.GetByToken(userLogoutDTO.RefreshToken));

            ExtensionMethods.RefreshTokenNullCheck(refreshToken);

            await _refreshTokenRepository.DeleteAsync(refreshToken);
        }

        /// <summary>
        ///     Register user.
        /// </summary>
        /// <param name="userRegistrationDTO">
        ///     DTO with the user's: name, surname, phone number, email and password.
        ///  </param>
        /// <returns>
        ///     The System.Threading.Tasks.Task that represents the asynchronous operation.
        /// </returns>
        /// <exception cref="HttpException">
        ///     Returns BadRequest if there is already a user with such email.
        /// </exception>
        public async Task RegisterAsync(
            UserRegistrationDTO userRegistrationDTO)
        {
            var isEmailBusy = await _userRepository.AnyAsync(
                new UserSpecification.GetByEmail(userRegistrationDTO.Email));

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
                .FindByNameAsync(IdentityRoleNames.Client.ToString());

            ExtensionMethods.IdentityRoleNullCheck(userRole);

            var addToRoleResult = await _userManager.AddToRoleAsync(user, userRole.Name);

            ExtensionMethods.CheckIdentityResultNullCheck(addToRoleResult);

            await _cartService.CreateAsync(user);
        }

        /// <summary>
        ///     Reset user password.
        /// </summary>
        /// <param name="resetPasswordDTO">
        ///     DTO with email, confirmation token and new password.
        /// </param>
        /// <returns>
        ///     The System.Threading.Tasks.Task that represents the asynchronous operation.
        /// </returns>
        /// <exception cref="HttpException">
        ///     Returns BadRequest if the information matches and the token is invalid.
        /// </exception>
        public async Task ResetPasswordAsync(
            ResetPasswordDTO resetPasswordDTO)
        {
            var user = await _userManager.FindByEmailAsync(resetPasswordDTO.Email);

            if (await _userManager.CheckPasswordAsync(user, resetPasswordDTO.NewPassword))
            {
                throw new HttpException(
                        ErrorMessages.THE_NEW_INFO_IS_THE_SAME_AS_PREVIOUS,
                        HttpStatusCode.BadRequest);
            }

            if (!await _userManager.VerifyUserTokenAsync(
                user,
                _userManager.Options.Tokens.PasswordResetTokenProvider,
                "ResetPassword",
                resetPasswordDTO.ConfirmationToken))
            {
                throw new HttpException(
                        ErrorMessages.INVALID_TOKEN,
                        HttpStatusCode.BadRequest);
            }

            var result = await _userManager.ResetPasswordAsync(
                user,
                resetPasswordDTO.ConfirmationToken,
                resetPasswordDTO.NewPassword);

            ExtensionMethods.CheckIdentityResultNullCheck(result);
        }

        /// <summary>
        ///     Sends a message to change the password.
        /// </summary>
        /// <param name="callbackUrl">
        ///     Link to client side.
        /// </param>
        /// <returns>
        ///     The System.Threading.Tasks.Task that represents the asynchronous operation.
        /// </returns>
        public async Task SendResetResetPasswordRequestAsync(
            string userEmail, string callbackUrl)
        {
            var user = await _userManager.FindByEmailAsync(userEmail);

            ExtensionMethods.UserNullCheck(user);

            await _emailService.SendResetPasswordRequestAsync(user, callbackUrl);
        }
    }
}
