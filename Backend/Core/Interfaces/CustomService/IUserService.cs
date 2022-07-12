using Core.DTO;
using Core.DTO.User;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Core.Interfaces.CustomService
{
    public interface IUserService
    {
        Task<UserProfileInfoDTO> GetUserProfileInfoAsync(string userId);
        string GetCurrentUserNameIdentifier(ClaimsPrincipal currentUser);
        Task UserEditProfileInfoAsync(
            UserEditProfileInfoDTO userEditProfileInfo, string userId, string callbackUrl);
    }
}
