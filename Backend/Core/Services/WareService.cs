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
using Core.DTO;
using AutoMapper;
using Core.DTO.Characteristic;
using Core.DTO.PaginationFilter;

namespace Core.Services
{
    public class WareService : IWareService
    {
        private readonly IRepository<Category> _categoryRepository;
        private readonly IRepository<Characteristic> _characteristicRepository;
        private readonly IRepository<Ware> _wareRepository;

        private readonly ICharacteristicService _characteristicService;
        private readonly IFileService _fileService;

        private readonly IMapper _mapper;

        public WareService(
            IRepository<Category> categoryRepository,
            IRepository<Characteristic> characteristicRepository,
            IRepository<Ware> wareRepository,
            ICharacteristicService characteristicService,
            IFileService fileService,
            IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _characteristicRepository = characteristicRepository;
            _wareRepository = wareRepository;
            _characteristicService = characteristicService;
            _fileService = fileService;
            _mapper = mapper;
        }

        public async Task CheckIfExistsByIdAsync(
            int wareId)
        {
            var ware = await _wareRepository.GetByIdAsync(wareId);

            ExtensionMethods.WareNullCheck(ware);
        }

        public async Task CreateAsync(
            CreateWareDTO createWareDTO, string userId)
        {
            _characteristicService.CheckNamesForDuplicates(createWareDTO.Characteristics);

            var category = await _categoryRepository.SingleOrDefaultAsync(
            new CategorySpecification.GetByTitle(createWareDTO.CategoryTitle));

            ExtensionMethods.CategoryNullCheck(category);

            var wareDuplicate = await _wareRepository.SingleOrDefaultAsync(
                new WareSpecification.GetByTitleAndCreatorId(createWareDTO.Title, userId));

            if (wareDuplicate != null)
            {
                throw new HttpException(
                    ErrorMessages.DUPLICATE_WARE_TITLE_BY_THE_USER,
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

        public async Task<PaginatedList<WareBriefInfoDTO>> GetAllAsync(
            PaginationFilterDTO paginationFilter)
        {
            var waresCount = await _wareRepository.CountAsync(
                new WareSpecification.GetAll(paginationFilter));

            if (waresCount == 0)
            {
                return null;
            }

            var totalPages = PaginatedList<WareInfoDTO>
                .GetTotalPages(paginationFilter, waresCount);

            var wares = await _wareRepository.ListAsync(
                new WareSpecification.GetAll(paginationFilter));

            return FormPaginatedList(
                wares,
                waresCount,
                paginationFilter.PageNumber,
                totalPages);
        }

        public async Task<PaginatedList<WareBriefInfoDTO>> GetByCategoryAsync(
            PaginationFilterWareDTO paginationFilter)
        {
            if (!await _categoryRepository.AnyAsync(
               new CategorySpecification.GetByTitle(paginationFilter.CategoryTitle)))
            {
                throw new HttpException(
                    ErrorMessages.THE_CATEGORY_NOT_FOUND,
                    HttpStatusCode.NotFound);
            }

            var waresCount = await _wareRepository.CountAsync(
                new WareSpecification.GetByCategory(paginationFilter));

            if (waresCount == 0)
            {
                return null;
            }

            var paginationFilterDTO = new PaginationFilterDTO()
            {
                PageNumber = paginationFilter.PageNumber,
                PageSize = paginationFilter.PageSize
            };

            var totalPages = PaginatedList<WareInfoDTO>
                .GetTotalPages(paginationFilterDTO, waresCount);

            var wares = await _wareRepository.ListAsync(
                new WareSpecification.GetByCategory(paginationFilter));

            return FormPaginatedList(
                wares,
                waresCount,
                paginationFilter.PageNumber,
                totalPages);
        }

        public async Task<Ware> GetByIdAsync(
            int id)
        {
            var ware = await _wareRepository.GetByIdAsync(id);

            ExtensionMethods.WareNullCheck(ware);

            return ware;
        }

        public async Task<PaginatedList<WareBriefInfoDTO>> GetCreatedByUserAsync(
            string userId, PaginationFilterDTO paginationFilter)
        {
            var wares = await _wareRepository.ListAsync(
                new WareSpecification.GetByCreatorId(paginationFilter, userId));

            var waresCount = await _wareRepository.CountAsync(
                new WareSpecification.GetByCreatorId(paginationFilter, userId));

            if (waresCount == 0)
            {
                return null;
            }

            var totalPages = PaginatedList<WareInfoDTO>
                .GetTotalPages(paginationFilter, waresCount);

            return FormPaginatedList(
                wares,
                waresCount,
                paginationFilter.PageNumber,
                totalPages);
        }

        private PaginatedList<WareBriefInfoDTO> FormPaginatedList(
            List<Ware> wares, int waresCount, int pageNumber, int totalPages)
        {
            var wareDTOs = new List<WareBriefInfoDTO>();

            foreach (var ware in wares)
            {
                wareDTOs.Add(
                    new WareBriefInfoDTO
                    {
                        Id = ware.Id,
                        Title = ware.Title,
                        Cost = ware.Cost,
                        PhotoBase64 = _fileService.GenereteBase64(ware.PhotoLink),
                        CategoryTitle = ware.Category.Title
                    });
            }

            return PaginatedList<WareBriefInfoDTO>.Evaluate(
                wareDTOs,
                pageNumber,
                waresCount,
                totalPages);
        }

        public async Task<WareInfoDTO> FormWareInfoDTOByIdAsync(
            int id)
        {
            var ware = await _wareRepository.SingleOrDefaultAsync(
                new WareSpecification.GetById(id));

            ExtensionMethods.WareNullCheck(ware);

            return new WareInfoDTO
            {
                Title = ware.Title,
                Description = ware.Description,
                CreatorFullName = ware.Creator.Name + ' ' + ware.Creator.Surname,
                Cost = ware.Cost,
                PhotoBase64 = _fileService.GenereteBase64(ware.PhotoLink),
                AvailableCount = ware.AvailableCount,
                CreationDate = ware.CreationDate,
                CategoryTitle = ware.Category.Title,
                Characteristics = _mapper
                    .Map<List<CharacteristicWithoutWareIdDTO>>(ware.Characteristics)
            };
        }
    }
}
