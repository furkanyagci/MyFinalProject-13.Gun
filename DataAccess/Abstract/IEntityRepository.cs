using Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Abstract
{
    //Where T: class : class olabilir demek değil referans tip olabilir demektir. Çoğunlukla yanlış bilinir.
    //IEntity filtresi : T Bir referans tip olmalı ve T ya IEntity olabilir yada IEntity den implemente eden bir nesne olabilir.
    //new() : new'lenebilir olmalı demek yani Buraya parametre olarak IEntity'gönderilemez oldu. Ama ondan implente eden classlar gönderilebilir çünkü class'lar new'lenebilir.
    public interface IEntityRepository<T> where T: class,IEntity,new() //Gelmesini istediğimiz classlar gelsin diye filtreleme yapabiliriz.Generic constraint(generic kısıt demek).Entites concrete klasöründeki classlarımız gelebilsin diye bu şekilde filtreledik bu classlar IEntity'den implemente oldukları için bu filtrelemeye uyarak hata vermeden çalışır
    {//IProductDal içinkileri buraya yapıştırdık. Class yerine Geeneric verdik.
        List<T> GetAll(Expression<Func<T,bool>> filter=null);//Expression : Filtreleme işlemi yapmak için kullanılıyor. p=>p.ProductId=2 getir demek yerine Expression kullanarak filtreleme yapılabilir.Bu sayede Kategoriye göre getiryada Id'ye göre getirmek için ayrı ayrı metotlar yazman gerekmeyecek.
        T Get(Expression<Func<T, bool>> filter = null);//ID ile veri almak için bunu yazdık.Örneğin bir kişinin hesap bilgileri için kullanılabilir.

        void Add(T product);
        void Update(T product);
        void Delete(T product);

        //List<T> GetAllByCategory(int categoryID); Bu koda ihtiyaç kalmadı en üstteki ikisi sayesinde bunu sildi hoca.
    }
}
