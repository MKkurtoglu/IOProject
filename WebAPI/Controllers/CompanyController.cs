using BusinessLayer.Abstract;
using EntitiesLayer.Concrete.ECommerce.Models.LegalDocuments;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyInformationService _companyService;

        public CompanyController(ICompanyInformationService companyService)
        {
            _companyService = companyService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCompanyInfo()
        {
            var result = await _companyService.GetCompanyInfoAsync();
            return result.IsSuccess ? Ok(result.Data) : BadRequest(result.Message);
        }

        [HttpPut]
         // Sadece admin güncelleyebilir
        public async Task<IActionResult> UpdateCompanyInfo([FromBody] CompanyInformation company)
        {
            var result = await _companyService.UpdateCompanyInfoAsync(company);
            return result.IsSuccess ? Ok(result.Message) : BadRequest(result.Message);
        }
    }
}
