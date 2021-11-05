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

        public IEnumerable<KeyValuePair<Type?, Func<IDependencyProvider, object>>> Tags { get; }

        public IEnumerable<Func<IDependencyProvider, IScope>> Scopes { get; }

        public IEnumerable<Type> Types { get; }


        public ScopeTypeDependencyBuilder(
            Type type,
            Func<IDependencyContext, Type, Action<object>, object> factory
        )
        {
            Type = type ?? throw new ArgumentNullException(nameof(type));
            Factory = factory ?? throw new ArgumentNullException(nameof(factory));
            Tags = new List<KeyValuePair<Type?, Func<IDependencyProvider, object>>>();
            Scopes = new List<Func<IDependencyProvider, IScope>>();
            Types = new List<Type>();
        }


        public ITagScopeTypeDependencyBuilder AddScope(Func<IDependencyProvider, IScope> scope)
        {
            if (scope is null)
                throw new ArgumentNullException(nameof(scope));

            ((ICollection<Func<IDependencyProvider, IScope>>)Scopes).Add(scope);

            return this;
        }

        ITagScopeDependencyBuilder ITagScopeDependencyBuilder.AddScope(Func<IDependencyProvider, IScope> scope) => AddScope(scope);

        IScopeTypeDependencyBuilder IScopeTypeDependencyBuilder.AddScope(Func<IDependencyProvider, IScope> scope) => AddScope(scope);

        IScopeDependencyBuilder IScopeDependencyBuilder.AddScope(Func<IDependencyProvider, IScope> scope) => AddScope(scope);


        public ITagScopeTypeDependencyBuilder Tag(Func<IDependencyProvider, object> tag)
        {
            if (tag is null)
                throw new ArgumentNullException(nameof(tag));

            ((ICollection<KeyValuePair<Type?, Func<IDependencyProvider, object>>>)Tags)
                .Add(new KeyValuePair<Type?, Func<IDependencyProvider, object>>(null, tag));

            return this;
        }

        ITagScopeDependencyBuilder ITagScopeDependencyBuilder.Tag(Func<IDependencyProvider, object> tag) => Tag(tag);

        ITagDependencyBuilder ITagDependencyBuilder.Tag(Func<IDependencyProvider, object> tag) => Tag(tag);

        ITagTypeTagDependencyBuilder ITagTypeTagDependencyBuilder.Tag(Func<IDependencyProvider, object> tag) => Tag(tag);


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

        ITagTypeTagDependencyBuilder ITagTypeTagDependencyBuilder.As(Type type) => As(type);


        public ITagScopeTypeDependencyBuilder As(Func<IDependencyProvider, object> tag, Type type)
        {
            if (tag is null)
                throw new ArgumentNullException(nameof(tag));
            if (type is null)
                throw new ArgumentNullException(nameof(type));

            ((ICollection<KeyValuePair<Type?, Func<IDependencyProvider, object>>>)Tags)
                .Add(new KeyValuePair<Type?, Func<IDependencyProvider, object>>(type, tag));

            return this;
        }

        ITagTypeDependencyBuilder ITagTypeDependencyBuilder.As(Func<IDependencyProvider, object> tag, Type type) => As(tag, type);

        ITagTypeTagDependencyBuilder ITagTypeTagDependencyBuilder.As(Func<IDependencyProvider, object> tag, Type type) => As(tag, type);

        public IDependencyFactory BuildDependency(IDependencyProvider provider)
        {
            if (provider is null)
                throw new ArgumentNullException(nameof(provider));

            var scopes = Scopes.Select(scope => scope(provider)
                ?? throw new NullReferenceException("At least one of the scope function return null."));
            if (!scopes.Any())
                scopes = new[] { DependencyInjection.Scope.Scopes.Any };
            var scope = new SeparateOrScope(scopes.ToArray());

            var constructible = BaseDependencyFactory.TypeConstructible(Types.Any() ? Types : new[] { Type });
            return Tags.Any()
                ? new TagScopeDependencyFactory(constructible, Factory, scope,
                    TagDependencyProviderExtensions.Tagged(Tags.Select(tag =>
                    {
                        return new KeyValuePair<Type?, object>(tag.Key, tag.Value(provider)
                            ?? throw new NullReferenceException("At least one of the tag function return null."));
                    })), true)
                : new ScopeDependencyFactory(constructible, Factory, scope, true);
        }


    }
}
