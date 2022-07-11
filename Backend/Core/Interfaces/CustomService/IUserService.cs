using Core.DTO;
using Core.DTO.User;
using Core.Helpers;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Core.Interfaces.CustomService
{
    public interface IUserService
    {
        string GetCurrentUserNameIdentifier(ClaimsPrincipal currentUser);
        Task<UserProfileInfoDTO> GetUserProfileInfoAsync(string userId);
        Task<PaginatedList<UserProfileInfoDTO>> GetUsersProfileInfoAsync(
            PaginationFilterDTO paginationFilter);
    }
}
