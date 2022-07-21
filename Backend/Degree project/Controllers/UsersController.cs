using Core.Constants;
using Core.DTO;
using Core.Helpers;
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
        public async Task<IActionResult> UserEditProfileInfoAsync(
            UserEditProfileInfoDTO newUserInfo)
        {
            var callbackUrl = Request.GetTypedHeaders().Referer.ToString();
            var userId = _userService.GetCurrentUserNameIdentifier(User);
            await _userService.UserEditProfileInfoAsync(newUserInfo, userId, callbackUrl);

            return Ok();
        }

        [HttpGet("users-info")]
        [AuthorizeByRole(IdentityRoleNames.Admin)]
        public async Task<IActionResult> GetUsersInfoAsync(
            [FromQuery] PaginationFilterDTO paginationFilter)
        {
            var usersInfo = await _userService.GetUsersProfileInfoAsync(paginationFilter);

            return Ok(usersInfo);
        }

        [HttpPost("user-edit-info")]
        [AuthorizeByRole(IdentityRoleNames.Admin)]
        public async Task<IActionResult> UserEditProfileInfoAsync(
            UserEditProfileInfoDTO newUserInfo, string email)
        {
            var userId = await _userService.GetUserIdByEmailAsync(email);
            var callbackUrl = Request.GetTypedHeaders().Referer.ToString();
            await _userService.UserEditProfileInfoAsync(newUserInfo, userId, callbackUrl);

            return Ok();
        }
    }
}
