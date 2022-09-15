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
            return Ok(await _userService
                .GetPageOfClientsAsync(paginationFilter));
        }

        [HttpGet("admins/couriers/page")]
        [AuthorizeByRole(IdentityRoleNames.Admin)]
        public async Task<IActionResult> GetPageOfCouriersAsync(
            [FromQuery] PaginationFilterDTO paginationFilter)
        {
            return Ok(await _userService
                .GetPageOfCouriersAsync(paginationFilter));
        }

        [HttpGet("profile")]
        [AuthorizeByRole(IdentityRoleNames.Client)]
        public async Task<IActionResult> GetClientProfileAsync()
        {
            return Ok(await _userService
                .GetProfileAsync(
                    UserService.GetCurrentUserIdentifier(User))
                );
        }

        [HttpPut("admins/clients/by-client/profile/update")]
        [AuthorizeByRole(IdentityRoleNames.Admin)]
        public async Task<IActionResult> UpdateClientProfileAsync(
            [FromBody] UserEditProfileInfoDTO newUserInfo, string email) /// !!!
        {
            await _userService.UpdateProfileAsync(
                newUserInfo,
                await _userService.GetIdByEmailAsync(email),
                Request.GetTypedHeaders().Referer.ToString());

            return Ok();
        }

        [HttpPut("profile/update")]
        [AuthorizeByRole(IdentityRoleNames.Client)]
        public async Task<IActionResult> UpdateClientProfileAsync(
            [FromBody] UserEditProfileInfoDTO newUserInfo)
        {
            ;
            await _userService.UpdateProfileAsync(
                newUserInfo,
                UserService.GetCurrentUserIdentifier(User),
                Request.GetTypedHeaders().Referer.ToString());

            return Ok();
        }
    }
}
