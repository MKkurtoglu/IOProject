using BusinessLayer.Abstract;
using EntitiesLayer.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _categoryService.GetAllCategoryDtoAsync();
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpGet("getCategoryById")]
        public async Task<IActionResult> GetCategoryAsync(int id)
        {
            var result = await _categoryService.GetCategoryDtoAsync(id);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpPost("addCategory")]
        public async Task<IActionResult> AddCategoryAsync(Category category)
        {
            var result = await _categoryService.AddAsync(category);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpPost("updateCategory")]
        public async Task<IActionResult> UpdateCategoryAsync(Category category)
        {
            var result = await _categoryService.UpdateAsync(category);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpPost("deleteCategory")]
        public async Task<IActionResult> DeleteCategoryAsync(int id)
        {
            var categoryResult = await _categoryService.GetByIdAsync(id);
            if (!categoryResult.IsSuccess)
                return BadRequest(categoryResult);

            var deleteResult = await _categoryService.DeleteAsync(categoryResult.Data);
            return deleteResult.IsSuccess ? Ok(deleteResult) : BadRequest(deleteResult);
        }
    }
}
