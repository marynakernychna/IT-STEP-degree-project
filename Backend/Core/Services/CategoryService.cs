﻿using AutoMapper;
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
        // If we replace it with an appropriate service, it will cause an initialization loop.
        private readonly IRepository<Ware> _wareRepository;

        private readonly IMapper _mapper;

        public CategoryService(
            IRepository<Category> categoryRepository,
            IRepository<Ware> wareRepository,
            IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _wareRepository = wareRepository;
            _mapper = mapper;
        }

        public async Task<bool> CheckIfExistsByTitleAsync(
            string title)
        {
            return await _categoryRepository.AnyAsync(
                new CategorySpecification.GetByTitle(title));
        }

        public async Task CreateAsync(
            CategoryDTO categoryDTO)
        {
            if (await _categoryRepository.AnyAsync(
                    new CategorySpecification.GetByTitle(categoryDTO.Title)))
            {
                throw new HttpException(
                        ErrorMessages.THE_CATEGORY_ALREADY_EXISTS,
                        HttpStatusCode.BadRequest
                    );
            }

            await _categoryRepository.AddAsync(
                _mapper.Map<Category>(categoryDTO));
        }

        public async Task DeleteAsync(
            string categoryTitle)
        {
            var category = await _categoryRepository.SingleOrDefaultAsync(
                new CategorySpecification.GetByTitle(categoryTitle));

            ExtensionMethods.CategoryNullCheck(category);

            var wares = await _wareRepository.ListAsync(
                new WareSpecification.GetAllByCategoryTitle(categoryTitle));

            foreach (var ware in wares)
            {
                ware.CategoryId = Constants.Constants.NO_CATEGORY_ID;
            }

            await _wareRepository.UpdateRangeAsync(wares);

            await _categoryRepository.DeleteAsync(category);
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

        public async Task<int> GetIdByTitleAsync(
            string title)
        {
            var category = await _categoryRepository.SingleOrDefaultAsync(
                new CategorySpecification.GetByTitle(title));

            ExtensionMethods.CategoryNullCheck(category);

            return category.Id;
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

        public async Task UpdateAsync(
            UpdateCategoryDTO updateCategoryDTO)
        {
            if (String.Equals(updateCategoryDTO.CurrentTitle, updateCategoryDTO.NewTitle))
            {
                throw new HttpException(
                    ErrorMessages.THE_PREVIOUS_INFO_IS_THE_SAME,
                    HttpStatusCode.BadRequest);
            }

            if (await _categoryRepository.AnyAsync(
                    new CategorySpecification.GetByTitle(updateCategoryDTO.NewTitle)))
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
