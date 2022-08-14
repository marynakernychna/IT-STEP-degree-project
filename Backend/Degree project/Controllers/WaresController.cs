using Core.Constants;
using Core.DTO;
using Core.DTO.Ware;
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
    public class WaresController : Controller
    {
        private readonly IUserService _userService;
        private readonly IWareService _wareService;

        public WaresController(
            IUserService userService,
            IWareService wareService)
        {
            _userService = userService;
            _wareService = wareService;
        }

        [HttpPost("create")]
        [AuthorizeByRole(IdentityRoleNames.User)]
        public async Task<IActionResult> CreateAsync(
            [FromBody] CreateWareDTO createWareDTO)
        {
            var userId = _userService.GetCurrentUserNameIdentifier(User);
            await _wareService.CreateAsync(createWareDTO, userId);

            return Ok();
        }

        [HttpGet("getByCategory")]
        [AuthorizeByRole(IdentityRoleNames.User)]
        public async Task<IActionResult> GetByCategoryAsync(
           [FromQuery] PaginationFilterDTO paginationFilter, string categoryTitle)
        {
            var wares = await _wareService.GetByCategoryAsync(paginationFilter, categoryTitle);

            return Ok(wares);
        }
    }
}
