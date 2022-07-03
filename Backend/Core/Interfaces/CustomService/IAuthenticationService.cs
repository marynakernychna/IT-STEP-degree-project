using Core.DTO.Authentication;
using System.Threading.Tasks;

namespace Core.Interfaces.CustomService
{
    public interface IAuthenticationService
    {
        Task RegisterAsync(UserRegistrationDTO userRegistrationDTO);
        Task<UserAutorizationDTO> LoginAsync(UserLoginDTO userLoginDTO);
    }
}
