using Core.DTO.Characteristic;
using System.Collections.Generic;

namespace Core.Interfaces.CustomService
{
    public interface ICharacteristicService
    {
        void CheckNamesForDuplicates(
            List<CharacteristicWithoutWareIdDTO> сharacteristics);
    }
}
