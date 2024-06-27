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
        // önemli not --> bir katman başka bir katman dan bir classı bir efdalı asla interface dışında 
        // bağlantı kurulmayacak.yoksa büyük problemlere yol açar.
        IProductService _productService;
        public ProductsController(IProductService productService)
        {
            _productService = productService; // soyut bağlılık olabilir ancak bu nesne ile yapılamaz. sadene interface ile yapılacak
            // ancak burada şu durum var. parametre ile verilen bir interface olduğu için bu sistem aşağıda parametrede interface ile verdiğimiz de 
            // sistem çözümleyemeyecek. belki 2 tane maanger var hangisi olduğunu anlayaamyacak.
            // bu problemin çözümünü mvc de olduğu gibi IoC (Inversion of Control) ile çözeceğiz.
            // bu bir bellek gibi konteyner'da new PM(), new EfPD() gibi ttuacak ve WEb api bizim yerimize bu referansları atayacak.
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var result = _productService.GetAll();
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);


        }
        [HttpGet("GetById")]
        public IActionResult Get(int id)
        {
            var result = _productService.Get(id);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpPost("Add")]
        public IActionResult Add(Product product)
        {
            var result = _productService.Insert(product);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }
    }
}
