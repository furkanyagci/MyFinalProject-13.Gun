using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Concrete.InMemory
{
    public class InMemoryProductDal : IProductDal
    {
        List<Product> _products;// Bu list'i class içinde ama bütün metotların dışında tanımladığım için global de tanımlamış oluyorum globalde old.için alt çizgi ile tanımlarız. isimlendirme standartlarında böyle

        public InMemoryProductDal()
        {
            //Oracle,Sql Server,Postgres, MogoDb. Veritabanından geliyormuş gibi simule ediyoruz o yüzden bu üründeleri girdik
            _products = new List<Product> {
                new Product {ProductID=1,CategoryID=1,ProductName="Bardak",UnitPrice=15,UnitsInStock=15 },
                new Product {ProductID=2,CategoryID=1,ProductName="Kamera",UnitPrice=500,UnitsInStock=3 },
                new Product {ProductID=3,CategoryID=2,ProductName="Telefon",UnitPrice=1500,UnitsInStock=2 },
                new Product {ProductID=4,CategoryID=2,ProductName="Klavye",UnitPrice=150,UnitsInStock=65 },
                new Product {ProductID=5,CategoryID=2,ProductName="Fare",UnitPrice=85,UnitsInStock=1 },
            };
        }

        public void Add(Product product)
        {
            _products.Add(product);
        }

        public void Delete(Product product)
        {
            //1. Yöntem
            //Product productToDelete = null;//=new Product(); Burada newlersen referans alırsın. ama burada new'lemez if içinde ürün bulununca p'yi atarsan referan numarasını atamış olursun. Burada yaptığın new boşu boşuna belleği yorar gereksiz referans numarası almış olursun.
            //foreach (var p in _products)
            //{
            //    if (product.ProductID == p.ProductID)
            //    {
            //        productToDelete = p;
            //    }
            //}

            //2.Yöntem
            //LINQ - Language Integrated Query : C# DİĞER DİLLERDEN DAHA GÜÇLÜ HALE GETİREN KULLANIMLARDAN BİR TANESİDİR.
            Product productToDelete = _products.SingleOrDefault(p => p.ProductID == product.ProductID);//SingleOrDefault Product'ı tek tek dolaşır ilgili ürünü bulur. => bu işarete Lambda denir. p aliasdır(takma ad) foreach deki gibi

            _products.Remove(productToDelete);//_products.Remove(product); Bu şekilde yazarsak silme işlemi yapamaz.ID'sini bulup ID ile silmemiz gerekiyor 2 yöntem var foreach ile olan klasik olan 2. yöntem LINQ 
        }

        public List<Product> GetAll()
        {
            return _products;
        }

        public void Update(Product product)
        {
            //Gönderdiğim ürün ID'sine sahip olan listedeki ürünü bul 
            Product productToUpdate = _products.SingleOrDefault(p => p.ProductID == product.ProductID);
            productToUpdate.ProductName = product.ProductName;
            product.CategoryID = product.CategoryID;
            product.UnitPrice = product.UnitPrice;
            productToUpdate.UnitsInStock = product.UnitsInStock;
        }

        public List<Product> GetAllByCategory(int categoryId)
        {
            return _products.Where(p => p.CategoryID == categoryId).ToList();
        }

        public List<Product> GetAll(Expression<Func<Product, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        public Product Get(Expression<Func<Product, bool>> filter = null)
        {
            throw new NotImplementedException();
        }
    }
}
