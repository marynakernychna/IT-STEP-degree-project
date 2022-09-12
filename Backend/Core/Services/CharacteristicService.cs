using Core.DTO.Characteristic;
using Core.Exceptions;
using Core.Interfaces.CustomService;
using Core.Resources;
using System.Collections.Generic;
using System.Net;

namespace Core.Services
{
    public class CharacteristicService : ICharacteristicService
    {
        public CharacteristicService()
        { }

        public void CheckNamesForDuplicates(
            List<CharacteristicWithoutWareIdDTO> сharacteristics)
        {
            for (int i = 0; i < сharacteristics.Count - 1; i++)
            {
                var characteristic = сharacteristics[i];

                for (int j = i + 1; j < сharacteristics.Count; j++)
                {
                    if (characteristic.Name == сharacteristics[j].Name)
                    {
                        throw new HttpException(
                            ErrorMessages.THE_CHARACTERISTIC_NAME_DUPLICATE,
                            HttpStatusCode.BadRequest);
                    }
                }
            }
        }
    }
}
