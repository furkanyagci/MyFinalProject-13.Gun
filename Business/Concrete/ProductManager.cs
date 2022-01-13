using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{//*** Bir iş sınıfı başka sınıfları new'lemez
    public class ProductManager : IProductService
    {
        //Business da inMemory, ENtityFramework classları(klasörün için boşl ama class eklensede burada kullanılmaz) kullanmak yok. Business'ın bildiği tek şey IProductDal. IProductDal herşey olabilir.
        IProductDal _productDal;

        public ProductManager(IProductDal productDal)
        {
            _productDal = productDal;
        }

        public List<Product> GetAll()
        {
            //iş kodları
            //Yetkisi var mı?

            return _productDal.GetAll();
        }

        public List<Product> GetAllBtCategoryId(int id)
        {
            return _productDal.GetAll(p => p.CategoryID == id);
        }

        public List<Product> GetByUnitPrice(decimal min, decimal max)
        {
            return _productDal.GetAll(p => p.UnitPrice >= min && p.UnitPrice <= max);
        }

        public List<ProductDetailDto> GetProductDetails()
        {
            return _productDal.GetProductDetails();
        }
    }
}
