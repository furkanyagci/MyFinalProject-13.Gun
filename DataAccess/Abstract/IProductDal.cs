using Core.DataAccess;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Abstract
{

    //Core katmanını oluşturduktan sonra burdada hata verdi bu katmana Core katmanını referans verdik. Bu olaya Code Refactoring deniyor.
    public interface IProductDal:IEntityRepository<Product>//IEntityRepository'yi Product için yapılandırdın demek
    {//interface mototları default public'dir kendi değildir.O yüzden interface'i public yaptık.

        List<ProductDetailDto> GetProductDetails();


    }

    //Code Refactoring
}
