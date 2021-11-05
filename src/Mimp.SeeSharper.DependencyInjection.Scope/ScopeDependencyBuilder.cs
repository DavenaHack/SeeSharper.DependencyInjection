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

        public IEnumerable<Func<IDependencyProvider, IScope>> Scopes { get; }

        public IEnumerable<Func<IDependencyProvider, object>> Tags { get; }


        public ScopeDependencyBuilder(
            Func<IDependencyContext, Type, bool> constructible,
            Func<IDependencyContext, Type, Action<object>, object> factory
        )
        {
            Constructible = constructible ?? throw new ArgumentNullException(nameof(constructible));
            Factory = factory ?? throw new ArgumentNullException(nameof(factory));
            Tags = new List<Func<IDependencyProvider, object>>();
            Scopes = new List<Func<IDependencyProvider, IScope>>();
        }


        public ITagScopeDependencyBuilder AddScope(Func<IDependencyProvider, IScope> scope)
        {
            if (scope is null)
                throw new ArgumentNullException(nameof(scope));

            ((ICollection<Func<IDependencyProvider, IScope>>)Scopes).Add(scope);

            return this;
        }

        IScopeDependencyBuilder IScopeDependencyBuilder.AddScope(Func<IDependencyProvider, IScope> scope) => AddScope(scope);


        public ITagScopeDependencyBuilder Tag(Func<IDependencyProvider, object> tag)
        {
            if (tag is null)
                throw new ArgumentNullException(nameof(tag));

            ((ICollection<Func<IDependencyProvider, object>>)Tags).Add(tag);

            return this;
        }

        ITagDependencyBuilder ITagDependencyBuilder.Tag(Func<IDependencyProvider, object> tag) => Tag(tag);


        public IDependencyFactory BuildDependency(IDependencyProvider provider)
        {
            if (provider is null)
                throw new ArgumentNullException(nameof(provider));

            var scopes = Scopes.Select(scope => scope(provider)
                ?? throw new NullReferenceException("At least one of the scope function return null."));
            if (!scopes.Any())
                scopes = new[] { DependencyInjection.Scope.Scopes.Any };
            var scope = new SeparateOrScope(scopes.ToArray());

            return Tags.Any()
                ? new TagScopeDependencyFactory(Constructible, Factory, scope,
                    TagDependencyProviderExtensions.Tagged(Tags.Select(tag =>
                    {
                        return new KeyValuePair<Type?, object>(null, tag(provider)
                            ?? throw new NullReferenceException("At least one of the tag function return null."));
                    })), true)
                : new ScopeDependencyFactory(Constructible, Factory, scope, true);
        }


    }
}
