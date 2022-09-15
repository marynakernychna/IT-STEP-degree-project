using Core.Constants;
using Core.DTO.Authentication;
using Core.DTO.User;
using Core.Exceptions;
using Core.Interfaces.CustomService;
using Core.Resources;
using System.Net;
using System.Threading.Tasks;

namespace Core.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly ICartService _cartService;
        private readonly IEmailService _emailService;
        private readonly IIdentityRoleService _identityRoleService;
        private readonly ITokenService _tokenService;
        private readonly IUserService _userService;

        public AuthenticationService(
                ICartService cartService,
                IEmailService emailService,
                IIdentityRoleService identityRoleService,
                ITokenService tokenService,
                IUserService userService)
        {
            _identityRoleService = identityRoleService;
            _tokenService = tokenService;
            _emailService = emailService;
            _cartService = cartService;
            _userService = userService;
        }

        public async Task ChangePasswordAsync(
            ChangePasswordDTO changePasswordDTO,
            string userId)
        {
            await _userService.ChangePasswordAsync(
                changePasswordDTO.CurrentPassword,
                changePasswordDTO.NewPassword,
                userId);
        }

        public async Task<UserAutorizationDTO> LoginAsync(
            UserLoginDTO userLoginDTO)
        {
            var user = await _userService
                .GetByEmailAsync(userLoginDTO.Email);

            if (!await _userService
                    .CheckPasswordAsync(user, userLoginDTO.Password))
            {
                throw new HttpException(
                        ErrorMessages.INVALID_CREDENTIALS,
                        HttpStatusCode.BadRequest);
            }

            var userRole = await _identityRoleService.GetByUserAsync(user);

            return await _tokenService
                .GenerateForUserAsync(user, userRole);
        }

        public async Task LogoutAsync(
            UserLogoutDTO userLogoutDTO)
        {
            await _tokenService
                .DeleteRefreshTokenAsync(userLogoutDTO.RefreshToken);
        }

        public async Task RegisterClientAsync(
            UserRegistrationDTO userRegistrationDTO)
        {
            var client = await _userService.CreateAsync(
                userRegistrationDTO,
                IdentityRoleNames.Client.ToString());

            await _cartService.CreateAsync(client);
        }

        public async Task RegisterCourierAsync(
            UserRegistrationDTO userRegistrationDTO)
        {
            await _userService.CreateAsync(
                userRegistrationDTO,
                IdentityRoleNames.Courier.ToString());
        }

        public async Task ResetPasswordAsync(
            ResetPasswordDTO resetPasswordDTO)
        {
            await _userService.ResetPasswordAsync(
                resetPasswordDTO.Email,
                resetPasswordDTO.ConfirmationToken,
                resetPasswordDTO.NewPassword);
        }

        public async Task SendResetResetPasswordRequestAsync(
            string userEmail,
            string callbackUrl)
        {
            await _emailService.SendResetPasswordRequestAsync(
                await _userService.GetByEmailAsync(userEmail),
                callbackUrl);
        }
    }
}
