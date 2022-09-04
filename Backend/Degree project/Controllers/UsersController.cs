using Core.Constants;
using Core.DTO;
using Core.DTO.User;
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

        [HttpGet("profile")]
        [AuthorizeByRole(IdentityRoleNames.User)]
        public async Task<IActionResult> GetClientProfileAsync()
        {
            var userId = _userService.GetCurrentUserIdentifier(User);
            var userInfo = await _userService.GetProfileAsync(userId);

            return Ok(userInfo);
        }

        [HttpGet("admins/page")]
        [AuthorizeByRole(IdentityRoleNames.Admin)]
        public async Task<IActionResult> GetPageOfClientsAsync(
            [FromQuery] PaginationFilterDTO paginationFilter)
        {
            var usersInfo = await _userService.GetPageOfClientsAsync(paginationFilter);

            return Ok(usersInfo);
        }

        [HttpPost("admin/clients/by-client/profile/update")]
        [AuthorizeByRole(IdentityRoleNames.Admin)]
        public async Task<IActionResult> UpdateClientProfileAsync(
            [FromBody] UserEditProfileInfoDTO newUserInfo, string email)
        {
            var userId = await _userService.GetIdByEmailAsync(email);
            var callbackUrl = Request.GetTypedHeaders().Referer.ToString();
            await _userService.UpdateProfileAsync(newUserInfo, userId, callbackUrl);

            return Ok();
        }

        [HttpPut("profile/update")]
        [AuthorizeByRole(IdentityRoleNames.User)]
        public async Task<IActionResult> UpdateClientProfileAsync(
            [FromBody] UserEditProfileInfoDTO newUserInfo)
        {
            var callbackUrl = Request.GetTypedHeaders().Referer.ToString();
            var userId = _userService.GetCurrentUserIdentifier(User);
            await _userService.UpdateProfileAsync(newUserInfo, userId, callbackUrl);

            return Ok();
        }
    }
}
