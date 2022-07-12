using Core.DTO;
using Core.Interfaces.CustomService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(
            IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("user-info")]
        public async Task<IActionResult> GetUserInfoAsync()
        {
            var userId = _userService.GetCurrentUserNameIdentifier(User);
            var userInfo = await _userService.GetUserProfileInfoAsync(userId);

            return Ok(userInfo);
        }

        [HttpPost("edit-info")]
        public async Task<IActionResult> UserEditProfileInfo(
            UserEditProfileInfoDTO newUserInfo)
        {
            var callbackUrl = Request.GetTypedHeaders().Referer.ToString();
            var userId = _userService.GetCurrentUserNameIdentifier(User);
            await _userService.UserEditProfileInfoAsync(newUserInfo, userId, callbackUrl);

            return Ok();
        }
    }
}
