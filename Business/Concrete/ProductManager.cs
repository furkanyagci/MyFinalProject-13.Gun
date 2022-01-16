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
            //Yetkisi var mı? gibi kodlamalar yapılır.
            if (DateTime.Now.Hour == 15)
            {
                return new ErrorDataResult<List<Product>>(Messages.MaintenanceTime);
            }

            return new SuccessDataResult<List<Product>>(_productDal.GetAll(),Messages.ProductsListed);//DataResult dı SeuccessDataResult'a çevirdik
        }

        public IDataResult<List<Product>> GetAllBtCategoryId(int id)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p => p.CategoryID == id));
        }

        public IDataResult<Product> GetById(int productId)
        {
            return new SuccessDataResult<Product>(_productDal.Get(p => p.ProductID == productId));
        }

        public IDataResult<List<Product>> GetByUnitPrice(decimal min, decimal max)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p => p.UnitPrice >= min && p.UnitPrice <= max));
        }

        public IDataResult<List<ProductDetailDto>> GetProductDetails()
        {
            return new SuccessDataResult<List<ProductDetailDto>>(_productDal.GetProductDetails());
        }

        public IResult Add(Product product)
        {
            //business code - iş kod - bir filtreleme yapılacaksa burada yapılır örn: ürün adı 2 harfden fazla mı? Fiyat bilgisi 0'dan büyük mü?

            if (product.ProductName.Length < 2)
            {
                return new ErrorResult(Messages.ProductNameInvalid);//  ErrorResult("Ürün isim en az 2 karakter olmalı") = magic strings.Bu şekilde kullanım kötü koddur antipatterndir. Biz kodlarımızın içine böyle stringle yazmayacağız.Messages.cs bunun için açtık ve temel mesajlarımız onun içinde 
            }

            _productDal.Add(product);

            return new SuccessResult(Messages.ProductAdded);//SuccessResult() Boş gönderirsek true olur.SuccessResult.cs incele
        }
    }
}
