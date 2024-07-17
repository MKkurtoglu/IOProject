using Autofac.Core;
using Base.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
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
            collection.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }
    }
}
