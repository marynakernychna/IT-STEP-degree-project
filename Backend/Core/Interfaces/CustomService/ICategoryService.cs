using Core.DTO.Category;
using System.Threading.Tasks;

namespace Core.Interfaces.CustomService
{
    public interface ICategoryService
    {
        Task CreateCategoryAsync(CreateCategoryDTO createTripDTO);
    }
}
