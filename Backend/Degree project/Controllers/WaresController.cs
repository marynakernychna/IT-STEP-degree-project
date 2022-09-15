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
        [AuthorizeByRole(IdentityRoleNames.Client)]
        public async Task<IActionResult> GetByCategoryAsync(
            [FromQuery] PaginationFilterWareDTO paginationFilter)
        {
            return Ok(await _wareService
                .GetByCategoryAsync(paginationFilter));
        }

        [HttpGet("by-id")]
        [AuthorizeByRole(
            IdentityRoleNames.Client,
            IdentityRoleNames.Admin)]
        public async Task<IActionResult> GetByIdAsync(
            [FromQuery] EntityIdDTO entityIdDTO)
        {
            return Ok(await _wareService
                .FormWareInfoDTOByIdAsync(entityIdDTO.Id));
        }

        [HttpGet("clients/by-client/page")]
        [AuthorizeByRole(IdentityRoleNames.Client)]
        public async Task<IActionResult> GetCreatedByClientAsync(
            [FromQuery] PaginationFilterDTO paginationFilter)
        {
            return Ok(await _wareService.GetCreatedByUserAsync(
                UserService.GetCurrentUserIdentifier(User),
                paginationFilter));
        }

        [HttpGet("page")]
        [AuthorizeByRole(IdentityRoleNames.Client)]
        public async Task<IActionResult> GetAllAsync(
            [FromQuery] PaginationFilterDTO paginationFilter)
        {
            return Ok(await _wareService
                .GetAllAsync(paginationFilter));
        }

        [HttpPost("create")]
        [AuthorizeByRole(IdentityRoleNames.Client)]
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
