using Core.Constants;
using Core.DTO;
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
    }
}
