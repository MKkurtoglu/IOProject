using BusinessLayer.Abstract;
using EntitiesLayer.Concrete;
using EntitiesLayer.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModelsController : ControllerBase
    {
        private readonly IModelService _modelService;

        public ModelsController(IModelService modelService)
        {
            _modelService = modelService;
        }

        [HttpGet("getAllModels")]
        public async Task<IActionResult> GetAllModelsAsync()
        {
            var result = await _modelService.GetAllAsync();
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpGet("getAllModelsWithBrand")]
        public async Task<IActionResult> GetAllModelsWithBrandAsync()
        {
            var result = await _modelService.GetAllModelWithBrandAsync();
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpGet("getModelById")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var result = await _modelService.GetByIdAsync(id);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpGet("getAllModelByBrand")]
        public async Task<IActionResult> GetAllModelByBrandAsync(int brandId)
        {
            var result = await _modelService.GetModelByBrandAsync(brandId);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpPost("addModel")]
        public async Task<IActionResult> AddModelAsync(ModelAddDto modelAddDto)
        {
            var model = new Model
            {
                ModelName = modelAddDto.ModelName,
                BrandId = modelAddDto.BrandId
            };

            var result = await _modelService.AddAsync(model);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpPost("deleteModel")]
        public async Task<IActionResult> DeleteModelAsync(int id)
        {
            var modelResult = await _modelService.GetByIdAsync(id);
            if (!modelResult.IsSuccess)
                return NotFound(new { message = "Model bulunamadı" });

            var result = await _modelService.DeleteAsync(modelResult.Data);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpPost("updateModel")]
        public async Task<IActionResult> UpdateModelAsync(ModelUpdateDto modelDto)
        {
            var modelResult = await _modelService.GetByIdAsync(modelDto.ModelId);
            if (!modelResult.IsSuccess)
                return NotFound(new { message = "Güncellenecek model bulunamadı" });

            var model = modelResult.Data;
            model.ModelId = modelDto.ModelId;
            model.ModelName = modelDto.ModelName;
            model.BrandId = modelDto.BrandId;

            var result = await _modelService.UpdateAsync(model);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
    }
}

