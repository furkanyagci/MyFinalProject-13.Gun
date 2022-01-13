/*using Entities.Abstract;/*Buraya taşıyınca buranın altını çizdi hata verdi.Düzeltmek için Entitites'i buraya referans verirsem Northwind projesindeki entities'e bağımlı olur. Bu doğru değil.IEntity Entities katmanında Onuda Core'da Entities adında klasör açıp buraya taşıdık. Core Mantığım neydi İstediğim katmanı burada ayrı ayrı klaösrleyip implemente ederim diyordum.Bu using'e gerek kalmadı sildi hoca.
 NOT : Core katmanı diğer katmanları referan almaz bu yıldızlı bir not.*/

using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

/*Core Katmanının .Net Core ile alakası yok bu Core Öncedende kullanılan bir Katmanlı mimari Mantığı olan Core.Ortak olan DataAccess vb. classları bu katmana taşıyacağız. Ve mimariyi dahada Generic bir hale getireceğiz.
1. Çalışacağımız Entity değişken
2. Context Nesnemiz Değişken
 Örneğin : EfProductDal içinde Product entity'si var ve Context nesnesi için kodlar var Bunlar değişkendir. Bunları Base bir Class'a taşısam ve onu Generic Yapsam desem ki bana çalışacağım Entites'i ver ve çalışacağım Context'ti ver. EfProductDal , EfCategoryDal , EfEmployeeDal vb. gibi classları tekrardan yazmaktan kurtulurum Dont Repeat yourself olur yani kendimizi tekrar etmemiş oluruz. IEntityRepositry generic yaptık ve ICategoryDal, ICustomerDal, IProductDal implemente ettik bu sayede bu classların içini tekrardan yazmaktan kurtulduk iş yükünden kurtulduk aynı buradaki mantık gibi Generic yapacağız. 9.Gün videosunun başını tekrar izleyebilirsin
*/

/*
 Core Benim Evrensel Katmanım 2 tane nesneyi Core'a taşıdık.Ben sadece Northwind'de değil bütün .Net projelerinde kullanabilrim anlamına geliyor.
 */

namespace Core.DataAccess//DataAcces Katmanı içinde Abtract klasörü içindeydi sonra Core katmanını oluşturduk ve buraya taşıdık bu class'ı.
{
    //IEntityRepository'nin hiçbir db'ye bağımlılığı yok. İstediğimiz db'yi bağlayıp kullanabiliriz.
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
