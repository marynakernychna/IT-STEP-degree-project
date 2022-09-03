﻿using Core.Constants;
using Core.DTO;
using Core.DTO.Category;
using Core.Helpers;
using Core.Interfaces.CustomService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(
            ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        [AuthorizeByRole(IdentityRoleNames.User)]
        public async Task<IActionResult> GetAllAsync()
        {
            var categories = await _categoryService.GetAllAsync();

            return Ok(categories);
        }

        [HttpGet("admins/page")]
        [AuthorizeByRole(IdentityRoleNames.Admin)]
        public async Task<IActionResult> GetPageAsync(
            [FromQuery] PaginationFilterDTO paginationFilter)
        {
            var categories = await _categoryService.GetAllAsync(paginationFilter);

            return Ok(categories);
        }

        [HttpPost("admins/create")]
        [AuthorizeByRole(IdentityRoleNames.Admin)]
        public async Task<IActionResult> CreateAsync(
            [FromBody] CategoryDTO createTripDTO)
        {
            await _categoryService.CreateAsync(createTripDTO);

            return Ok();
        }

        [HttpPut("admins/update")]
        [AuthorizeByRole(IdentityRoleNames.Admin)]
        public async Task<IActionResult> UpdateAsync(
            [FromBody] UpdateCategoryDTO updateCategoryDTO)
        {
            await _categoryService.UpdateAsync(updateCategoryDTO);

            return Ok();
        }

        [HttpDelete("admins/delete")]
        [AuthorizeByRole(IdentityRoleNames.Admin)]
        public async Task<IActionResult> DeleteAsync(
            [FromQuery] CategoryDTO categoryDTO)
        {
            await _categoryService.DeleteAsync(categoryDTO.Title);

            return Ok();
        }
    }
}
