using Business.Abstract;
using Business.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            /*Aþaðýdaki yapýyý daha farklý bir yapýya taþýyor olacaðýz. Autofac(Autofac bize AOP imkanýda sunar), Ninject, CastleWindsor, StructureMap, LightInject, DryInject .Net projelerinde IoC'nin yaptýðý iþi yapýyor. IoC Container altyapýsý ortada yokken bu þekilde çalýþmak isteyen ortamlar için altyapý sunuyordu. 
            
             */
            services.AddControllers();
            services.AddSingleton<IProductService, ProductManager>();//Bana arka planda bir referans oluþtur.Kýsacasý IoC'ler sizin yerinize new'liyor. Controller diyorki eðer bir baðýmlýlýk görürsen bu tipte onun karþýlýðý budur. Biri senden IProductService isterse ,ProductManager newleyip ona ver demek. AddSingleton : Tüm bellekte bir tane ProductManager oluþturuyor isterse 1 milyon client(istemci) gelsin hepsine aynýsýný veriyor.1 Milyon tane instance üretiminden kurtuluyorsunuz ama AddSingleton ne zaman kullanacaðýz içerisinde data tutmuyorsak örn: bir e-ticaret sitesinde sepet tutuyorsanýz herkese ayný sepeti verirsen herkesin sepeti birbirine girer.Bu durum sadece API kullanýrken deðil heryerde bu tekniði kullanabiliriz.Bunu yaptýktan sonra çalýþtýrýnca hata veriyor çünkü ProductManager newlerken onunda baþka birþeye baðýmlý old. gördü hata verdi.IProductDal istiyor onun için aþaðýda yazdýk IProductDal isterse EfProductDal ver diye.
            services.AddSingleton<IProductDal, EfProductDal>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
