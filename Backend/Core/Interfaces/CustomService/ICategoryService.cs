using Core.DTO;
using Core.DTO.Category;
using Core.Helpers;
using System.Threading.Tasks;

namespace Core.Interfaces.CustomService
{
    public interface ICategoryService
    {
        Task CreateCategoryAsync(CreateCategoryDTO createTripDTO);
        Task DeleteAsync(int categoryId);
        Task CreateAsync(string categoryTitle);
        Task<PaginatedList<CategoryDTO>> GetAllAsync(PaginationFilterDTO paginationFilter);
    }
}
