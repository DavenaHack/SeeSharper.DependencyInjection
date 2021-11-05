using Mimp.SeeSharper.DependencyInjection.Abstraction;
using Mimp.SeeSharper.DependencyInjection.Scope.Abstraction;
using System;
using System.Collections.Generic;

namespace Mimp.SeeSharper.DependencyInjection.Scope
{
    public class ScopeDependencySource : IDependencySource
    {


        private readonly IDictionary<IDependencyProvider, ISet<IDependencyContext>> _scopeRequriedDependencies;


        public IDependencySource Source { get; }

        public IScope Scope { get; set; }


        public ScopeDependencySource(IDependencySource source, IScope scope)
        {
            Source = source ?? throw new ArgumentNullException(nameof(source));
            Scope = scope ?? throw new ArgumentNullException(nameof(scope));
            _scopeRequriedDependencies = new Dictionary<IDependencyProvider, ISet<IDependencyContext>>();
        }



        public IEnumerable<IDependencyFactory> GetFactories(IDependencyProvider provider, IDependencyContext context)
        {
            if (provider is null)
                throw new ArgumentNullException(nameof(provider));
            if (context is null)
                throw new ArgumentNullException(nameof(context));

            if (!_scopeRequriedDependencies.TryGetValue(provider, out var dependencies))
                lock (_scopeRequriedDependencies)
                    if (!_scopeRequriedDependencies.TryGetValue(provider, out dependencies))
                        _scopeRequriedDependencies.Add(provider, dependencies = new HashSet<IDependencyContext>());
            lock (dependencies)
            {
                // the request dependency is required to determite the scope
                if (dependencies.Contains(context))
                    return Array.Empty<IDependencyFactory>();
                dependencies.Add(context);

            }
            var contextScope = context.GetScope();
            lock (dependencies)
            {
                dependencies.Remove(context);
                if (dependencies.Count == 0)
                    lock (_scopeRequriedDependencies)
                        _scopeRequriedDependencies.Remove(provider);
            }

            if (!Scope.In(contextScope))
                return Array.Empty<IDependencyFactory>();

            return Source.GetFactories(provider, context);
        }


        public void Dispose(IDependencyProvider provider)
        {
            Source.Dispose(provider);
        }


    }
}
