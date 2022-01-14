using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
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

        public void IResult(Product product)//İşlem ile ilgili bilgilendirme yapmak istiyorum örn: ürün eklendi yada eklenmedi
        {
            //business codes - iş kodları buraya yazılır sonra db'ye kaydedilir yada uyarı verilir.Herhangi bir filtreleme varsa burada yapılır.
            throw new NotImplementedException();
        }

        public IDataResult<List<Product>> GetAll()
        {
            //iş kodları
            //Yetkisi var mı?

            return new DataResult<List<Product>>(_productDal.GetAll(),true,"Ürünler listelendi");
        }

        public List<Product> GetAllBtCategoryId(int id)
        {
            return _productDal.GetAll(p => p.CategoryID == id);
        }

        public Product GetById(int productId)
        {
            return _productDal.Get(p => p.ProductID == productId);
        }

        public List<Product> GetByUnitPrice(decimal min, decimal max)
        {
            return _productDal.GetAll(p => p.UnitPrice >= min && p.UnitPrice <= max);
        }

        public List<ProductDetailDto> GetProductDetails()
        {
            return _productDal.GetProductDetails();
        }

        public IResult Add(Product product)
        {
            //business code - iş kod bir filtreleme yapılacaksa burada yapılır örn: ürün adı 2 harfden fazla mı? Fiyat bilgisi 0'dan büyük mü?

            if (product.ProductName.Length<2)
            {
                return new ErrorResult(Messages.ProductNameInvalid);//  ErrorResult("Ürün isim en az 2 karakter olmalı") = magic strings.Bu şekilde kullanım kötü koddur antipatterndir. Biz kodlarımızın içine böyle stringle yazmayacağız.Messages.cs bunun için açtık ve temel mesajlarımız onun içinde 
            }

            _productDal.Add(product);

            return new SuccessResult(Messages.ProductAdded);//SuccessResult() Boş gönderirsek true olur.SuccessResult.cs incele
        }
    }
}
