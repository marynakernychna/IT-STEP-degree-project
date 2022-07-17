using AutoMapper;
using Core.DTO.Category;
using Core.Entities;
using Core.Exceptions;
using Core.Helpers;
using Core.Interfaces;
using Core.Interfaces.CustomService;
using Core.Resources;
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
            if (createTripDTO != null)
            {
                var category = _mapper.Map<Category>(createTripDTO);
                category.Wares = null;

                var categoryFromDb = await _categoryRepository.AddAsync(category);

                ExtensionMethods.CategoryNullCheck(categoryFromDb);
            }
            else
            {
                throw new HttpException(ErrorMessages.CategoryNotFound, HttpStatusCode.BadRequest);
            }
        }
    }
}
