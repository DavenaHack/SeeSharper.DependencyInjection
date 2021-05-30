using Microsoft.Extensions.DependencyInjection;
using Mimp.SeeSharper.DependencyInjection.Abstraction;
using Mimp.SeeSharper.DependencyInjection.Scope.Abstraction;
using System;

namespace Mimp.SeeSharper.DependencyInjection.Extensions.DependencyInjection
{
    public class DependencyServiceScopeFactory : IServiceScopeFactory
    {


        public IDependencyProvider Provider { get; }


        public DependencyServiceScopeFactory(IDependencyProvider provider)
        {
            Provider = provider ?? throw new ArgumentNullException(nameof(provider));
        }


        public IServiceScope CreateScope()
        {
            var scope = Provider.CreateScope();
            return new DependencyServiceScope(new DependencyServiceProvider(scope.Provider), scope);
        }


    }
}
