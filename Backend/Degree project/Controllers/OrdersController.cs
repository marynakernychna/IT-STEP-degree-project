using Core.Constants;
using Core.DTO;
using Core.DTO.Order;
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
    public class OrdersController : Controller
    {
        private readonly IOrderService _orderService;

        public OrdersController(
            IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet("clients/by-client/page")]
        [AuthorizeByRole(IdentityRoleNames.Client)]
        public async Task<IActionResult> GetPageByClientAsync(
            [FromQuery] PaginationFilterDTO paginationFilterDTO)
        {
            return Ok(await _orderService.GetPageByClientAsync(
                UserService.GetCurrentUserIdentifier(User),
                paginationFilterDTO));
        }

        [HttpGet("couriers/available/page")]
        [AuthorizeByRole(IdentityRoleNames.Courier)]
        public async Task<IActionResult> GetPageOfAvailvableAsync(
            [FromQuery] PaginationFilterDTO paginationFilterDTO)
        {
            return Ok(await _orderService
                .GetPageOfAvailvableAsync(paginationFilterDTO));
        }

        [HttpGet("couriers/by-courier/assigned/page")]
        [AuthorizeByRole(IdentityRoleNames.Courier)]
        public async Task<IActionResult> GetPageOfAssignedByCourierAsync(
            [FromQuery] PaginationFilterDTO paginationFilterDTO)
        {
            return Ok(await _orderService.GetPageOfAssignedByCourierAsync(
                UserService.GetCurrentUserIdentifier(User),
                paginationFilterDTO));
        }

        [HttpGet("delivered/by-user/page")]
        [AuthorizeByRole(
            IdentityRoleNames.Client,
            IdentityRoleNames.Courier)]
        public async Task<IActionResult> GetPageOfDeliveredOrdersAsync(
            [FromQuery] PaginationFilterDTO paginationFilterDTO)
        {
            return Ok(await _orderService.GetPageOfDeliveredAsync(
                UserService.GetCurrentUserIdentifier(User),
                paginationFilterDTO));
        }

        [HttpPost("create")]
        [AuthorizeByRole(IdentityRoleNames.Client)]
        public async Task<IActionResult> CreateAsync(
            [FromBody] OrderDTO createOrderDTO)
        {
            await _orderService.CreateAsync(
                UserService.GetCurrentUserIdentifier(User),
                createOrderDTO);

            return Ok();

        }

        [HttpPut("couriers/assign")]
        [AuthorizeByRole(IdentityRoleNames.Courier)]
        public async Task<IActionResult> AssignAsync(
            [FromBody] EntityIdDTO idDTO)
        {
            await _orderService.AssignAsync(
                UserService.GetCurrentUserIdentifier(User),
                idDTO.Id);

            return Ok();
        }

        [HttpPut("couriers/reject")]
        [AuthorizeByRole(IdentityRoleNames.Courier)]
        public async Task<IActionResult> RejectAsync(
            [FromBody] EntityIdDTO idDTO)
        {
            await _orderService.RejectAsync(
                idDTO.Id,
                UserService.GetCurrentUserIdentifier(User));

            return Ok();
        }

        [HttpPut("delivery/confirm")]
        [AuthorizeByRole(
            IdentityRoleNames.Client,
            IdentityRoleNames.Courier)]
        public async Task<IActionResult> ConfirmDeliveryAsync(
            [FromBody] EntityIdDTO idDTO)
        {
            await _orderService.ConfirmDeliveryAsync(
                UserService.GetCurrentUserIdentifier(User),
                idDTO.Id);

            return Ok();
        }

        [HttpPut("delivery/reject")]
        [AuthorizeByRole(
            IdentityRoleNames.Client,
            IdentityRoleNames.Courier)]
        public async Task<IActionResult> RejectDeliveryAsync(
            [FromBody] EntityIdDTO idDTO)
        {
            await _orderService.RejectDeliveryAsync(
                UserService.GetCurrentUserIdentifier(User),
                idDTO.Id);

            return Ok();
        }

        [HttpPut("update")]
        [AuthorizeByRole(IdentityRoleNames.Client)]
        public async Task<IActionResult> UpdateAsync(
            [FromBody] ChangeOrderInfoDTO changeOrderInfoDTO)
        {
            await _orderService.UpdateAsync(
                changeOrderInfoDTO,
                UserService.GetCurrentUserIdentifier(User));

            return Ok();
        }

        [HttpDelete("delete")]
        [AuthorizeByRole(IdentityRoleNames.Client)]
        public async Task<IActionResult> DeleteAsync(
            [FromQuery] EntityIdDTO entityIdDTO)
        {
            await _orderService.DeleteAsync(
                UserService.GetCurrentUserIdentifier(User),
                entityIdDTO.Id);

            return Ok();
        }
    }
}
