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

        [HttpGet("by-user")]
        [AuthorizeByRole(IdentityRoleNames.User)]
        public async Task<IActionResult> GetByUserAsync(
            [FromQuery] PaginationFilterDTO paginationFilterDTO)
        {
            var userId = _userService.GetCurrentUserNameIdentifier(User);

            var wares = await _cartService.GetByUserIdAsync(userId, paginationFilterDTO);

            return Ok(wares);
        }

        [HttpPost("add-ware")]
        [AuthorizeByRole(IdentityRoleNames.User)]
        public async Task<IActionResult> AddWareAsync(
            [FromBody] EntityIdDTO entityIdDTO)
        {
            var userId = _userService.GetCurrentUserNameIdentifier(User);

            await _cartService.AddWareAsync(userId, entityIdDTO.Id);

            return Ok();
        }

        [HttpDelete("delete-ware")]
        [AuthorizeByRole(IdentityRoleNames.User)]
        public async Task<IActionResult> DeleteWareAsync(
            [FromQuery] EntityIdDTO entityIdDTO)
        {
            var userId = _userService.GetCurrentUserNameIdentifier(User);

            await _cartService.DeleteWareAsync(userId, entityIdDTO.Id);

            return Ok();
        }

        [HttpGet("get-cart-by-user")]
        [AuthorizeByRole(IdentityRoleNames.Admin)]
        public async Task<IActionResult> GetCartByUserAsync(
            [FromQuery] PaginationFilterCartDTO paginationFilterCartDTO)
        {
            var paginationFilterDTO = new PaginationFilterDTO()
            {
                PageSize = paginationFilterCartDTO.PageSize,
                PageNumber = paginationFilterCartDTO.PageNumber
            };

            var wares = await _cartService.GetByUserIdAsync(paginationFilterCartDTO.UserId, paginationFilterDTO);

            return Ok(wares);
        }
    }
}
