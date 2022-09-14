using Core.Constants;
using Core.DTO;
using Core.DTO.User;
using Core.Helpers;
using Core.Interfaces.CustomService;
using Core.Services;
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

        [HttpGet("admins/clients/page")]
        [AuthorizeByRole(IdentityRoleNames.Admin)]
        public async Task<IActionResult> GetPageOfClientsAsync(
            [FromQuery] PaginationFilterDTO paginationFilter)
        {
            var usersInfo = await _userService.GetPageOfClientsAsync(paginationFilter);

            return Ok(usersInfo);
        }

        [HttpGet("admins/couriers/page")]
        [AuthorizeByRole(IdentityRoleNames.Admin)]
        public async Task<IActionResult> GetPageOfCouriersAsync(
            [FromQuery] PaginationFilterDTO paginationFilter)
        {
            var couriers = await _userService.GetPageOfCouriersAsync(paginationFilter);

            return Ok(couriers);
        }

        [HttpGet("profile")]
        [AuthorizeByRole(IdentityRoleNames.Client)]
        public async Task<IActionResult> GetClientProfileAsync()
        {
            var userInfo = await _userService.GetProfileAsync(
                UserService.GetCurrentUserIdentifier(User));

            return Ok(userInfo);
        }

        [HttpPut("admins/clients/by-client/profile/update")]
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
        [AuthorizeByRole(IdentityRoleNames.Client)]
        public async Task<IActionResult> UpdateClientProfileAsync(
            [FromBody] UserEditProfileInfoDTO newUserInfo)
        {
            var callbackUrl = Request.GetTypedHeaders().Referer.ToString();

            await _userService.UpdateProfileAsync(
                newUserInfo,
                UserService.GetCurrentUserIdentifier(User),
                callbackUrl);

            return Ok();
        }
    }
}
