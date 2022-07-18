using AutoMapper;
using Core.DTO.Category;
using Core.Entities;
using Core.Exceptions;
using Core.Interfaces;
using Core.Interfaces.CustomService;
using Core.Resources;
using Core.Specifications;
using System.Net;
using System.Threading.Tasks;

namespace Core.Services
{
    internal class CategoryService : ICategoryService
    {
        private readonly IRepository<Category> _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryService(
            IRepository<Category> categoryRepository,
            IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task CreateCategoryAsync(CreateCategoryDTO createTripDTO)
        {
            var isCategoryExist = await _categoryRepository.AnyAsync(
                new CategorySpecification.GetByTitle(createTripDTO.Title));

            if (isCategoryExist)
            {
                throw new HttpException(
                    ErrorMessages.CategoryAlreadyExists, 
                    HttpStatusCode.BadRequest
                    );
                
            }

            var category = _mapper.Map<Category>(createTripDTO);
            category.Wares = null;

            await _categoryRepository.AddAsync(category);

        }
    }
}
