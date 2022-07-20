using Core.Constants;
using Core.DTO;
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
        public async Task<IActionResult> CreateAsync(
            [FromBody] CategoryDTO createTripDTO)
        {
            await _categoryService.CreateAsync(createTripDTO);

            return Ok();
        }

        [HttpDelete("delete")]
        [AuthorizeByRole(IdentityRoleNames.Admin)]
        public async Task<IActionResult> DeleteAsync(
            [FromBody] CategoryDTO categoryDTO)
        {
            await _categoryService.DeleteAsync(categoryDTO.Title);

            return Ok();
        }

        [HttpGet("all")]
        [AuthorizeByRole(IdentityRoleNames.Admin)]
        public async Task<IActionResult> GetAllAsync(
            [FromQuery] PaginationFilterDTO paginationFilter)
        {
            var categories = await _categoryService.GetAllAsync(paginationFilter);

            return Ok(categories);
        }

        [HttpPut("update")]
        [AuthorizeByRole(IdentityRoleNames.Admin)]
        public async Task<IActionResult> UpdateAsync(
            [FromBody] UpdateCategoryDTO updateCategoryDTO)
        {
            await _categoryService.UpdateAsync(updateCategoryDTO);

            return Ok();
        }
    }
}
