using Microsoft.Extensions.DependencyInjection;

namespace Base.Utilities.IoC
{
    public interface ICoreModule
    {
        void Load(IServiceCollection collection);
    }
}
