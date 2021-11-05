using Mimp.SeeSharper.DependencyInjection.Scope.Abstraction;
using System;

namespace Mimp.SeeSharper.DependencyInjection.Scope
{
    public class DependencyScopeFactory : IDependencyScopeFactory
    {


        public IDependencyScope CreateDependencyScope(IScopeContext context)
        {
            if (context is null)
                throw new ArgumentNullException(nameof(context));

            return new DependencyScope(context.Scope,
                scope => new ScopeDependencyProvider(scope, context.Parent));
        }


    }
}
