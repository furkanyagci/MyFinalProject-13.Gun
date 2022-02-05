using Autofac;
using Business.Abstract;
using Business.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.DependencyResolvers.Autofac
{//AutofacBusinessModule adının sebebi Business katmanına göre yazılması yani bu projeye göre yazılacak.Bunun birde Core katmanında yazılanı var o Core katmanı gibi her projeye eklenip kullanılabilir.Her projede ayrı ayrı set etmeyelim diye Core'a yazacağız bu projedeki olayları burada set edeceğiz
    public class AutofacBusinessModule: Module //using Autofac; olacak Reflection olanı seçme. Bu Module ne işe yarıyor WebAPI Starup.cs interface isteyince class newletme ortamını böyle sağlıyorsun
    {//Autofac bize aynı zamanda AOP desteği veriyor.
        protected override void Load(ContainerBuilder builder)//override yaz gelenler arasından Load seç.
        {
            builder.RegisterType<ProductManager>().As<IProductService>().SingleInstance(); //Bu kod  services.AddSingleton<IProductService, ProductManager>(); karşılık geliyor.builder.RegisterType<ProductManager>().As<IProductService>(); biri senden IProductService isterse ProductManager new'leyip ver demektir. .SingleInstance(); Tek bir instance oluştur demek içinde data taşımadıı için bu şekilde tek instance oluşturup onu herkesle paylaşıyor.
            builder.RegisterType<EfProductDal>().As<IProductDal>().SingleInstance();

            /*Bu class'ı yazdıktan sonra WepAPI de program.cs'yi açıp gerekli ayarları yaptık. Startup.cs deki ayarları değilde buradakileri kullansın diye.
             
             */
        }
    }
}
