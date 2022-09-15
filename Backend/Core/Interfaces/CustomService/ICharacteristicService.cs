using Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Interfaces.CustomService
{
    public interface ICharacteristicService
    {
        Task AddRangeAsync(
            List<Characteristic> сharacteristics);
    }
}
