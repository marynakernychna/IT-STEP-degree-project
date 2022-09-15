using Core.Constants;
using Core.DTO.Authentication;
using Core.DTO.User;
using Core.Helpers;
using Core.Interfaces.CustomService;
using Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(
                IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost("clients/register")]
        public async Task<IActionResult> RegisterClientAsync(
            [FromBody] UserRegistrationDTO userRegistrationDTO)
        {
            await _authenticationService
                .RegisterClientAsync(userRegistrationDTO);

            return Ok();
        }

        [HttpPost("couriers/register")]
        [AuthorizeByRole(IdentityRoleNames.Admin)]
        public async Task<IActionResult> RegisterCourierAsync(
            [FromBody] UserRegistrationDTO userRegistrationDTO)
        {
            await _authenticationService
                .RegisterCourierAsync(userRegistrationDTO);

            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync(
            [FromBody] UserLoginDTO data)
        {
            return Ok(await _authenticationService.LoginAsync(data));
        }

        [HttpPost("logout")]
        [AuthorizeByRole(
            IdentityRoleNames.Admin,
            IdentityRoleNames.Client,
            IdentityRoleNames.Courier)]
        public async Task<IActionResult> LogoutAsync(
            [FromBody] UserLogoutDTO userLogoutDTO)
        {
            await _authenticationService
                .LogoutAsync(userLogoutDTO);

            return Ok();
        }

        [HttpPost("reset-password-request")]
        public async Task<IActionResult> SendResetPasswordRequestAsync(
            [FromBody] ConfirmationResetPasswordDTO confirmationResetPasswordDTO)
        {
            await _authenticationService
                .SendResetResetPasswordRequestAsync(
                    confirmationResetPasswordDTO.Email,
                    Request.GetTypedHeaders().Referer.ToString()
                );

            return Ok();
        }

        [HttpPut("change-password")]
        [AuthorizeByRole(
            IdentityRoleNames.Client,
            IdentityRoleNames.Courier,
            IdentityRoleNames.Admin)]
        public async Task<IActionResult> ChangePasswordAsync(
            [FromBody] ChangePasswordDTO changePasswordDTO)
        {
            await _authenticationService
                .ChangePasswordAsync(
                    changePasswordDTO,
                    UserService.GetCurrentUserIdentifier(User)
                );

            return Ok();
        }

        [HttpPut("reset-password")]
        public async Task<IActionResult> ResetPasswordAsync(
            [FromBody] ResetPasswordDTO resetPasswordDTO)
        {
            await _authenticationService
                .ResetPasswordAsync(resetPasswordDTO);

            return Ok();
        }
    }
}
