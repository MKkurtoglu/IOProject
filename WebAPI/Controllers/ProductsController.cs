using BusinessLayer.Abstract;
using EntitiesLayer.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        // Dependency Injection için constructor
        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _productService.GetAllAsync();
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpGet("getProductsWithImages")]
        public async Task<IActionResult> GetProductsWithImagesAsync()
        {
            var result = await _productService.GetAllWithImageAsync();
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var result = await _productService.GetProductDtoAsync(id);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpGet("GetByCategory")]
        public async Task<IActionResult> GetAllByCategoryAsync(int categoryId)
        {
            var result = await _productService.GetAllByCategoryAsync(categoryId);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddAsync(Product product)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _productService.AddAsync(product);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpPost("updateProduct")]
        public async Task<IActionResult> UpdateProductAsync(Product product)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _productService.UpdateAsync(product);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpPost("deleteProduct")]
        public async Task<IActionResult> DeleteProductAsync(Product product)
        {
            var result = await _productService.DeleteAsync(product);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
    }
}
