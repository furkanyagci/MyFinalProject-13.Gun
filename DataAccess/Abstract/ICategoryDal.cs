using Core.DataAccess;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Abstract
{
    public interface ICategoryDal:IEntityRepository<Category> // bu interface içindeki kodlara ihtiyaç kalmadı silebiliriz.Tek yerden yönetebileceğiz İnterface'leri
    {//DataAccess katmanında Abstract klasöü içindeki IproductDal içindekileri buraya aynen yapıştırdık sadece Class'ını Category yaptık.Dolayısıyla biz bunu Generiz bir yapıyla kurabiliriz. Bunların yerine İnterface yapsak Generic olan Bu Category yerinde yadda Product yerine Generic olsa istediğimiz yapıyı kurabiliriz. Bu yapının adı Generic Repository Design Pattern. IEntityRepository'yi ekledik bu design için oraya bak.
        
        //List<Category> GetAll();
        //void Add(Category product);
        //void Update(Category product);
        //void Delete(Category product);
        //List<Category> GetAllByCategory(int categoryID);
    }
}
