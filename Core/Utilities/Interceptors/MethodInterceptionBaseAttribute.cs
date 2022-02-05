using Castle.DynamicProxy;
using System;

namespace Core.Utilities.Interceptors
{
    //Autofac.Extensions.DependencyInjection, Autofac.Extras.DynamicProxy, Autofac bunları Core katmanına NuGet dan ekle

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public abstract class MethodInterceptionBaseAttribute : Attribute, IInterceptor
    {
        public int Priority { get; set; }//Priority öncelik demek, hangi attribute önce çalışsın. Örn : önce logalama sonra authorization gibi

        public virtual void Intercept(IInvocation invocation)
        {

        }
    }
}
