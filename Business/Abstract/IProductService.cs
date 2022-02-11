using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IProductService
    {
        IDataResult<List<Product>> GetAll();//IResult yaparsak sadece mesaj döner.Ama IDateResult yazarsak Hem Data hem Mesaj hem işlem sonucu true flase döner
        IDataResult<List<Product>> GetAllBtCategoryId(int Id);
        IDataResult<List<Product>> GetByUnitPrice(decimal min, decimal max);
        IDataResult<List<ProductDetailDto>> GetProductDetails();
        IDataResult<Product> GetById(int productId);//Bunu gerçek hayatta ürün detayına girmek istiyorsunuz basıyorsunuz ürün detay geliyor o tip işlemlerde bu kullanılıyor.
        IResult Add(Product product);

    }
}
