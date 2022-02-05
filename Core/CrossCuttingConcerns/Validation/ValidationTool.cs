using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.CrossCuttingConcerns.Validation
{
    public static class ValidationTool
    {//Yazılım Geliştirici Yetiştirme Kampı 12. Gün C# Kurumsal Yazılım Mimarileri bu kısım için bu videoyu 1:50:00 dan sorasını izleyebilirsin

        //  ProductValidator class'ında AbstractValidator üstünde gel F12'ye bas Bu class'a implemente edilen IValidator interface'ini kopyala aşağıdaki metoda yazdık. Sebebi evrensel kullanılabilir bir hale getirmek için.

        public static void Validate(IValidator validator, object entity)//object entity : Her türü alabilsin diye object yaptık.Hepsinin base'i object
        {
            var context = new ValidationContext<object>(entity);

            //ProductValidator productValidator = new ProductValidator();Bu satıra ihtiyaç kalmadı
            
            var result = validator.Validate(context);//Doğrula(Calidate) ProductValidator class'ını. Context nesnesi ValidationContext'te gönderdiğim class yani product.
            if (!result.IsValid)//Eğer false dönerse hata fırlatacak.
            {
                throw new ValidationException(result.Errors);
            }
        }
    }
}
