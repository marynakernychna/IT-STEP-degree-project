using AutoMapper;
using Core.DTO.Category;
using Core.Entities;
using Core.Exceptions;
using Core.Helpers;
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

            await _categoryRepository.AddAsync(category);
        }

        public async Task DeleteAsync(int categoryId)
        {
            var category = await _categoryRepository.GetByIdAsync(categoryId);

            ExtensionMethods.CategoryNullCheck(category);

            await _categoryRepository.DeleteAsync(category);
        }
    }
}
