using Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class Category : IEntity //Mimaride IEntity görüyorsak Category'nin veri tabanı tablosu olduğunu anlarız.Bunu işaretlemenin anlamı IEntity Bu class'ın referansını tutabiliyor.
    {
        //Çıplak Class Kalmasın - Eğer ki bir class inheritance veya interface implementasyonu almıyorsa ileride proje büyüyünce problem yaşayacaksın. Concrete klasöründeki sınıflar bir veritabanı tablosuna karşılık geliyor.
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }

    }
}
