using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.InMemory;
using System;

namespace ConsoleUI
{
    //SOLID
    //Open Closed Principle
    class Program
    {
        static void Main(string[] args)
        {
            ProductManager productManager = new ProductManager(new EfProductDal());//EfProdcutDal yazdıktan sonra buraya yazdık. Bu PnP oldu yani tak çalıştır. Diğer hiçbir katmana dokunmadık EfProdcutDal oluşturduk ve buraya yazdık.Sitem bir anda EntityFramework altyapısına dönmüş oldu.
            foreach (var product in productManager.GetByUnitPrice(40,100))
            {
                Console.WriteLine(product.ProductName);
            }


        }
    }
}
