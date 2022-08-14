using Core.DTO;
using Core.DTO.Category;
using Core.Helpers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Interfaces.CustomService
{
    public interface ICategoryService
    {
        Task<PaginatedList<CategoryInfoDTO>> GetAllAsync(
            PaginationFilterDTO paginationFilter);
        Task<List<CategoryDTO>> GetAllAsync();
        Task CreateAsync(CategoryDTO categoryDTO);
        Task UpdateAsync(UpdateCategoryDTO updateCategoryDTO);
        Task DeleteAsync(string categoryTitle);
    }
}
