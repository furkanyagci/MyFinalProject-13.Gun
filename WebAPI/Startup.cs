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
            //Buradaki yapýmýzý Autofac'e çevirdik. Autofac aþaðýdaki instance üretimini bizim adýmýza yapacak hemde bize AOP desteði sunacak.
            services.AddControllers();
            //services.AddSingleton<IProductService, ProductManager>();
            //services.AddSingleton<IProductDal, EfProductDal>();//IoC yapýlandýrmayý yani hangi interface'in karþýlýðý nedir yapýlandýrmasýný WebAPI'de yaparsak ileride bu projeye bir API daha eklersek yada bambaþka bir servis mimarisi eklemek istersek bütün konfigurasyonumuz WebAPI de kalýyor o yüzden bu yapýlanmayý bu Starup.cs de deðilde daha backend'de yapmalýyýz oda Autofac'dir. Business sað týk NuGet autofac yaz 6.1 version kur. sonra autofac.extras yaz Autofac.Extras.DynamicProxy yazaný seç 6.0.0 versiyonunu kur. Straup.cs yapýtýðýmýz iþlemleri Business katmanýna taþýyacaðýz.
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
