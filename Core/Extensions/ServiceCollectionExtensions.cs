using Core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Extensions
{
    public static class ServiceCollectionExtensions//Extensions metodu yazabilmek için class'ın statci olması gerekiyor.
    {
        public static IServiceCollection AddDependencyResolvers(this IServiceCollection serviceCollection, ICoreModule[] modules) //API'mizin servis bağımlılıklarını yada araya girmesini istediğimiz servislerimizi eklediğimiz koleksiyondur. Buradaki this IServiceCollection serviceCollection parametre değil neyi genişletmek(Extensions) istediğimizi söylüyoruz.
        {
            foreach (var module in modules)
            {
                module.Load(serviceCollection);
            }

            return ServiceTool.Create(serviceCollection);
        }
    }

    // *** Bu yapı Core katmanıda dahil olmak üzere ekleyeceğimiz bütün injectionları bir arada toplayabileceğimzi bir yapıya dönüştü.
}
