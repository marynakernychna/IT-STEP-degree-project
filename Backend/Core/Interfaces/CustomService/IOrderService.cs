using Core.DTO.Order;
using System.Threading.Tasks;

namespace Core.Interfaces.CustomService
{
    public interface IOrderService
    {
        Task CreateAsync(string userId, CreateOrderDTO createOrderDTO);
    }
}
