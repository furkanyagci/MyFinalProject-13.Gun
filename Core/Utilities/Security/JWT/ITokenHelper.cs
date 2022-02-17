using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Security.JWT
{
    //kullanıcı adı ve parola girdikten sonra butona bastı apiye geldi burada eğer doğruysa bizim aşağıdaki oparasyonumuz çalışacak. İlgili kullanıcı için db ye gidecek kullanıcının Claim'lerini bulacak orada JWT üretecek onları kullanıcıya verecek.
    public interface ITokenHelper
    {
        AccessToken CreateToken(User user, List<OperationClaim> operationClaims);
    }

    //Bu class'ı yazdıktan sonra engindemirog github'ından kokdlar kopyaladı daha hızlı ilerlemek için.
}