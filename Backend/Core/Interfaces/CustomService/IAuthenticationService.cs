using Core.DTO.Authentication;
using Core.DTO.User;
using System.Threading.Tasks;

namespace Core.Interfaces.CustomService
{
    public interface IAuthenticationService
    {
        Task ChangePasswordAsync(
            ChangePasswordDTO changePasswordDTO, string userId);
        Task<UserAutorizationDTO> LoginAsync(
            UserLoginDTO userLoginDTO);
        Task LogoutAsync(
            UserLogoutDTO userLogoutDTO);
        Task RegisterClientAsync(
            UserRegistrationDTO userRegistrationDTO);
        Task RegisterCourierAsync(
            UserRegistrationDTO userRegistrationDTO);
        Task ResetPasswordAsync(
            ResetPasswordDTO resetPasswordDTO);
        Task SendResetResetPasswordRequestAsync(
            string email, string callbackUrl);
    }
}
