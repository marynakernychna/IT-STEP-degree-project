using Core.Constants;
using Core.DTO;
using Core.DTO.PaginationFilter;
using Core.Helpers;
using Core.Interfaces.CustomService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CartsController : Controller
    {
        private readonly IUserService _userService;
        private readonly ICartService _cartService;

        public CartsController(
            IUserService userService,
            ICartService cartService)
        {
            _userService = userService;
            _cartService = cartService;
        }

        [HttpGet("by-client/page")]
        [AuthorizeByRole(IdentityRoleNames.User)]
        public async Task<IActionResult> GetPageByClientAsync(
            [FromQuery] PaginationFilterDTO paginationFilterDTO)
        {
            var userId = _userService.GetCurrentUserIdentifier(User);

            var wares = await _cartService.GetPageByClientAsync(userId, paginationFilterDTO);

            return Ok(wares);
        }

        [HttpGet("admins/by-client/page")]
        [AuthorizeByRole(IdentityRoleNames.Admin)]
        public async Task<IActionResult> GetPageByClientAsync(
            [FromQuery] PaginationFilterCartDTO paginationFilterCartDTO)
        {
            var wares = await _cartService.GetPageByClientAsync(paginationFilterCartDTO);

            return Ok(wares);
        }

        [HttpPut("by-client/add-ware")]
        [AuthorizeByRole(IdentityRoleNames.User)]
        public async Task<IActionResult> AddWareAsync(
            [FromBody] EntityIdDTO entityIdDTO)
        {
            var userId = _userService.GetCurrentUserIdentifier(User);

            await _cartService.AddWareAsync(userId, entityIdDTO.Id);

            return Ok();
        }

        [HttpDelete("by-client/delete-ware")]
        [AuthorizeByRole(IdentityRoleNames.User)]
        public async Task<IActionResult> DeleteWareAsync(
            [FromQuery] EntityIdDTO entityIdDTO)
        {
            var userId = _userService.GetCurrentUserIdentifier(User);

            await _cartService.DeleteWareAsync(userId, entityIdDTO.Id);

            return Ok();
        }
    }
}
