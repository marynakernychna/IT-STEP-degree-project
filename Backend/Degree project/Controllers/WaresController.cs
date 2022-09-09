using Core.Constants;
using Core.DTO;
using Core.DTO.PaginationFilter;
using Core.DTO.Ware;
using Core.Helpers;
using Core.Interfaces.CustomService;
using Core.Services;
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
        private readonly IWareService _wareService;

        public WaresController(
            IWareService wareService)
        {
            _wareService = wareService;
        }

        [HttpGet("by-category/page")]
        [AuthorizeByRole(IdentityRoleNames.User)]
        public async Task<IActionResult> GetByCategoryAsync(
            [FromQuery] PaginationFilterWareDTO paginationFilter)
        {
            var wares = await _wareService.GetByCategoryAsync(paginationFilter);

            return Ok(wares);
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

        [HttpGet("clients/by-client/page")]
        [AuthorizeByRole(IdentityRoleNames.User)]
        public async Task<IActionResult> GetCreatedByClientAsync(
            [FromQuery] PaginationFilterDTO paginationFilter)
        {
            var ware = await _wareService.GetCreatedByUserAsync(
                UserService.GetCurrentUserIdentifier(User),
                paginationFilter);

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

        [HttpPost("create")]
        [AuthorizeByRole(IdentityRoleNames.User)]
        public async Task<IActionResult> CreateAsync(
            [FromBody] CreateWareDTO createWareDTO)
        {
            await _wareService.CreateAsync(
                createWareDTO,
                UserService.GetCurrentUserIdentifier(User));

            return Ok();
        }
    }
}
