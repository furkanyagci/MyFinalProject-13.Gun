using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfProductDal : EfEntityRepositoryBase<Product, NorthwindContext>, IProductDal
    {//EfEntityRepositoryBase içinde IProductDal içindeki operasyonlar old. için IProdcutDal'ı implemente etmemize gerek kalmadı.

        public List<ProductDetailDto> GetProductDetails()
        {
            using (NorthwindContext context=new NorthwindContext())
            {
                var result = from p in context.Products
                             join c in context.Categories
                             on p.CategoryID equals c.CategoryID  //= yerine equals yazılır.
                             select new ProductDetailDto { ProductId = p.ProductID, ProductName = p.ProductName, CategoryName = c.CategoryName, UnitsInStock = p.UnitsInStock };
                return result.ToList();
            }
        }







        //NuGet
        //DataAcces sağtık > NuGet paketlerini yönet gözat kısmına bunu yaz => entityframeworkcore.sql gelenler içinden bunu seç => EntityFrameworkCore.SqlServer version kısmında 3.1.11 seç sonra yükle. EF kullanırken hazırpaketlerden yararlanıyoruz. Bu işlemden sonra DataAccess içinde artık EF kodu yazabiliriz demektir.
        //public void Add(Product entity)
        //{//Yukarıdaki using le aşağıdaki using aynı şey değil ARAŞTIRMKA İÇİN : IDisposable pattern implementation of C#
        //    using (NorthwindContext context = new NorthwindContext())//C# özel yapıdır.Bir nesne new'lendiğinde Garbage collector belli bir zaman sonra gelir onu bellekten atar ama using ile yazarsanır o nesne işi bitince hemen Garbage Collector'e gelir beni at der. Belleği hızlıca temizleme. Context ağır bir yüktür bellek için o yüzen bu önemli
        //    {
        //        var addedEntity = context.Entry(entity);
        //        addedEntity.State = EntityState.Added;
        //        context.SaveChanges();
        //    }
        //}

        //public void Delete(Product entity)
        //{
        //    using (NorthwindContext context = new NorthwindContext())
        //    {
        //        var deletedEntity = context.Entry(entity);
        //        deletedEntity.State = EntityState.Deleted;
        //        context.SaveChanges();
        //    }
        //}

        //public Product Get(Expression<Func<Product, bool>> filter = null)
        //{
        //    using (NorthwindContext context = new NorthwindContext())
        //    {
        //        return context.Set<Product>().SingleOrDefault(filter);
        //    }
        //}

        //public List<Product> GetAll(Expression<Func<Product, bool>> filter = null)
        //{
        //    using (NorthwindContext context = new NorthwindContext())
        //    {
        //        return filter == null ? context.Set<Product>().ToList() : context.Set<Product>().Where(filter).ToList(); //Arkda planda bize Select * from Product döndürür. Ternary ifyazdık eğer filtre null ise Product tablosunu lit halinde verecek filtre null değilse filtreleyip verecek.
        //    }
        //}

        //public void Update(Product entity)
        //{
        //    using (NorthwindContext context = new NorthwindContext())
        //    {
        //        var updatedEntity = context.Entry(entity);
        //        updatedEntity.State = EntityState.Modified;
        //        context.SaveChanges();
        //    }
        //}

    }
}
