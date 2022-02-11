using Business.Abstract;
using Business.CCS;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{//*** Bir iş sınıfı başka sınıfları new'lemez
    public class ProductManager : IProductService
    {
        //*** NOT : Bir entity manager kendisi hariç başka Dal'ı enjecte edemez. Yani ProductManager IProductDal hariç başka bir Dal'ı enjecte edemez. Başka Tablodanki verilere ulaşmak için o tablonun Business katmanındaki Service'i kullanılır. örn. kategori tablosundan veri almak için ICategoryDal KULLANILMAZ ICategoryService kullanılabilir. ICategoryService bunlara servis dememizin sebebi bir kere yaz ona ait kuralları oraya koy başkası bunu kullanmak istiyorsa bunu kullansın.Bu kısmı Tekrar dinlemek istersen 13.Gün videosunun 2:40:00 dk den sonrasını izle.
        IProductDal _productDal;
        //ICategoryDal _categoryDal; // *** NOT: Bir managerden başka manager kullanılacaksa onun Interface'i kullanılır.Burada IcategoryDal yerine ICategoryService kullanmalıyım.
        ICategoryService _categoryService;
        public ProductManager(IProductDal productDal, ICategoryService categoryService/*,ICategoryDal categoryDal*/)//*** Yıldızlı NOT : Bir Entity Manager kendisi hariç başka Dal'ı enjecte edemez
        {
            _productDal = productDal;
            //_categoryDal = categoryDal;
            _categoryService = categoryService;
        }

        public void IResult(Product product)
        {
            throw new NotImplementedException();
        }

        public IDataResult<List<Product>> GetAll()
        {
            //iş kodları
            //Yetkisi var mı? gibi kodlamalar yapılır.
            //if (DateTime.Now.Hour == 15)
            //{
            //    return new ErrorDataResult<List<Product>>(Messages.MaintenanceTime);
            //}

            return new SuccessDataResult<List<Product>>(_productDal.GetAll(), Messages.ProductsListed);//DataResult dı SeuccessDataResult'a çevirdik
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

        //Aspect Oriented Programming (AOP)
        //[ValidationAspect(typeof(ProductValidator))]//Bu metodu doğrula ProductValidatordaki kurallara göre demektir.
        public IResult Add(Product product)
        {
            /*Cross Cutting Concerns : Katmanlı mimarilerde bu katmanları dikine kesen  ilgi alanlarımız var.Uygulamayı dikine kesen ilgi alanları diye çeviriyormuş Engin hoca.
             * Bunlar aşağıdakilerdir.
             * Validation
             * Log
             * Cache
             * Transaction
             * Authorization
             * vb.
            */

            /*Bir kategoride en fazla 10 ürün olabilir kodunu yazınız. Aşağıdaki gibi yazarsak yanlış yoldayızdemektir. Bu şekilde spagetti kod olur. Oyüzden bu kodu CheckIfProductCountOfCategoryCorrect() oluşturup içine yazdık.
            Bir iş kuralı yazacaksan baştan en alttaki gibi private metot içine yazarsın iş kuralı parçacığı old. için. Aşağıda CheckIfProductCountOfCategoryCorrect() metotduna bak.
            */
            //var result = _productDal.GetAll(p => p.CategoryID == product.CategoryID).Count;
            //if (result >= 10)
            //{
            //    return new ErrorResult(Messages.ProductCountOfCategoryError);
            //}

            //Yarın öbür gün yeni bir iş kuralı mı geldi? aşağıdaki metotların oraya yaz buraya virgül koyup ekle hiçbir yeri değiştirmene gerek yok.
            IResult result = BusinessRules.Run(CheckIfProductNameExist(product.ProductName), CheckIfProductCountOfCategoryCorrect(product.CategoryID), CheckIfCategoryLimitExceded());
            //iş kurallarını BusinessRules daki Run metoduna gönderiyoruz dönen sonuç null değilse gelen veriyi dönderiyoruz.
            if (result != null)
            {
                return result;
            }
            return new ErrorResult(result.Message);

            ////Core > Utilities > Business klasörü oluşturduk iş kurallarını bunun içine yazacağız. Aşağıdaki koda gerek kalmadı
            //if (CheckIfProductCountOfCategoryCorrect(product.CategoryID).Success && CheckIfProductNameExist(product.ProductName).Success)
            //{
            //    _productDal.Add(product);

            //    return new SuccessResult(Messages.ProductAdded);//SuccessResult() Boş gönderirsek true olur.SuccessResult.cs incele
            //}
            //return new ErrorResult();
        }

        //Eğer mevcut kategori sayısı 15'i geçtiyse sisteme yeni ürün eklenemez.Bu soruda mantık aramayın amaç farklı tablodan veri alma denemesi yapma. *** NOT : Bu tip işlemde yani başka tablodan veri alacaksan injection yaparsın ama o tablonun _categoryDal'ı değil _categoryService kullanarak yaparsın.
        private IResult CheckIfCategoryLimitExceded()
        {
            var result = _categoryService.GetAll().Data.Count;
            if (result > 15)
            {
                return new ErrorResult(Messages.CategoryLimitExceded);
            }

            return new SuccessResult();
        }

        //Aynı isimde ürün eklenemez
        private IResult CheckIfProductNameExist(string productName)
        {
            var result = _productDal.GetAll(p => p.ProductName == productName);
            if (result == null)
            {
                return new SuccessResult();
            }
            return new ErrorResult(Messages.ProductNameAlreadyExists);
        }

        //Spagetti koda dönüşmemesi için bu şekilde yazdık bu iş parçacığını
        private IResult CheckIfProductCountOfCategoryCorrect(int categoryId)//iş kuralı parçacığı olduğu için Product yerine int bir parametre yazdık.
        {
            //Aşağıdaki kod veritabanında Select count(*) from products where categoryID=categortId bu sorguyu çalıştırır.
            var result = _productDal.GetAll(p => p.CategoryID == categoryId).Count;
            if (result >= 10)
            {
                return new ErrorResult(Messages.ProductCountOfCategoryError);
            }
            return new SuccessResult();
        }
    }
}
