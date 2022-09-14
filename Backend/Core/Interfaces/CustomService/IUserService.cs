using Core.DTO;
using Core.DTO.User;
using Core.Helpers;
using System.Threading.Tasks;

namespace Core.Interfaces.CustomService
{
    public interface IUserService
    {
        Task<string> GetIdByEmailAsync(
            string email);
        Task<PaginatedList<UserProfileInfoDTO>> GetPageOfCouriersAsync(
            PaginationFilterDTO paginationFilter);
        Task<PaginatedList<UserProfileInfoDTO>> GetPageOfClientsAsync(
            PaginationFilterDTO paginationFilter);
        Task<UserProfileInfoDTO> GetProfileAsync(
            string userId);
        Task UpdateProfileAsync(
            UserEditProfileInfoDTO userEditProfileInfo,
            string userId, string callbackUrl);
    }
}
