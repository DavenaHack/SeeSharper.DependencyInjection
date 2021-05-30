using Mimp.SeeSharper.DependencyInjection.Abstraction;
using Mimp.SeeSharper.DependencyInjection.Scope.Abstraction;
using System;

namespace Mimp.SeeSharper.DependencyInjection.Scope
{
    public class ScopeDependencyProvider : IScopeDependencyProvider
    {


        public IDependencyProvider Parent { get; }

        public IDependencyScope Scope { get; }


        public ScopeDependencyProvider(IDependencyScope scope, IDependencyProvider parent)
        {
            Scope = scope ?? throw new ArgumentNullException(nameof(scope));
            Parent = parent ?? throw new ArgumentNullException(nameof(parent));
        }


        public virtual IDependency? Provide(IDependencyContext context)
        {
            if (context is null)
                throw new ArgumentNullException(nameof(context));

            return Parent.Provide(context);
        }


    }
}
