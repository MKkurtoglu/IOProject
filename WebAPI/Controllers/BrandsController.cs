using BusinessLayer.Abstract;
using EntitiesLayer.Concrete;
using EntitiesLayer.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandsController : ControllerBase
    {
        private readonly IBrandService _brandService;

        public BrandsController(IBrandService brandService)
        {
            _brandService = brandService;
        }

        [HttpGet("getAllBrands")]
        public async Task<IActionResult> GetAllBrandsAsync()
        {
            var result = await _brandService.GetAllAsync();
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpGet("getBrandById")]
        public async Task<IActionResult> GetBrandAsync(int id)
        {
            var result = await _brandService.GetByIdAsync(id);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpPost("addBrand")]
        public async Task<IActionResult> AddBrandAsync(BrandDto brandDto)
        {
            var brand = new Brand
            {
                BrandName = brandDto.BrandName
            };

            var result = await _brandService.AddAsync(brand);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpPost("updateBrand")]
        public async Task<IActionResult> UpdateBrandAsync(BrandDto brandDto)
        {
            var brandResult = await _brandService.GetByIdAsync(brandDto.BrandId);
            if (!brandResult.IsSuccess)
                return BadRequest(brandResult);

            var brand = brandResult.Data;
            brand.BrandId = brandDto.BrandId;
            brand.BrandName = brandDto.BrandName;

            var updateResult = await _brandService.UpdateAsync(brand);
            return updateResult.IsSuccess ? Ok(updateResult) : BadRequest(updateResult);
        }

        [HttpPost("deleteBrand")]
        public async Task<IActionResult> DeleteBrandAsync(int id)
        {
            var brandResult = await _brandService.GetByIdAsync(id);
            if (!brandResult.IsSuccess)
                return BadRequest(brandResult);

            var deleteResult = await _brandService.DeleteAsync(brandResult.Data);
            return deleteResult.IsSuccess ? Ok(deleteResult) : BadRequest(deleteResult);
        }
    }
}
