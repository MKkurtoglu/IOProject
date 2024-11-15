using Base.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Extensions
{
    // extensions classları static olur
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDependencyResolvers(this IServiceCollection collection,params ICoreModule[] coreModule)
        {
            foreach (var module in coreModule)
            { 
            module.Load(collection);
            }
            return ServiceTool.Create(collection);
        }
    }
}
