using Core.DTO.Characteristic;
using System.Collections.Generic;

namespace Core.Interfaces.CustomService
{
    public interface ICharacteristicService
    {
        void CheckCharacteristicNames(
            List<CharacteristicWithoutWareIdDTO> сharacteristics);
    }
}
