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

        [HttpPost("create")]
        [AuthorizeByRole(IdentityRoleNames.User)]
        public async Task<IActionResult> CreateAsync(
            [FromBody] CreateOrderDTO createOrderDTO)
        {
            var userId = _userService.GetCurrentUserNameIdentifier(User);

            await _orderService.CreateAsync(userId, createOrderDTO);

            return Ok();
        }

        [HttpGet("available")]
        [AuthorizeByRole(IdentityRoleNames.Courier)]
        public async Task<IActionResult> GetAvailvableAsync(
            [FromQuery] PaginationFilterDTO paginationFilterDTO)
        {
            var orders = await _orderService.GetAvailableAsync(paginationFilterDTO);

            return Ok(orders);
        }

        [HttpGet("assign-to-order")]
        [AuthorizeByRole(IdentityRoleNames.Courier)]
        public async Task<IActionResult> AssignToOrderAsync([FromQuery] int orderId)
        {
            var courierId = _userService.GetCurrentUserNameIdentifier(User);

            await _orderService.AssignToOrderAsync(courierId, orderId);

            return Ok();
        }

        [HttpPut("reject-selected-order")]
        [AuthorizeByRole(IdentityRoleNames.Courier)]
        public async Task<IActionResult> RejectSelectedOrdedr([FromQuery] int orderId)
        {
            var courierId = _userService.GetCurrentUserNameIdentifier(User);

            await _orderService.RejectSelectedOrderAsync(orderId, courierId);

            return Ok();
        }

        [HttpGet("by-courier")]
        [AuthorizeByRole(IdentityRoleNames.Courier)]
        public async Task<IActionResult> GetByCourierAsync(
            [FromQuery] PaginationFilterDTO paginationFilterDTO)
        {
            var courierId = _userService.GetCurrentUserNameIdentifier(User);

            var orders = await _orderService.GetByCourierAsync(courierId, paginationFilterDTO);

            return Ok(orders);
        }
    }
}
