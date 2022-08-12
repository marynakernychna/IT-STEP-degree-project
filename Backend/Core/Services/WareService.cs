using Core.Interfaces.CustomService;
using System.Threading.Tasks;
using Core.DTO.Ware;
using Core.Interfaces;
using Core.Entities;
using Core.Specifications;
using Core.Helpers;
using Core.Exceptions;
using System.Net;
using Core.Resources;
using System.Collections.Generic;

namespace Core.Services
{
    public class WareService : IWareService
    {
        private readonly IFileService _fileService;
        private readonly ICharacteristicService _characteristicService;
        private readonly IRepository<Ware> _wareRepository;
        private readonly IRepository<Category> _categoryRepository;
        private readonly IRepository<Characteristic> _characteristicRepository;

        public WareService(
            IFileService fileService,
            ICharacteristicService characteristicService,
            IRepository<Ware> wareRepository,
            IRepository<Category> categoryRepository,
            IRepository<Characteristic> characteristicRepository)
        {
            _fileService = fileService;
            _characteristicService = characteristicService;
            _wareRepository = wareRepository;
            _categoryRepository = categoryRepository;
            _characteristicRepository = characteristicRepository;
        }

        public async Task CreateAsync(CreateWareDTO createWareDTO, string userId)
        {
            _characteristicService.CheckCharacteristicNames(createWareDTO.Characteristics);

            var category = await _categoryRepository.SingleOrDefaultAsync(
            new CategorySpecification.GetByTitle(createWareDTO.CategoryTitle));

            ExtensionMethods.CategoryNullCheck(category);

            var wareDuplicate = await _wareRepository.SingleOrDefaultAsync(
                new WareSpecification.GetByTitleAndCreatorId(createWareDTO.Title, userId));

            if (wareDuplicate != null)
            {
                throw new HttpException(
                    ErrorMessages.WareTitleDuplicateByUser,
                    HttpStatusCode.BadRequest);
            }

            var fileName = _fileService.CreateWarePhotoFile(
                createWareDTO.PhotoBase64, createWareDTO.PhotoExtension);

            var ware = await _wareRepository.AddAsync(
                new Ware
                {
                    Title = createWareDTO.Title,
                    Description = createWareDTO.Description,
                    Cost = createWareDTO.Cost,
                    PhotoLink = fileName,
                    AvailableCount = createWareDTO.AvailableCount,
                    CategoryId = category.Id,
                    CreatorId = userId
                });

            var characteristics = new List<Characteristic>();

            foreach (var characteristic in createWareDTO.Characteristics)
            {
                characteristics.Add(
                    new Characteristic
                    {
                        Name = characteristic.Name,
                        Value = characteristic.Value,
                        WareId = ware.Id
                    });
            }

            await _characteristicRepository.AddRangeAsync(characteristics);
        }
    }
}
