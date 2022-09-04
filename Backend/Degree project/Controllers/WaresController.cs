using Core.Constants;
using Core.DTO;
using Core.DTO.PaginationFilter;
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

        [HttpGet("by-id")]
        [AuthorizeByRole(
            IdentityRoleNames.User,
            IdentityRoleNames.Admin)]
        public async Task<IActionResult> GetByIdAsync(
            [FromQuery] EntityIdDTO entityIdDTO)
        {
            var ware = await _wareService.GetByIdAsync(entityIdDTO.Id);

            return Ok(ware);
        }

        [HttpGet("page")]
        [AuthorizeByRole(IdentityRoleNames.User)]
        public async Task<IActionResult> GetAllAsync(
            [FromQuery] PaginationFilterDTO paginationFilter)
        {
            var wares = await _wareService.GetAllAsync(paginationFilter);

            return Ok(wares);
        }

        [HttpGet("by-category/page")]
        [AuthorizeByRole(IdentityRoleNames.User)]
        public async Task<IActionResult> GetByCategoryAsync(
            [FromQuery] PaginationFilterWareDTO paginationFilter)
        {
            var wares = await _wareService.GetByCategoryAsync(paginationFilter);

            return Ok(wares);
        }

        [HttpGet("clients/by-client/page")]
        [AuthorizeByRole(IdentityRoleNames.User)]
        public async Task<IActionResult> GetCreatedByClientAsync(
            [FromQuery] PaginationFilterDTO paginationFilter)
        {
            var userId = _userService.GetCurrentUserIdentifier(User);

            var ware = await _wareService.GetCreatedByUserAsync(userId, paginationFilter);

            return Ok(ware);
        }

        [HttpPost("create")]
        [AuthorizeByRole(IdentityRoleNames.User)]
        public async Task<IActionResult> CreateAsync(
            [FromBody] CreateWareDTO createWareDTO)
        {
            var userId = _userService.GetCurrentUserIdentifier(User);
            await _wareService.CreateAsync(createWareDTO, userId);

            return Ok();
        }
    }
}
