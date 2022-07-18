using AutoMapper;
using Core.DTO;
using Core.DTO.Category;
using Core.Entities;
using Core.Exceptions;
using Core.Helpers;
using Core.Interfaces;
using Core.Interfaces.CustomService;
using Core.Resources;
using Core.Specifications;
using System.Collections.Generic;
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

        public async Task CreateAsync(string categoryTitle)
        {
            var isCategoryExist = await _categoryRepository.AnyAsync(
                new CategorySpecification.GetByTitle(categoryTitle));

            if (isCategoryExist)
            {
                throw new HttpException(
                        ErrorMessages.CategoryAlreadyExists,
                        HttpStatusCode.BadRequest
                    );
            }

            var category = _mapper.Map<Category>(categoryTitle);

            await _categoryRepository.AddAsync(category);
        }

        public async Task<PaginatedList<CategoryDTO>> GetAllAsync(
            PaginationFilterDTO paginationFilter)
        {
            var categoriesCount = await _categoryRepository.CountAsync(
                new CategorySpecification.GetAll(paginationFilter));

            int totalPages = PaginatedList<CategoryDTO>
                .GetTotalPages(paginationFilter, categoriesCount);

            if (totalPages == 0)
            {
                return null;
            }

            var categories = await _categoryRepository.ListAsync(
                new CategorySpecification.GetAll(paginationFilter));

            return PaginatedList<CategoryDTO>.Evaluate(
                _mapper.Map<List<CategoryDTO>>(categories),
                paginationFilter.PageNumber,
                categoriesCount,
                totalPages);
        }
    }
}
