using Autofac;
using Autofac.Extras.DynamicProxy;
using Business.Abstract;
using Business.Concrete;
using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using Core.Utilities.Security.JWT;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.DependencyResolvers.Autofac
{//DependencyResolvers türkçesi bağımlılık çözümlemeleri demek.
    public class AutofacBusinessModule: Module 
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ProductManager>().As<IProductService>().SingleInstance(); 
            builder.RegisterType<EfProductDal>().As<IProductDal>().SingleInstance();

            //aşağıdaki 2 builder'ı 13.Gün sonunda yapılan değişikler için ekledik yooksa injection hatası verir
            builder.RegisterType<CategoryManager>().As<ICategoryService>().SingleInstance();
            builder.RegisterType<EfCategoryDal>().As<ICategoryDal>().SingleInstance();

            builder.RegisterType<UserManager>().As<IUserService>();
            builder.RegisterType<EfUserDal>().As<IUserDal>();

            builder.RegisterType<AuthManager>().As<IAuthService>();
            builder.RegisterType<JwtHelper>().As<ITokenHelper>();

            //Aşağıdaki kodu kaldırdık. Startup.cs de var oadanda Core katmanı Utilities > IoC kalsörüleri içine ICoreModule interface'ine taşıdık. Tüm apilerimizde kullanabilelim diye.
            //builder.RegisterType<HttpContextAccessor>().As<IHttpContextAccessor>();//Burada HttpContextAccessor using Microsoft.AspNetCore.Http; ekli old. halde hata veriyor. using Microsoft.AspNetCore.Http; paketini yükle diyor buda sürüm sorunu var demektir. Ampülden yükle dedik ama yine sorun çözülmedi. *** Çözüldü : Postmande Authorization bilgisi vermediğimiz için SecuredOperation.cs HttpContext oluşmuyor. GraphQL sağıda Text yazıyorsa onu JSON yapmayı unutma yoksa "415 unsupported media type" hatası veriyor.


            var assembly = System.Reflection.Assembly.GetExecutingAssembly();

            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces()
                .EnableInterfaceInterceptors(new ProxyGenerationOptions()
                {
                    Selector = new AspectInterceptorSelector()
                }).SingleInstance();
        }
    }
}
