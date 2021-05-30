using Mimp.SeeSharper.DependencyInjection.Abstraction;
using System;

namespace Mimp.SeeSharper.DependencyInjection.Extensions.DependencyInjection
{
    public class DependencyServiceProvider : IServiceProvider
    {


        public IDependencyProvider Provider { get; }


        public DependencyServiceProvider(IDependencyProvider provider)
        {
            Provider = provider ?? throw new ArgumentNullException(nameof(provider));
        }


        public object? GetService(Type serviceType)
        {
            if (serviceType is null)
                throw new ArgumentNullException(nameof(serviceType));

            var dependency = Provider.Provide(serviceType);
            if (dependency is null)
                return null;

            return dependency.Dependency; // TODO check if there a method/option to dispose it like IDependency should
        }


    }
}
