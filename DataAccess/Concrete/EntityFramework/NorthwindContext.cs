using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Concrete.EntityFramework
{//Context nesnesi DB tabloları ile proje classlarını bağlamak
    public class NorthwindContext : DbContext
    {
        //override yaz boşluk on yazınca çıkıyor.
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)//Bu metot Db nin hangi db ile ilişkilendireceğini belirttiğin metot
        {//başına @ koyunca test slaci normal ters slaç olarak algıla demek. Normal projede SQL server için ip görürsünüz. örn: Server=175.45.2.12 gibi. Ama biz development ortamında old. için bu şekilde yazdık.
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=Northwind;Trusted_Connection=true");
        }

        public DbSet<Product> Products { get; set; }//DbSet Db tablosunu hangi alana bağlayacağını gösteriyor.Burada Dbdeki Products alanına Entitiesdeki Product class'ına bağla diyoruz.Hangi tabloya ne karşılık gelecek onu belirtiyoruz.
        public DbSet<Category> Categories { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }


    }
}
