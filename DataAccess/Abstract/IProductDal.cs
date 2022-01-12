using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Abstract
{
    public interface IProductDal:IEntityRepository<Product>//IEntityRepository'yi Product için yapılandırdın demek
    {//interface mototları default public'dir kendi değildir.O yüzden interface'i public yaptık.
        






    }
}
