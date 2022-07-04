using Core.Interfaces.CustomService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(
            IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("user-info")]
        public async Task<ActionResult> GetUserInfo()
        {
            var userId = _userService.GetCurrentUserNameIdentifier(User);
            var userInfo = await _userService.GetUserProfileInfoAsync(userId);
            return Ok(userInfo);
        }
    }
}
