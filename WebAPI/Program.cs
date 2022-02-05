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

        //Serverla ilgili konfigurasyonun old. yer a�a��s�
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .UseServiceProviderFactory(new AutofacServiceProviderFactory())//UseServiceProviderFactory Bunun t�rk�esi "kullan servis sa�layc�s� fabrikas�". Diyorsunki .Net'e senin alt yap�nda biliyorum ki IoC altyap�n var ben onu kullanm�yorum fabrika olarak Autofac'i kullan.
            .ConfigureContainer<ContainerBuilder>(builder =>//Burada Kendi Modul'�m�z� g�steriyoruz. Ben bunu kullanmak istiyorum diyoruz.
            {
                builder.RegisterModule(new AutofacBusinessModule());
            })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
