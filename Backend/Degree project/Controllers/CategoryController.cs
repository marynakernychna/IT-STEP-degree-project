using Core.Constants;
using Core.DTO.Category;
using Core.Helpers;
using Core.Interfaces.CustomService;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(
            ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpPost("create")]
        [AuthorizeByRole(IdentityRoleNames.Admin)]
        public async Task<IActionResult> CreateCategoryAsync([FromBody] CreateCategoryDTO createTripDTO)
        {
            await _categoryService.CreateCategoryAsync(createTripDTO);

            return Ok();
        }
    }
}
