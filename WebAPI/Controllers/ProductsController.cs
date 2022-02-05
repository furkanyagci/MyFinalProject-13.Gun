using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]//Buradaki route ise bize nasıl istekde bulunacakları. yani https://localhost:44385/api/products yazarak bu class'a ulaşabilirler demek
    [ApiController]//Bu attribute'tür. Attribute : Bir Class'la ilgili bilgi verme onu imzalama yöntemidir.Burada kısacası Bu Class bir controller'dır kendini ona göre yapılandır diyoruz .Net'e
    public class ProductsController : ControllerBase
    {
        //Loosey coupled - Gevşek bağlılık
        //naming convention - isimlendirme standardı
        IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }


        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _productService.GetAll();
            if (result.Success)
            {
                return Ok(result);//Status 200 OK demek burası
            }
            else
            {
                return BadRequest(result);
            }
        }

        [HttpGet("getbyid")]

        public IActionResult GetById(int id)
        {
            var result = _productService.GetById(id);
            if (result.Success)
            {
                return Ok(result);//Status 200 OK demek burası
            }
            else
            {
                return BadRequest(result);
            }
        }

        [HttpPost("add")]
        public IActionResult Post(Product product)
        {
            var result = _productService.Add(product);
            if (result.Success)
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
