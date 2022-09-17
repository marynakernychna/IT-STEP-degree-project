using Core.DTO.Authentication;
using Core.Entities;
using System.Threading.Tasks;

namespace Core.Interfaces.CustomService
{
    public interface ITokenService
    {
        Task DeleteRefreshTokenAsync(
            string refreshToken);
        Task<UserAutorizationDTO> GenerateForUserAsync(
            User user,
            string userRole);
    }
}
