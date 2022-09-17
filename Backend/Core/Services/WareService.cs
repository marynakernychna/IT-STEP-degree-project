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
        private readonly IRepository<Ware> _wareRepository;

        private readonly ICategoryService _categoryService;
        private readonly ICharacteristicService _characteristicService;
        private readonly IFileService _fileService;

        private readonly IMapper _mapper;

        public WareService(
            IRepository<Ware> wareRepository,
            ICategoryService categoryService,
            ICharacteristicService characteristicService,
            IFileService fileService,
            IMapper mapper)
        {
            _wareRepository = wareRepository;
            _categoryService = categoryService;
            _characteristicService = characteristicService;
            _fileService = fileService;
            _mapper = mapper;
        }

        public async Task CheckIfExistsByIdAsync(
            int wareId)
        {
            ExtensionMethods.WareNullCheck(
                await _wareRepository.GetByIdAsync(wareId));
        }

        public async Task CreateAsync(
            CreateWareDTO createWareDTO,
            string userId)
        {
            var categoryId = await _categoryService
                .GetIdByTitleAsync(createWareDTO.CategoryTitle);

            CharacteristicService
                .CheckNamesForDuplicates(createWareDTO.Characteristics);

            var wareDuplicate = await _wareRepository.SingleOrDefaultAsync(
                new WareSpecification.GetByTitleAndCreatorId(createWareDTO.Title, userId));

            if (wareDuplicate != null)
            {
                throw new HttpException(
                    ErrorMessages.DUPLICATE_WARE_TITLE_BY_THE_USER,
                    HttpStatusCode.BadRequest);
            }

            var fileName = _fileService.CreateWarePhotoFile(
                createWareDTO.PhotoBase64,
                createWareDTO.PhotoExtension);

            var ware = await _wareRepository.AddAsync(
                new Ware
                {
                    Title = createWareDTO.Title,
                    Description = createWareDTO.Description,
                    Cost = createWareDTO.Cost,
                    PhotoLink = fileName,
                    AvailableCount = createWareDTO.AvailableCount,
                    CategoryId = categoryId,
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

            await _characteristicService.AddRangeAsync(characteristics);
        }

        public async Task<PaginatedList<WareBriefInfoDTO>> GetAllAsync(
            PaginationFilterDTO paginationFilter)
        {
            var waresCount = await _wareRepository.CountAsync(
                new WareSpecification.GetPage(paginationFilter));

            if (waresCount == 0)
            {
                return null;
            }

            return FormPaginatedList(
                await _wareRepository.ListAsync(
                    new WareSpecification.GetPage(paginationFilter)),
                waresCount,
                paginationFilter.PageNumber,
                PaginatedList<WareInfoDTO>
                    .GetTotalPages(paginationFilter, waresCount));
        }

        public async Task<PaginatedList<WareBriefInfoDTO>> GetByCategoryAsync(
            PaginationFilterWareDTO paginationFilter)
        {
            if (!await _categoryService
                    .CheckIfExistsByTitleAsync(paginationFilter.CategoryTitle))
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

            return FormPaginatedList(
                await _wareRepository.ListAsync(
                    new WareSpecification.GetByCategory(paginationFilter)),
                waresCount,
                paginationFilter.PageNumber,
                PaginatedList<WareInfoDTO>
                    .GetTotalPages(paginationFilterDTO, waresCount));
        }

        public async Task<Ware> GetByIdAsync(
            int id)
        {
            var ware = await _wareRepository.GetByIdAsync(id);

            ExtensionMethods.WareNullCheck(ware);

            return ware;
        }

        public async Task<PaginatedList<WareBriefInfoDTO>> GetCreatedByUserAsync(
            string userId,
            PaginationFilterDTO paginationFilter)
        {
            var waresCount = await _wareRepository.CountAsync(
                new WareSpecification.GetByCreatorId(paginationFilter, userId));

            if (waresCount == 0)
            {
                return null;
            }

            return FormPaginatedList(
                await _wareRepository.ListAsync(
                    new WareSpecification.GetByCreatorId(paginationFilter, userId)),
                waresCount,
                paginationFilter.PageNumber,
                PaginatedList<WareInfoDTO>
                    .GetTotalPages(paginationFilter, waresCount));
        }

        private PaginatedList<WareBriefInfoDTO> FormPaginatedList(
            List<Ware> wares,
            int waresCount,
            int pageNumber,
            int totalPages)
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
