using Mimp.SeeSharper.DependencyInjection.Scope.Abstraction;
using System;

namespace Mimp.SeeSharper.DependencyInjection.Scope
{
    public class DependencyScopeFactory : IDependencyScopeFactory
    {


        public bool SubScopes { get; }


        public DependencyScopeFactory(bool subScopes)
        {
            SubScopes = subScopes;
        }

        public DependencyScopeFactory()
            : this(true) { }


        public IDependencyScope CreateScope(IDependencyScopeContext context)
        {
            if (context is null)
                throw new ArgumentNullException(nameof(context));

            return new DependencyScope(SubScopes ? CreateSubScope(context.Scope) : context.Scope, 
                scope => new ScopeDependencyProvider(scope, context.Parent));
        }


        protected virtual Func<IDependencyScope, object> CreateSubScope(Func<IDependencyScope, object> scope) => depScope =>
        {
            var parent = depScope.Provider.Parent;
            var current = scope(depScope);
            if (parent is not IScopeDependencyProvider scoped)
                return current;

            var parentScope = scoped.Scope.Scope;
            if (current is not SubScope subScope)
                return new SubScope(parentScope, current);

            if (subScope.Parent == parentScope)
                return subScope;

            return new SubScope(parentScope, subScope);
        };


    }
}
