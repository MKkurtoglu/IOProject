using Autofac.Core;
using Base.CrossCuttingConcerns.Caching;
using Base.CrossCuttingConcerns.Caching.MicrosoftCache;
using Base.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.DependencyResolvers
{
    public class CoreModule : ICoreModule
    {
        //program.cs'e yazmayıp corumodule ile dependecy'leri çözecez.
        public void Load(IServiceCollection collection)
        {
            collection.AddMemoryCache(); // asp.net den gelen bir injecttir. 
            collection.AddSingleton<ICacheManager, MemoryCacheManager>();
            collection.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            collection.AddSingleton<Stopwatch>();
        }
    }
}
