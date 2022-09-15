using Core.DTO;
using Core.DTO.Category;
using Core.Helpers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Interfaces.CustomService
{
    public interface ICategoryService
    {
        Task CreateAsync(
            CategoryDTO categoryDTO);
        Task DeleteAsync(
            string categoryTitle);
        Task<List<CategoryDTO>> GetAllAsync();
        Task<PaginatedList<CategoryInfoDTO>> GetPageAsync(
            PaginationFilterDTO paginationFilter);
        Task UpdateAsync(
            UpdateCategoryDTO updateCategoryDTO);
    }
}
