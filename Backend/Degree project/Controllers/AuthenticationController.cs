using Core.Constants;
using Core.DTO.Authentication;
using Core.DTO.User;
using Core.Helpers;
using Core.Interfaces.CustomService;
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
        private readonly IUserService _userService;

        public AuthenticationController(
                IAuthenticationService authenticationService,
                IUserService userService
            )
        {
            _authenticationService = authenticationService;
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync(
            [FromBody] UserRegistrationDTO userRegistrationDTO)
        {
            await _authenticationService.RegisterAsync(userRegistrationDTO);

            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] UserLoginDTO data)
        {
            var tokens = await _authenticationService.LoginAsync(data);

            return Ok(tokens);
        }

        [HttpPost("logout")]
        [AuthorizeByRole(IdentityRoleNames.Admin, IdentityRoleNames.User)]
        public async Task<IActionResult> LogoutAsync([FromBody] UserLogoutDTO userLogoutDTO)
        {
            await _authenticationService.LogoutAsync(userLogoutDTO);

            return Ok();
        }

        [HttpPost("send-confirm-reset-password-email")]
        public async Task<IActionResult> SendConfirmResetPasswordEmailAsync(
            [FromBody] ConfirmationResetPasswordDTO confirmationResetPasswordDTO)
        {
            var callbackUrl = Request.GetTypedHeaders().Referer.ToString();

            await _authenticationService.SendConfirmResetPasswordEmail(
                confirmationResetPasswordDTO.Email, callbackUrl);

            return Ok();
        }

        [HttpPut("reset-password")]
        public async Task<IActionResult> ResetPasswordAsync(
            [FromQuery] ResetPasswordDTO resetPasswordDTO)
        {
            await _authenticationService.ResetPasswordAsync(resetPasswordDTO);

            return Ok();
        }
    }
}
