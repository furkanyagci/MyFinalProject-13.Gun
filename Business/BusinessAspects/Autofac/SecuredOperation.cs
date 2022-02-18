using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using Castle.DynamicProxy;
using Microsoft.Extensions.DependencyInjection;
using Core.Extensions;
using Business.Constants;

namespace Business.BusinessAspects.Autofac
{
    //SecuredOperation JWT için
    public class SecuredOperation : MethodInterception
    {
        private string[] _roles;
        private IHttpContextAccessor _httpContextAccessor;//Bir istek yapıldığında JWT nin de içinde old. bir istek Herken için HttpContext oluşur. 

        public SecuredOperation(string roles)
        {
            _roles = roles.Split(',');
            _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();// buradaki hataları gidermek için Nuget'tan  DynamicProxy, Microsoft.Extensions.DependencyInjection paketlerini yükledik. Çalışma zamanında burada veridği hatanın sebebi Postmande Send ederken Authorization bilgisi vermiyoruz o yüzden HtppContext oluşmuyor null geliyor. bunun çözümü için postmana https://localhost:44385/api/auth/register yaz. Body raw kısmını aç aşağıdakileri yaz sen et düzelir. BU şekilde Authorization oluşturuyoruz.
            /*
                {
                    "firstName": "Engin",
                    "lastName": "Demirog",
                    "email": "engin@engin.com",
                    "password": "12345"
                }
             */
            //Burada yine hata veriyor Engin hoca paketlerle alakalı dedi. Hızlı çözüm için Startup.cs içine küçük bir kod yazdık oraya bak.
        }

        protected override void OnBefore(IInvocation invocation)//Metodun önünde çalıştır demek örnek add metodundan önce çalış adamın yetkisi varmı bak.
        {
            var roleClaims = _httpContextAccessor.HttpContext.User.ClaimRoles();//Kullanıcının rollerini bul
            foreach (var role in _roles)//Kullanıcının rollerini gez claimlerinin içinde ilgili rol varsa return et metodu çalıştırmaya devam et demek. Yoksa hata verir.
            {
                if (roleClaims.Contains(role))
                {
                    return;
                }
            }
            throw new Exception(Messages.AuthorizationDenied);
        }
    }
}
