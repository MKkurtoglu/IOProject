using BusinessLayer.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EntityImagesController : ControllerBase
    {
        private readonly IImageService _imageService;

        public EntityImagesController(IImageService imageService)
        {
            _imageService = imageService;
        }

        [HttpPost("addImage")]
        public async Task<IActionResult> AddImageAsync([FromForm] IFormFile formFile, [FromForm] string entityId, [FromForm] string entityType)
        {
            if (formFile == null)
                return BadRequest(new { message = "File cannot be null" });

            var id = int.Parse(entityId);
            var result = await _imageService.AddImageAsync(id, entityType, formFile, true);

            if (result == null)
                return BadRequest(new { message = "Result cannot be null" });

            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpGet("getAllImage")]
        public async Task<IActionResult> GetAllImageByEntityIdAsync(string entityId, string entityType)
        {
            var id = int.Parse(entityId);
            var result = await _imageService.GetAllImageByEntityAsync(id, entityType);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpPost("updateImage")]
        public async Task<IActionResult> UpdateImageAsync([FromForm] IFormFile formFile, [FromForm] string imageId, string entityType)
        {
            var id = int.Parse(imageId);
            var imageResult = await _imageService.GetEntityImageByImageIdAsync(id, entityType);

            if (!imageResult.IsSuccess)
                return BadRequest(new { message = "Image not found" });

            var result = await _imageService.UpdateImageAsync(imageResult.Data, formFile);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpPost("deleteImage")]
        public async Task<IActionResult> DeleteImageAsync([FromQuery] int imageId, [FromQuery] string entityType)
        {
            var imageResult = await _imageService.GetEntityImageByImageIdAsync(imageId, entityType);

            if (!imageResult.IsSuccess)
                return BadRequest(new { message = "Image not found" });

            var result = await _imageService.DeleteAsync(imageResult.Data);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
    }
}
