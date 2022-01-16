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
            /*A�a��daki yap�y� daha farkl� bir yap�ya ta��yor olaca��z. Autofac(Autofac bize AOP imkan�da sunar), Ninject, CastleWindsor, StructureMap, LightInject, DryInject .Net projelerinde IoC'nin yapt��� i�i yap�yor. IoC Container altyap�s� ortada yokken bu �ekilde �al��mak isteyen ortamlar i�in altyap� sunuyordu. 
            
             */
            services.AddControllers();
            services.AddSingleton<IProductService, ProductManager>();//Bana arka planda bir referans olu�tur.K�sacas� IoC'ler sizin yerinize new'liyor. Controller diyorki e�er bir ba��ml�l�k g�r�rsen bu tipte onun kar��l��� budur. Biri senden IProductService isterse ,ProductManager newleyip ona ver demek. AddSingleton : T�m bellekte bir tane ProductManager olu�turuyor isterse 1 milyon client(istemci) gelsin hepsine ayn�s�n� veriyor.1 Milyon tane instance �retiminden kurtuluyorsunuz ama AddSingleton ne zaman kullanaca��z i�erisinde data tutmuyorsak �rn: bir e-ticaret sitesinde sepet tutuyorsan�z herkese ayn� sepeti verirsen herkesin sepeti birbirine girer.Bu durum sadece API kullan�rken de�il heryerde bu tekni�i kullanabiliriz.Bunu yapt�ktan sonra �al��t�r�nca hata veriyor ��nk� ProductManager newlerken onunda ba�ka bir�eye ba��ml� old. g�rd� hata verdi.IProductDal istiyor onun i�in a�a��da yazd�k IProductDal isterse EfProductDal ver diye.
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
