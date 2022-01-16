using Business.Abstract;
using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]//Buradaki route ise bize nasıl istekde bulunacakları. yani https://localhost:44385/api/products yazarak bu class'a ulaşabilirler demek
    [ApiController]//Bu attribute'tür. Attribute : Bir Class'la ilgili bilgi verme onu imzalama yöntemidir.Burada kısacası Bu Class bir controller'dır kendini ona göre yapılandır diyoruz .Net'e
    public class ProductsController : ControllerBase
    {
        //Loosey coupled - Gevşek bağlılık
        //naming convention - isimlendirme standardı
        IProductService _productService;

        /*IoC(Inversion of Control) Container - Bir kutu gibi düşünün bellekteki bir yer bir liste gibi düşünün ben oraya bir tana new ProductManager, new EfProductDal gibi referanslar koyayım içine ondan sonra kim ihtiyaç duyuyorsa ona verelim.
        Bunun için Startıp.cs dosyası aç.

        */
        public ProductsController(IProductService productService)//IProductService Injection'ı yaptık.
        {//Şuanda hiçbir katman bir diğerini newlemiyor veya somut(class) sınıf üzerinden gitmiyoruz. Soyut(interface) üzerinden gidiyoruz. 
            _productService = productService;
        }


        [HttpGet("getall")]
        public IActionResult GetAll()//Farklı HTTP status kodları döndürebileceğimiz bir alt yapı sunuyor bize WebAPI List<Product> idi IActionResult yaptık
        {
            //Swagger
            //Dependency Chain - Bağımlılık zinciri - burada bu var IProductService bir ProductManager ihtiyaç duyuyor ProductManager bir EfProductDal'a ihtiyaç duyuyor.
            //IProductService productService = new ProductManager(new EfProductDal());//Bağımlılığı kaldırmak için hoca bunu sildi ve yukarıdaki constructor'ı yazdı.
            var result = _productService.GetAll();
            if (result.Success)
            {
                return Ok(result);//Status 200 OK demek burası
            }
            else
            {
                return BadRequest(result);//Postman de Status baktığımızda 400 Bad Request görünür. Bu hata old. anlamına geliyor. result.Message yazarsam sadece mesajı vermiş olurum.
            }
        }

        [HttpGet("getbyid")]

        public IActionResult GetById(int id)//Get adında iki metot olunca çalıştırınca hata verdi hangi metodu çalıştıracağını bilemedi  https://localhost:44385/api/products?id=1 yazarsakta hata veriyor. Bunun çözümü için 2 yöntem var 1.[HttpGet(id)] yazarız 2.Yöntem isim(alias) veririz [HttpGet("getall")] hoca 2.yöntemi kullanıyormuş. Bu işlemlerden sonra metot isimlerinide değiştirdik. Bu Get di GetById oldu. Çalıştırmak için https://localhost:44385/api/products/getall yazacağız bu metot için https://localhost:44385/api/products/getbyid?id=1 yazacağız
        {
            var result = _productService.GetById(id);
            if (result.Success)
            {
                return Ok(result);//Status 200 OK demek burası
            }
            else
            {
                return BadRequest(result);//Postman de Status baktığımızda 400 Bad Request görünür. Bu hata old. anlamına geliyor. result.Message yazarsam sadece mesajı vermiş olurum.
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

        /*
         ProductManager.cs'nin bağımlılığını nasıl çözdük aşağıda gibi yazarak çözdük.Yani bir Constructor injection yaparak belirttik.
         
        IProductDal _productDal;

        public ProductManager(IProductDal productDal)
        {
            _productDal = productDal;
        }
        
         */

    }
}
