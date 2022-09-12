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
using System;
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

        public async Task CreateAsync(
            CategoryDTO categoryDTO)
        {
            var isCategoryExist = await _categoryRepository.AnyAsync(
                new CategorySpecification.GetByTitle(categoryDTO.Title));

            if (isCategoryExist)
            {
                throw new HttpException(
                        ErrorMessages.THE_CATEGORY_ALREADY_EXISTS,
                        HttpStatusCode.BadRequest
                    );
            }

            var category = _mapper.Map<Category>(categoryDTO);

            await _categoryRepository.AddAsync(category);
        }

        public async Task<List<CategoryDTO>> GetAllAsync()
        {
            var categories = await _categoryRepository.ListAsync(
                new CategorySpecification.GetAll());

            if (categories.Count == 0)
            {
                return null;
            }

            return _mapper.Map<List<CategoryDTO>>(categories);
        }

        public async Task<PaginatedList<CategoryInfoDTO>> GetPageAsync(
            PaginationFilterDTO paginationFilter)
        {
            var categoriesCount = await _categoryRepository.CountAsync(
                new CategorySpecification.GetAll(paginationFilter));

            int totalPages = PaginatedList<CategoryInfoDTO>
                .GetTotalPages(paginationFilter, categoriesCount);

            if (totalPages == 0)
            {
                return null;
            }

            var categories = await _categoryRepository.ListAsync(
                new CategorySpecification.GetAll(paginationFilter));

            var result = new List<CategoryInfoDTO>();

            foreach (var category in categories)
            {
                int availableTotalCount = 0;

                foreach (var ware in category.Wares)
                {
                    availableTotalCount += ware.AvailableCount;
                }

                result.Add(new CategoryInfoDTO
                {
                    Title = category.Title,
                    GoodsTotalCount = category.Wares.Count,
                    AvailableTotalCount = availableTotalCount
                });
            }

            return PaginatedList<CategoryInfoDTO>.Evaluate(
                result,
                paginationFilter.PageNumber,
                categoriesCount,
                totalPages);
        }

        public async Task DeleteAsync(
            string categoryTitle)
        {
            var category = await _categoryRepository.SingleOrDefaultAsync(
                new CategorySpecification.GetByTitle(categoryTitle));

            ExtensionMethods.CategoryNullCheck(category);

            await _categoryRepository.DeleteAsync(category);
        }

        public async Task UpdateAsync(
            UpdateCategoryDTO updateCategoryDTO)
        {
            if (String.Equals(updateCategoryDTO.CurrentTitle, updateCategoryDTO.NewTitle))
            {
                throw new HttpException(
                    ErrorMessages.THE_PREVIOUS_INFO_IS_THE_SAME,
                    HttpStatusCode.BadRequest);
            }

            var isCategoryExist = await _categoryRepository.AnyAsync(
                new CategorySpecification.GetByTitle(updateCategoryDTO.NewTitle));

            if (isCategoryExist)
            {
                throw new HttpException(
                    ErrorMessages.THE_CATEGORY_ALREADY_EXISTS,
                    HttpStatusCode.BadRequest);
            }

            var categoryToUpdate = await _categoryRepository.SingleOrDefaultAsync(
                new CategorySpecification.GetByTitle(updateCategoryDTO.CurrentTitle));

            ExtensionMethods.CategoryNullCheck(categoryToUpdate);

            categoryToUpdate.Title = updateCategoryDTO.NewTitle;

            await _categoryRepository.UpdateAsync(categoryToUpdate);
        }
    }
}
