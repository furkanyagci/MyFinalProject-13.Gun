using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Validation;
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

        [ValidationAspect(typeof(ProductValidator))]//Bu metodu doğrula ProductValidatordaki kurallara göre demektir.

        public IResult Add(Product product)
        {
            //business code ayrı validation code ayrı yerlere koyacaksınız.
            /*validation : Doğrulama bir nesnenin iş kurallarına dahil etmek için bu nesnenin yapısal olarak uygun olup olmadığını kontrol etmeye validation(doğrulama) denir. Örn : Şifre şuna uymalı veya ismin min 2 karakter olmalı. Product için girilen verinin yapısal uyumuyla alakalı olan herşeye doğrulama denir min kaç karakter olabilir maximum kaç karakter olabilir vb.
            Business(iş) kuralları ise bizim iş gereksinimlerimize uygunluktur. Örn: Ehliyet alacaksınız bir kişiye ehliyet verip vermemeyi kontrol ettiğiniz yer burasıdır. ilk yardımdan 70 almış motordan 70 almış gibi iş kurallarını burada yazarız. 
             */

            //ValidationRules içine FluentValidation klasörü oluştur. Sonra NuGet'tan FluentValidation yükle version 9.5.1 

            //if (product.ProductName.Length < 2)//bunu sildi hoca çünkü ProductValidator ekledik Business katmanına onu kullanacağız.
            //{
            //    return new ErrorResult(Messages.ProductNameInvalid);//  ErrorResult("Ürün isim en az 2 karakter olmalı") = magic strings.Bu şekilde kullanım kötü koddur antipatterndir. Biz kodlarımızın içine böyle stringle yazmayacağız.Messages.cs bunun için açtık ve temel mesajlarımız onun içinde 
            //}

            /*Cross Cutting Concerns - Uygulamayı dikine kesen ilgi alanları.
             * Örn Loglama, Cache,Transaction, Authorization(yetkilendirme) bunlara Cross Cutting Concerns denir.Validation da CCC'dir.
             * Bunun için Core katmanına Cross Cutting Concerns klasörü ekledik içine Validation klasörü ekledik
            */

            /* Bu kod refactoring edildi. Bu yapı bütün projelerimde kullanacağım için Core Katmanına ValidationTool içine taşındı
            var context = new ValidationContext<Product>(product);//ValidationContext class'ı using FluentValidation den geliyor biz oluşturmadık.Doğrulama context'i oluştur.
            ProductValidator productValidator = new ProductValidator();
            var result = productValidator.Validate(context);//Doğrula(Calidate) ProductValidator class'ını. Context nesnesi ValidationContext'te gönderdiğim class yani product.
            if (!result.IsValid)//Eğer false dönerse hata fırlatacak.
            {
                throw new ValidationException(result.Errors);
            }
            */

            //ValidationTool.Validate(new ProductValidator(),product);//Buna gerek kalmadı bu metod üstünde Attribute ekledik.
            //Loglama
            //Performans
            //transaction
            //yetkilendirme(Authorization)
            //Bunların hepsini burada yazmak yerine metot üstüne bir Attribute yazarak yapmak daha temiz kod yazmamıza sebeb olur.

            //github > Engindemiroğ > NetCoreBackend/Core/Utilities/ Interceptors klasörünü aldık buradaki kodlar uzun old. için yazılı olanı aldık zaman kazandık.
            //AOP nedir : örn metotları loglamak istiyorsun veya hata verince çalışmasını istediğin kodların varsa onları AOP yöntemiyle güzelce dizayn edebilirsin. Uygulamalarda sürekli try catch yada log yazmak zorunda kalmazsın. Bu yönteme Interceptors(araya girmek demek) deniyor.Metot'un başında sonuda araya girebilirsin.
            //Core katmanı Utilities klasörü altına Interceptors klasörü ekle içine

            _productDal.Add(product);

            return new SuccessResult(Messages.ProductAdded);//SuccessResult() Boş gönderirsek true olur.SuccessResult.cs incele
        }
    }
}
