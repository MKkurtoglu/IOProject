using EntitiesLayer.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CorporateController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public CorporateController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Hakkımızda bilgilerini getirir
        /// </summary>
        [HttpGet("about")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AboutUs))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAboutUsAsync()
        {
            var aboutUs = await Task.FromResult(
                _configuration.GetSection("CorporateContent:AboutUs").Get<AboutUs>()
            );

            if (aboutUs == null)
                return NotFound(new { message = "Hakkımızda bilgisi bulunamadı" });

            return Ok(aboutUs);
        }

        /// <summary>
        /// İletişim bilgilerini getirir
        /// </summary>
        [HttpGet("contact")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Contact))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetContactInfoAsync()
        {
            var contact = await Task.FromResult(
                _configuration.GetSection("CorporateContent:Contact").Get<Contact>()
            );

            if (contact == null)
                return NotFound(new { message = "İletişim bilgisi bulunamadı" });

            return Ok(contact);
        }
    }
}
