using Mimp.SeeSharper.DependencyInjection.Abstraction;
using Mimp.SeeSharper.DependencyInjection.Scope.Abstraction;
using Mimp.SeeSharper.DependencyInjection.Tag.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Mimp.SeeSharper.DependencyInjection.Scope
{
    public class ScopeDependencyBuilder : ITagScopeDependencyBuilder
    {


        public Func<IDependencyContext, Type, bool> Constructible { get; }

        public Func<IDependencyContext, Type, Action<object>, object> Factory { get; }

        public Action<IDependencyProvider, Action<IScopeVerifier>> Verifier { get; }

        public IEnumerable<object?> Scopes { get; }

        public IEnumerable<object> Tags { get; }


        public ScopeDependencyBuilder(
            Func<IDependencyContext, Type, bool> constructible,
            Func<IDependencyContext, Type, Action<object>, object> factory,
            Action<IDependencyProvider, Action<IScopeVerifier>> verifier
        )
        {
            Constructible = constructible ?? throw new ArgumentNullException(nameof(constructible));
            Factory = factory ?? throw new ArgumentNullException(nameof(factory));
            Verifier = verifier ?? throw new ArgumentNullException(nameof(verifier));
            Tags = new List<object>();
            Scopes = new List<object?>();
        }


        public ITagScopeDependencyBuilder AddScope(object? scope)
        {
            if (scope is null)
                throw new ArgumentNullException(nameof(scope));

            ((ICollection<object?>)Scopes).Add(scope);

            return this;
        }

        IScopeDependencyBuilder IScopeDependencyBuilder.AddScope(object? scope) => AddScope(scope);


        public ITagScopeDependencyBuilder Tag(object tag)
        {
            if (tag is null)
                throw new ArgumentNullException(nameof(tag));

            ((ICollection<object>)Tags).Add(tag);

            return this;
        }

        ITagDependencyBuilder ITagDependencyBuilder.Tag(object tag) => Tag(tag);


        public IDependencyFactory BuildDependency()
        {
            var isScope = ScopeDependencyFactory.IsScopes(Scopes.Any() ? Scopes : new object?[] { null });
            return Tags.Any()
                ? new TagScopeDependencyFactory(Constructible, Factory, Verifier, isScope,
                    TagDependencyProviderExtensions.Tagged(Tags.Select(t => new KeyValuePair<Type?, object>(null, t))), true)
                : new ScopeDependencyFactory(Constructible, Factory, Verifier, isScope, true);
        }


    }
}
