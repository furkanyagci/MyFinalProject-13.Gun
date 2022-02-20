using Core.CrossCuttingConcerns.Caching;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using Core.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.DependencyResolvers
{
    /*
     Burası bizim uygulama seviyesinde servis bağımlılıklarımızı çözümleyeceğimiz yer. Önce Startup.cs de yapıyorduk iş süreçleri için Business'a Autofac ile beraber taşıdık ama bunun devrede olması gerekiyor.
     */

    public class CoreModule : ICoreModule
    {
        public void Load(IServiceCollection serviceCollection)
        {
            serviceCollection.AddMemoryCache();//Bu kod MemoryCacheManager.cs deki IMemoryCache interface'ini .Net otomatik kendisi injection yapıyor.
            serviceCollection.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();//using Microsoft.AspNetCore.Http; eklenir.
            serviceCollection.AddSingleton<ICacheManager, MemoryCacheManager>();
        }
    }
}
