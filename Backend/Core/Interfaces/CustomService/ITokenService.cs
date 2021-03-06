using Core.DTO.Authentication;
using Core.Entities;
using System.Threading.Tasks;

namespace Core.Interfaces.CustomService
{
    public interface ITokenService
    {
        Task<UserAutorizationDTO> GenerateUserTokens(User user, string userRole);
    }
}
