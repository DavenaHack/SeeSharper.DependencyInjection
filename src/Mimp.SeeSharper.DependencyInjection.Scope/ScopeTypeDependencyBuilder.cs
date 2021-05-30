using Mimp.SeeSharper.DependencyInjection.Abstraction;
using Mimp.SeeSharper.DependencyInjection.Scope.Abstraction;
using Mimp.SeeSharper.DependencyInjection.Tag.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Mimp.SeeSharper.DependencyInjection.Scope
{
    public class ScopeTypeDependencyBuilder : ITagScopeTypeDependencyBuilder
    {


        public Type Type { get; }

        public Func<IDependencyContext, Type, Action<object>, object> Factory { get; }

        public Action<IDependencyProvider, Action<IScopeVerifier>> Verifier { get; }

        public IEnumerable<KeyValuePair<Type?, object>> Tags { get; }

        public IEnumerable<object?> Scopes { get; }

        public IEnumerable<Type> Types { get; }


        public ScopeTypeDependencyBuilder(
            Type type,
            Func<IDependencyContext, Type, Action<object>, object> factory,
            Action<IDependencyProvider, Action<IScopeVerifier>> verifier
        )
        {
            Type = type ?? throw new ArgumentNullException(nameof(type));
            Factory = factory ?? throw new ArgumentNullException(nameof(factory));
            Verifier = verifier ?? throw new ArgumentNullException(nameof(verifier));
            Tags = new List<KeyValuePair<Type?, object>>();
            Scopes = new List<object?>();
            Types = new List<Type>();
        }


        public ITagScopeTypeDependencyBuilder AddScope(object? scope)
        {
            if (scope is null)
                throw new ArgumentNullException(nameof(scope));

            ((ICollection<object?>)Scopes).Add(scope);

            return this;
        }

        ITagScopeDependencyBuilder ITagScopeDependencyBuilder.AddScope(object? scope) => AddScope(scope);

        IScopeTypeDependencyBuilder IScopeTypeDependencyBuilder.AddScope(object? scope) => AddScope(scope);

        IScopeDependencyBuilder IScopeDependencyBuilder.AddScope(object? scope) => AddScope(scope);


        public ITagScopeTypeDependencyBuilder Tag(object tag)
        {
            if (tag is null)
                throw new ArgumentNullException(nameof(tag));

            ((ICollection<KeyValuePair<Type?, object>>)Tags)
                .Add(new KeyValuePair<Type?, object>(null, tag));

            return this;
        }

        ITagScopeDependencyBuilder ITagScopeDependencyBuilder.Tag(object tag) => Tag(tag);

        ITagDependencyBuilder ITagDependencyBuilder.Tag(object tag) => Tag(tag);


        public ITagScopeTypeDependencyBuilder As(Type type)
        {
            if (type is null)
                throw new ArgumentNullException(nameof(type));

            ((ICollection<Type>)Types).Add(type);

            return this;
        }

        ITagTypeDependencyBuilder ITagTypeDependencyBuilder.As(Type type) => As(type);

        IScopeTypeDependencyBuilder IScopeTypeDependencyBuilder.As(Type type) => As(type);

        ITypeDependencyBuilder ITypeDependencyBuilder.As(Type type) => As(type);


        public ITagScopeTypeDependencyBuilder As(object tag, Type type)
        {
            if (tag is null)
                throw new ArgumentNullException(nameof(tag));
            if (type is null)
                throw new ArgumentNullException(nameof(type));

            ((ICollection<KeyValuePair<Type?, object>>)Tags)
                .Add(new KeyValuePair<Type?, object>(type, tag));

            return this;
        }

        ITagTypeDependencyBuilder ITagTypeDependencyBuilder.As(object tag, Type type) => As(tag, type);


        public IDependencyFactory BuildDependency()
        {
            var constructible = BaseDependencyFactory.TypeConstructible(Types.Any() ? Types : new[] { Type });
            var isScope = ScopeDependencyFactory.IsScopes(Scopes.Any() ? Scopes : new object?[] { null });
            return Tags.Any()
                ? new TagScopeDependencyFactory(constructible, Factory, Verifier, isScope,
                    TagDependencyProviderExtensions.Tagged(Tags), true)
                : new ScopeDependencyFactory(constructible, Factory, Verifier, isScope, true);
        }


    }
}
