using Autofac;
using Autofac.Extensions.DependencyInjection;
using Business.DependencyResolvers.Autofac;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        //Serverla ilgili konfigurasyonun old. yer aþaðýsý
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .UseServiceProviderFactory(new AutofacServiceProviderFactory())//UseServiceProviderFactory Bunun türkçesi "kullan servis saðlaycýsý fabrikasý". Diyorsunki .Net'e senin alt yapýnda biliyorum ki IoC altyapýn var ben onu kullanmýyorum fabrika olarak Autofac'i kullan.
            .ConfigureContainer<ContainerBuilder>(builder =>//Burada Kendi Modul'ümüzü gösteriyoruz. Ben bunu kullanmak istiyorum diyoruz.
            {
                builder.RegisterModule(new AutofacBusinessModule());
            })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
