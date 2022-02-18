using Business.Abstract;
using Business.Concrete;
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
            //Buradaki yap�m�z� Autofac'e �evirdik. Autofac a�a��daki instance �retimini bizim ad�m�za yapacak hemde bize AOP deste�i sunacak.
            services.AddControllers();
            //services.AddSingleton<IProductService, ProductManager>();
            //services.AddSingleton<IProductDal, EfProductDal>();//IoC yap�land�rmay� yani hangi interface'in kar��l��� nedir yap�land�rmas�n� WebAPI'de yaparsak ileride bu projeye bir API daha eklersek yada bamba�ka bir servis mimarisi eklemek istersek b�t�n konfigurasyonumuz WebAPI de kal�yor o y�zden bu yap�lanmay� bu Starup.cs de de�ilde daha backend'de yapmal�y�z oda Autofac'dir. Business sa� t�k NuGet autofac yaz 6.1 version kur. sonra autofac.extras yaz Autofac.Extras.DynamicProxy yazan� se� 6.0.0 versiyonunu kur. Straup.cs yap�t���m�z i�lemleri Business katman�na ta��yaca��z.

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();//14. G�n sonunda ��kan hatay� bu kod ve a�a��daki ServiceTool.Create(services) kodu ile ��zd�k.


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
            ServiceTool.Create(services);//14. G�n sonunda ��kan hatay� bu kod ile ��zd�k.
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
