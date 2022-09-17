using Core.DTO.Characteristic;
using Core.Entities;
using Core.Exceptions;
using Core.Interfaces;
using Core.Interfaces.CustomService;
using Core.Resources;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Core.Services
{
    public class CharacteristicService : ICharacteristicService
    {
        private readonly IRepository<Characteristic> _characteristicRepository;

        public CharacteristicService(
            IRepository<Characteristic> characteristicRepository)
        {
            _characteristicRepository = characteristicRepository;
        }

        public static void CheckNamesForDuplicates(
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

        public async Task AddRangeAsync(
            List<Characteristic> сharacteristics)
        {
            await _characteristicRepository.AddRangeAsync(сharacteristics);
        }
    }
}
