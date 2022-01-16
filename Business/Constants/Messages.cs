using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Constants//Contants - Sabitler demek. Enum gibi
{//Temel mesajlarımızı bunun içine yazacağız.
    public static class Messages //New'lememek için static verdik.Basit bir mesaj old. için ve sabit old. için newlemek istemiyoruz ve static yapıyoruz.
    {
        public static string ProductAdded = "Ürün eklendi";//bir değişken olmasına rağmen büyük harfle yazdık. Nedeni Public olması publicler pascalcase ile yazılır.
        public static string ProductNameInvalid = "Ürün ismi geçersiz";
        internal static string MaintenanceTime = "Sistem bakımda";
        internal static string ProductsListed = "Ürünler listelendi";
    }
}
