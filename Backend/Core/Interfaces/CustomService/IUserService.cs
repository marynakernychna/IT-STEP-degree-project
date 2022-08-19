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
        Task UserEditProfileInfoAsync(
            UserEditProfileInfoDTO userEditProfileInfo, string userId, string callbackUrl);
        Task<UserProfileInfoDTO> GetUserProfileInfoAsync(string userId);
        Task<PaginatedList<UserProfileInfoDTO>> GetUsersProfileInfoAsync(
            PaginationFilterDTO paginationFilter);
        Task<string> GetUserIdByEmailAsync(string email);
        Task ChangePasswordAsync(ChangePasswordDTO changePasswordDTO, string userId);
    }
}
