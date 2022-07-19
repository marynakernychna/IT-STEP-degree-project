using Core.DTO;
using Core.DTO.Category;
using Core.Helpers;
using System.Threading.Tasks;

namespace Core.Interfaces.CustomService
{
    public interface ICategoryService
    {
        Task<PaginatedList<CategoryDTO>> GetAllAsync(
            PaginationFilterDTO paginationFilter);
        Task CreateAsync(CategoryDTO categoryDTO);
        Task DeleteAsync(string categoryTitle);
    }
}
