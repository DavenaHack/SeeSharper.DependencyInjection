using Mimp.SeeSharper.DependencyInjection.Abstraction;
using Mimp.SeeSharper.DependencyInjection.Scope.Abstraction;
using Mimp.SeeSharper.DependencyInjection.Tag;
using Mimp.SeeSharper.DependencyInjection.Tag.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Mimp.SeeSharper.DependencyInjection.Scope
{
    public class ScopeDependencySource : IDependencySource
    {


        public IDependencySource Source { get; }

        public Func<IDependencyProvider, IDependencyContext, bool> IsScope { get; }


        public ScopeDependencySource(IDependencySource source, Func<IDependencyProvider, IDependencyContext, bool> isScope)
        {
            Source = source ?? throw new ArgumentNullException(nameof(source));
            IsScope = isScope ?? throw new ArgumentNullException(nameof(isScope));
        }


        public IEnumerable<IDependencyFactory> GetFactories(IDependencyProvider provider, IDependencyContext context)
        {
            if (provider is null)
                throw new ArgumentNullException(nameof(provider));
            if (context is null)
                throw new ArgumentNullException(nameof(context));

            if (!IsScope(provider, context))
                return Array.Empty<IDependencyFactory>();

            return Source.GetFactories(provider, context);
        }


        public void Dispose(IDependencyProvider provider)
        {
            Source.Dispose(provider);
        }


        public static Func<IDependencyProvider, IDependencyContext, bool> IsScopes(IEnumerable<object> scopes)
        {
            if (scopes is null)
                throw new ArgumentNullException(nameof(scopes));
            if (scopes.Any(s => s is null))
                throw new ArgumentNullException(nameof(scopes), $"At least one scope is null.");

            scopes = scopes.ToArray();

            return (provider, context) =>
            {
                // Scope and tag verifier have to provide from none scope dependency sources.
                // Otherwise it will call the scope dependency source if it has a scope verfier
                // and to resolve that it will ask for the tag verfier
                if (context.DependencyType == typeof(IScopeVerifier) && context is ITagDependencyContext scopeContext
                    && Equals(scopeContext.Tag, ScopeDependencyProviderExtensions.ScopeVerifierTag))
                    return false;
                if (context.DependencyType == typeof(ITagVerifier) && context is ITagDependencyContext tagContext
                    && Equals(tagContext.Tag, TagDependencyProviderExtensions.TagVerifierTag))
                    return false;

                return context.Provider.UseScopeVerifier(verifier => scopes.Any(s => verifier.HasScope(context.Provider, s)));
            };
        }


    }
}
