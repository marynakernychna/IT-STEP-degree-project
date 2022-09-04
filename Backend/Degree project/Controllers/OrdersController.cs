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
            var userId = _userService.GetCurrentUserNameIdentifier(User);

            var orders = await _orderService.GetByUserAsync(userId, paginationFilterDTO);

            return Ok(orders);
        }

        [HttpGet("couriers/available/page")]
        [AuthorizeByRole(IdentityRoleNames.Courier)]
        public async Task<IActionResult> GetPageOfAvailvableAsync(
            [FromQuery] PaginationFilterDTO paginationFilterDTO)
        {
            var orders = await _orderService.GetAvailableAsync(paginationFilterDTO);

            return Ok(orders);
        }

        [HttpGet("couriers/by-courier/assigned/page")]
        [AuthorizeByRole(IdentityRoleNames.Courier)]
        public async Task<IActionResult> GetPageOfAssignedByCourierAsync(
            [FromQuery] PaginationFilterDTO paginationFilterDTO)
        {
            var courierId = _userService.GetCurrentUserNameIdentifier(User);

            var orders = await _orderService.GetByCourierAsync(courierId, paginationFilterDTO);

            return Ok(orders);
        }

        [HttpPost("create")]
        [AuthorizeByRole(IdentityRoleNames.User)]
        public async Task<IActionResult> CreateAsync(
            [FromBody] OrderDTO createOrderDTO)
        {
            var userId = _userService.GetCurrentUserNameIdentifier(User);

            await _orderService.CreateAsync(userId, createOrderDTO);

            return Ok();

        }

        [HttpPut("update")]
        [AuthorizeByRole(IdentityRoleNames.User)]
        public async Task<IActionResult> UpdateAsync(
            [FromBody] ChangeOrderInfoDTO changeOrderInfoDTO)
        {
            var userId = _userService.GetCurrentUserNameIdentifier(User);

            await _orderService.ChangeInfoAsync(changeOrderInfoDTO, userId);

            return Ok();
        }

        [HttpPut("couriers/assign")]
        [AuthorizeByRole(IdentityRoleNames.Courier)]
        public async Task<IActionResult> AssignAsync(
            [FromBody] EntityIdDTO idDTO)
        {
            var courierId = _userService.GetCurrentUserNameIdentifier(User);

            await _orderService.AssignToOrderAsync(courierId, idDTO.Id);

            return Ok();
        }

        [HttpPut("couriers/reject")]
        [AuthorizeByRole(IdentityRoleNames.Courier)]
        public async Task<IActionResult> RejectAsync(
            [FromBody] EntityIdDTO idDTO)
        {
            var courierId = _userService.GetCurrentUserNameIdentifier(User);

            await _orderService.RejectSelectedOrderAsync(idDTO.Id, courierId);

            return Ok();
        }

        [HttpDelete("delete")]
        [AuthorizeByRole(IdentityRoleNames.User)]
        public async Task<IActionResult> DeleteAsync(
            [FromQuery] EntityIdDTO entityIdDTO)
        {
            var userId = _userService.GetCurrentUserNameIdentifier(User);

            await _orderService.DeleteAsync(userId, entityIdDTO.Id);

            return Ok();
        }
    }
}
