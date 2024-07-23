using Base.CrossCuttingConcerns.Caching;
using Base.Utilities.Interceptors;
using Base.Utilities.IoC;
using Castle.DynamicProxy;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Aspects.Autofac.Cache
{
    public class CacheRemoveAspect : MethodInterception
    {
        private string _pattern;
        private ICacheManager _cacheManager;

        public CacheRemoveAspect(string pattern)
        {
            _pattern = pattern;
            _cacheManager = ServiceTool.ServiceProvider.GetService<ICacheManager>();
        }

        protected override void OnSuccess(IInvocation invocation) // method baaşrılı olur ise - örnek ekleme methodu- ona göre cache silecek
        {
            _cacheManager.RemoveByPattern(_pattern);
        }
    }
}
