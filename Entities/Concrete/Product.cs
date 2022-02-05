//using Entities.Abstract; Bu using'e gerek kalmadı IEntity'yi Core katmanına taşıdık
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{//IEntity'yi Core katmanına taşıdık o yüzden Entities katmanına Core katmanını Referans verdik buradaki hata çözüldü
    public class Product:IEntity
    {
        public int ProductID { get; set; }
        public int CategoryID { get; set; }

        //[Required]// Entity'lere validation'lara karşılık gelen veriler buraya yazılırsa SOLID'e aykırı kod yazmış oluruz.Buraya yazmak yerine .NET dünyasında olduğu gibi business'a klasör oluşturup(ValidationRules) içine yazarız.
        //[MinLength(2)]
        public string ProductName { get; set; }
        public short UnitsInStock { get; set; } //short int'in bir küçüğü
        public decimal UnitPrice { get; set; }

    }
}
