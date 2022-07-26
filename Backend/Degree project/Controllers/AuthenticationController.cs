using Core.DTO.Authentication;
using Core.Interfaces.CustomService;
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
                IAuthenticationService authenticationService
            )
        {
            _authenticationService = authenticationService;
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
        public async Task<IActionResult> LogoutAsync([FromBody] UserLogoutDTO userLogoutDTO)
        {
            await _authenticationService.LogoutAsync(userLogoutDTO);
            return Ok();
        }
    }
}
