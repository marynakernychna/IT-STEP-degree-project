using Core.Constants;
using Core.DTO;
using Core.DTO.PaginationFilter;
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
    public class CartsController : Controller
    {
        private readonly ICartService _cartService;

        public CartsController(
            ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpGet("admins/by-client/page")]
        [AuthorizeByRole(IdentityRoleNames.Admin)]
        public async Task<IActionResult> GetPageByClientAsync(
            [FromQuery] PaginationFilterCartDTO paginationFilterCartDTO)
        {
            var wares = await _cartService
                .GetPageByClientAsync(paginationFilterCartDTO);

            return Ok(wares);
        }

        [HttpGet("by-client/page")]
        [AuthorizeByRole(IdentityRoleNames.Client)]
        public async Task<IActionResult> GetPageByClientAsync(
            [FromQuery] PaginationFilterDTO paginationFilterDTO)
        {
            var page = await _cartService
                .GetPageByClientAsync(
                    UserService.GetCurrentUserIdentifier(User),
                    paginationFilterDTO
                );

            return Ok(page);
        }

        [HttpPut("by-client/add-ware")]
        [AuthorizeByRole(IdentityRoleNames.Client)]
        public async Task<IActionResult> AddWareAsync(
            [FromBody] EntityIdDTO entityIdDTO)
        {
            await _cartService
                .AddWareAsync(
                    UserService.GetCurrentUserIdentifier(User),
                    entityIdDTO.Id
                );

            return Ok();
        }

        [HttpDelete("by-client/delete-ware")]
        [AuthorizeByRole(IdentityRoleNames.Client)]
        public async Task<IActionResult> DeleteWareAsync(
            [FromQuery] EntityIdDTO entityIdDTO)
        {
            await _cartService
                .DeleteWareAsync(
                    UserService.GetCurrentUserIdentifier(User),
                    entityIdDTO.Id
                );

            return Ok();
        }
    }
}
