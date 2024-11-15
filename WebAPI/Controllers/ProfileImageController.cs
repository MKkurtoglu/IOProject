using BusinessLayer.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileImageController : ControllerBase
    {
        private readonly IProfileImageService _profileImageService;

        public ProfileImageController(IProfileImageService profileImageService)
        {
            _profileImageService = profileImageService;
        }

        [HttpGet("getProfileImage")]
        public async Task<IActionResult> GetProfileImageAsync()
        {
            var result = await _profileImageService.GetAllImageByUserAsync();
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpPost("addProfileImage")]
        public async Task<IActionResult> AddProfileImageAsync([FromForm] IFormFile file)
        {
            if (file == null)
                return BadRequest(new { message = "File cannot be null" });

            if (!ValidateImageFile(file))
                return BadRequest(new { message = "Invalid file format or size" });

            var result = await _profileImageService.AddProfileImageAsync(file);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpPost("updateProfileImage")]
        public async Task<IActionResult> UpdateProfileImageAsync([FromForm] int id, [FromForm] IFormFile file)
        {
            if (file == null)
                return BadRequest(new { message = "File cannot be null" });

            if (!ValidateImageFile(file))
                return BadRequest(new { message = "Invalid file format or size" });

            var profileImage = await _profileImageService.GetByIdAsync(id);
            if (!profileImage.IsSuccess)
                return NotFound(new { message = $"Profile image with id {id} not found." });

            var result = await _profileImageService.UpdateImageAsync(profileImage.Data, file);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpPost("deleteCarImage")]
        public async Task<IActionResult> DeleteProfileImageAsync()
        {
            var profileImage = await _profileImageService.GetAllImageByUserAsync();
            if (!profileImage.IsSuccess)
                return NotFound(new { message = "No profile image found." });

            var result = await _profileImageService.DeleteAsync(profileImage.Data);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        private bool ValidateImageFile(IFormFile file)
        {
            // İzin verilen dosya türleri
            var allowedTypes = new[] { "image/jpeg", "image/jpg", "image/png" };

            // Dosya türü kontrolü
            if (!allowedTypes.Contains(file.ContentType.ToLower()))
                return false;

            // Dosya boyutu kontrolü (örneğin 5MB)
            if (file.Length > 5 * 1024 * 1024)
                return false;

            return true;
        }
    }
}
