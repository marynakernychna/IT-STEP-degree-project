using Core.DTO.Ware;
using System.Threading.Tasks;

namespace Core.Interfaces.CustomService
{
    public interface IWareService
    {
        Task CreateAsync(CreateWareDTO createWareDTO, string userId);
    }
}
