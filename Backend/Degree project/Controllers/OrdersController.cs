using Core.Constants;
using Core.DTO;
using Core.DTO.Order;
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
    public class OrdersController : Controller
    {
        private readonly IUserService _userService;
        private readonly IOrderService _orderService;

        public OrdersController(
            IUserService userService,
            IOrderService orderService)
        {
            _userService = userService;
            _orderService = orderService;
        }

        [HttpGet("clients/by-client/page")]
        [AuthorizeByRole(IdentityRoleNames.User)]
        public async Task<IActionResult> GetPageByClientAsync(
            [FromQuery] PaginationFilterDTO paginationFilterDTO)
        {
            var userId = _userService.GetCurrentUserIdentifier(User);

            var orders = await _orderService.GetPageByClientAsync(userId, paginationFilterDTO);

            return Ok(orders);
        }

        [HttpGet("couriers/available/page")]
        [AuthorizeByRole(IdentityRoleNames.Courier)]
        public async Task<IActionResult> GetPageOfAvailvableAsync(
            [FromQuery] PaginationFilterDTO paginationFilterDTO)
        {
            var orders = await _orderService.GetPageOfAvailvableAsync(paginationFilterDTO);

            return Ok(orders);
        }

        [HttpGet("couriers/by-courier/assigned/page")]
        [AuthorizeByRole(IdentityRoleNames.Courier)]
        public async Task<IActionResult> GetPageOfAssignedByCourierAsync(
            [FromQuery] PaginationFilterDTO paginationFilterDTO)
        {
            var courierId = _userService.GetCurrentUserIdentifier(User);

            var orders = await _orderService.GetPageOfAssignedByCourierAsync(courierId, paginationFilterDTO);

            return Ok(orders);
        }

        [HttpPost("create")]
        [AuthorizeByRole(IdentityRoleNames.User)]
        public async Task<IActionResult> CreateAsync(
            [FromBody] OrderDTO createOrderDTO)
        {
            var userId = _userService.GetCurrentUserIdentifier(User);

            await _orderService.CreateAsync(userId, createOrderDTO);

            return Ok();

        }

        [HttpPut("couriers/assign")]
        [AuthorizeByRole(IdentityRoleNames.Courier)]
        public async Task<IActionResult> AssignAsync(
            [FromBody] EntityIdDTO idDTO)
        {
            var courierId = _userService.GetCurrentUserIdentifier(User);

            await _orderService.AssignAsync(courierId, idDTO.Id);

            return Ok();
        }

        [HttpPut("couriers/reject")]
        [AuthorizeByRole(IdentityRoleNames.Courier)]
        public async Task<IActionResult> RejectAsync(
            [FromBody] EntityIdDTO idDTO)
        {
            var courierId = _userService.GetCurrentUserIdentifier(User);

            await _orderService.RejectAsync(idDTO.Id, courierId);

            return Ok();
        }

        [HttpPut("update")]
        [AuthorizeByRole(IdentityRoleNames.User)]
        public async Task<IActionResult> UpdateAsync(
            [FromBody] ChangeOrderInfoDTO changeOrderInfoDTO)
        {
            var userId = _userService.GetCurrentUserIdentifier(User);

            await _orderService.UpdateAsync(changeOrderInfoDTO, userId);

            return Ok();
        }

        [HttpDelete("delete")]
        [AuthorizeByRole(IdentityRoleNames.User)]
        public async Task<IActionResult> DeleteAsync(
            [FromQuery] EntityIdDTO entityIdDTO)
        {
            var userId = _userService.GetCurrentUserIdentifier(User);

            await _orderService.DeleteAsync(userId, entityIdDTO.Id);

            return Ok();
        }
    }
}
