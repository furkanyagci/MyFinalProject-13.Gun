using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DTOs
{
    //IDto
    public class ProductDetailDto:IDto//IEntity implement edemeyiz çünkü bu bir veritabanı tablosu değil.IEntity sadece veritabanı tablolarına implement edilir unutma!!!.
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string CategoryName { get; set; }
        public short UnitsInStock { get; set; }


    }
}
