using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.ValidationRules.FluentValidation
{//Bu class ürüm giriş kurallarını barındıyor. Önr: isim en az 2 karakter olabilir.
    public class ProductValidator: AbstractValidator<Product>//AbstractValidator class FluentValidation dan geliyor
    {
        public ProductValidator()
        {
            RuleFor(p => p.ProductName).NotEmpty();
            RuleFor(p => p.ProductName).MinimumLength(2);
            RuleFor(p => p.UnitPrice).NotEmpty();
            RuleFor(p => p.UnitPrice).GreaterThan(0);//0 dan büyük olmalı
            RuleFor(p => p.UnitPrice).GreaterThanOrEqualTo(10).When(p => p.CategoryID == 1);//10 lira ve üstü olabilir. when => ne zaman categoryID 1 old. zaman
            RuleFor(p => p.ProductName).Must(StartWithA).WithMessage("Ürünler A harfi ile başlamalı");//Kendi oluşturduğun metodu bu şekilde kullanabilirsin. WithMessage ile istediğimiz mesajı kullanıcıya gösterebiliriz.
        }

        private bool StartWithA(string arg)//buradaki arg ProductName. Eğer A ile başlıyorsa true döner eğer A ile başlamıyorsa false döner 
        {
            return arg.StartsWith("A");
        }
    }
}
