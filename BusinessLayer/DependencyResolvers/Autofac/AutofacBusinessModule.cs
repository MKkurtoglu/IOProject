using Autofac;
using Autofac.Extras.DynamicProxy;
using Base.Utilities.Interceptors;
using Base.Utilities.Security.JWT;
using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using Castle.DynamicProxy;
using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.DependencyResolvers.Autofac
{
    public class AutofacBusinessModule : Module
    {
        // bu autofac kullanmamızın sebepleri birden fazladır. bunalrdan bazıları şunlardır
        //Autfac aop yapılarını destekler 
        // autofac ile bu projeye başka bir apı ekler isek buradan direkt kullanabiliriz öncekinde problem yaşaaybilrdik o apinin de kendi startup'una IoC yazılması lazım gerekecekti.
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ProductManager>().As<IProductService>();
            builder.RegisterType<EfProductDal>().As<IProductDal>();

            builder.RegisterType<CategoryManager>().As<ICategoryService>();
            builder.RegisterType<EfCategoryDal>().As<ICategoryDal>();

            builder.RegisterType<CustomerManager>().As<ICustomerService>();
            builder.RegisterType<EfCustomerDal>().As<ICustomerDal >();

            builder.RegisterType<UserManager>().As<IUserService>();
            builder.RegisterType<EfUserDal>().As<IUserDal>();

            builder.RegisterType<AuthManager>().As<IAuthService>();
            builder.RegisterType<JwtHelper>().As<ITokenHelper>();

            var assembly = System.Reflection.Assembly.GetExecutingAssembly(); // assembly getirri hangi assembly peki ? içerisinde çalışan elemanların oldğu amssbly döner..

            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces() //çalışan uygulamaların olduğu assmebly içerisinde implemente edilmiş interfaceleri bul
                .EnableInterfaceInterceptors(new ProxyGenerationOptions()
                {
                    Selector = new AspectInterceptorSelector() // genel mantık ile yukarıdaki map classlarımız da aspectleri var mı diyue bakıyor bu sistem..
                }).SingleInstance();

        }
    }
}
