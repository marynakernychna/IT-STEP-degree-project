using Core.DTO.Authentication;
using Core.DTO.User;
using System.Threading.Tasks;

namespace Core.Interfaces.CustomService
{
    public interface IAuthenticationService
    {
        Task RegisterAsync(UserRegistrationDTO userRegistrationDTO);
        Task<UserAutorizationDTO> LoginAsync(UserLoginDTO userLoginDTO);
        Task LogoutAsync(UserLogoutDTO userLogoutDTO);
        Task ResetPasswordAsync(ResetPasswordDTO resetPasswordDTO);
        Task SendConfirmResetPasswordEmailAsync(string email, string callbackUrl);
        Task ChangePasswordAsync(ChangePasswordDTO changePasswordDTO, string userId);
    }
}
