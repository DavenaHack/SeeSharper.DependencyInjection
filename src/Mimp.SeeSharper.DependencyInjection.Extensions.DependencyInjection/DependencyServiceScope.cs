using Microsoft.Extensions.DependencyInjection;
using Mimp.SeeSharper.DependencyInjection.Scope.Abstraction;
using System;

namespace Mimp.SeeSharper.DependencyInjection.Extensions.DependencyInjection
{
    public class DependencyServiceScope : IServiceScope
    {


        public IServiceProvider ServiceProvider { get; }

        public IDependencyScope Scope { get; }


        public DependencyServiceScope(IServiceProvider serviceProvider, IDependencyScope scope)
        {
            ServiceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
            Scope = scope ?? throw new ArgumentNullException(nameof(scope));
        }


        public void Dispose()
        {
            Scope.Dispose();
            GC.SuppressFinalize(this);
        }


    }
}
