using Core.Entities;
using System.Threading.Tasks;

namespace Core.Interfaces.CustomService
{
    public interface IIdentityRoleService
    {
        Task<string> GetByUserAsync(
            User user);
    }
}
