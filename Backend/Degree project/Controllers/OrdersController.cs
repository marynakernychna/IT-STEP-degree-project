﻿using Core.Constants;
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
            [FromBody] OrderDTO createOrderDTO)
        {
            var userId = _userService.GetCurrentUserNameIdentifier(User);

            await _orderService.CreateAsync(userId, createOrderDTO);

            return Ok();

        }

        [HttpGet("by-user")]
        [AuthorizeByRole(IdentityRoleNames.User)]
        public async Task<IActionResult> GetByUserAsync(
            [FromQuery] PaginationFilterDTO paginationFilterDTO)
        {
            var userId = _userService.GetCurrentUserNameIdentifier(User);

            var orders = await _orderService.GetByUserAsync(userId, paginationFilterDTO);

            return Ok(orders);
        }

        [HttpGet("available")]
        [AuthorizeByRole(IdentityRoleNames.Courier)]
        public async Task<IActionResult> GetAvailvableAsync(
            [FromQuery] PaginationFilterDTO paginationFilterDTO)
        {
            var orders = await _orderService.GetAvailableAsync(paginationFilterDTO);

            return Ok(orders);
        }

        [HttpPut("assign-to-order")]
        [AuthorizeByRole(IdentityRoleNames.Courier)]
        public async Task<IActionResult> AssignToOrderAsync([FromBody] EntityIdDTO idDTO)
        {
            var courierId = _userService.GetCurrentUserNameIdentifier(User);

            await _orderService.AssignToOrderAsync(courierId, idDTO);

            return Ok();
        }

        [HttpPut("reject-selected-order")]
        [AuthorizeByRole(IdentityRoleNames.Courier)]
        public async Task<IActionResult> RejectSelectedOrderAsync([FromBody] EntityIdDTO idDTO)
        {
            var courierId = _userService.GetCurrentUserNameIdentifier(User);

            await _orderService.RejectSelectedOrderAsync(idDTO, courierId);

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

        [HttpPut("change-by-id")]
        [AuthorizeByRole(IdentityRoleNames.User)]
        public async Task<IActionResult> ChangeByIdAsync(
            [FromBody] ChangeOrderInfoDTO changeOrderInfoDTO)
        {
            var userId = _userService.GetCurrentUserNameIdentifier(User);

            await _orderService.ChangeInfoAsync(changeOrderInfoDTO, userId);

            return Ok();
        }
    }
}
