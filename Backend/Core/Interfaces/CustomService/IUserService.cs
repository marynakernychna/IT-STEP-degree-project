using Core.DTO;
using Core.DTO.Authentication;
using Core.DTO.User;
using Core.Entities;
using Core.Helpers;
using System.Threading.Tasks;

namespace Core.Interfaces.CustomService
{
    public interface IUserService
    {
        Task ChangePasswordAsync(
            string currentPassword, string newPassword,
            string userId);
        Task<bool> CheckIfExistsByEmailAsync(
            string email);
        Task CheckIfExistsByIdAsync(
            string userId);
        Task<bool> CheckPasswordAsync(
            User user, string password);
        Task<User> CreateAsync(
            UserRegistrationDTO userRegistrationDTO,
            string roleName);
        Task<User> GetByEmailAsync(
            string email);
        Task<User> GetByIdAsync(
            string userId);
        Task<string> GetIdByEmailAsync(
            string email);
        Task<PaginatedList<UserProfileInfoDTO>> GetPageOfClientsAsync(
            PaginationFilterDTO paginationFilter);
        Task<PaginatedList<UserProfileInfoDTO>> GetPageOfCouriersAsync(
            PaginationFilterDTO paginationFilter);
        Task<UserProfileInfoDTO> GetProfileAsync(
            string userId);
        Task ResetPasswordAsync(
            string userEmail, string resetToken,
            string newPassword);
        Task UpdateProfileAsync(
            UserEditProfileInfoDTO userEditProfileInfo,
            string userId, string callbackUrl);
    }
}
