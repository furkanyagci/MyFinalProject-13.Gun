using Business.Abstract;
using Business.Concrete;
using Core.DependencyResolvers;
using Core.Extensions;
using Core.Utilities.IoC;
using Core.Utilities.Security.Encryption;
using Core.Utilities.Security.JWT;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
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


            //Bu kodu  Core > Utilities > DependencyResolvers > CoreModule aktardýk. buradan sildi hoca
            //services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();//*** 14. Gün sonunda çýkan hatayý bu kod ve aþaðýdaki ServiceTool.Create(services) kodu ile çözdük. HttpContextAccessor kullanýcýnýn her yaptýðý istekle oluþan context. Ýsteðin baþlangýcýndan bitiþine kadar o kullanýcýnýn o isteðini HttpContextAccessor yapar. *** Bu injection olayý bizim farklý API'lerimizde olsa oradada kullanacaðýz o yüzden Core tarafýna aktaracaðýz. Core > Utilities > DependencyResolvers > CoreModule aktardýk.


            //14.Gunde eklendi
            var tokenOptions = Configuration.GetSection("TokenOptions").Get<TokenOptions>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidIssuer = tokenOptions.Issuer,
                        ValidAudience = tokenOptions.Audience,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(tokenOptions.SecurityKey)
                    };
                });

            //ServiceTool.Create(services); bu kod bizden servisleri istiyor. BU kodu sildik çünkü geçiçi bir çözümdü bunun yerine aþaðýsýndaki kodu yazdýk.
            //ServiceTool.Create(services);//*** 14. Gün sonunda çýkan hatayý bu kod ile çözdük. HttpContextAccessor devreye girmiyordu bu kodla devreye sokduk.

            services.AddDependencyResolvers(new ICoreModule[] {
            new CoreModule()
            }); /*Þuanda Sadece CoreModule var ama bunun gibi daha çok Module olabilir o yüzden AddDependencyResolvers yaptýk birden fazla olursa bu þekilde ekleyebiliriz. Core katmanýnda Extensions klasörüne ServiceCollectionExtensions.cs ekledik.

            (new ICoreModule[] {
            new CoreModule()
            }); bu ekleme þekli dizi old. için yarýn baþla bir Module geldiðinde araya virgül atýp buraya ekleyebiliriz. 
            */
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

            //14.gunde eklendi.
            app.UseAuthentication();


            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
