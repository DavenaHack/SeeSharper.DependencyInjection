using Mimp.SeeSharper.DependencyInjection.Abstraction;
using Mimp.SeeSharper.DependencyInjection.Tag.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Mimp.SeeSharper.DependencyInjection.Singleton
{
    public class SingletonDependencyBuilder : ITagDependencyBuilder
    {


        public Func<IDependencyContext, Type, bool> Constructible { get; }

        public Func<IDependencyContext, Type, Action<object>, object> Factory { get; }

        public IEnumerable<Func<IDependencyProvider, object>> Tags { get; }


        public SingletonDependencyBuilder(
            Func<IDependencyContext, Type, bool> constructible,
            Func<IDependencyContext, Type, Action<object>, object> factory
        )
        {
            Constructible = constructible ?? throw new ArgumentNullException(nameof(constructible));
            Factory = factory ?? throw new ArgumentNullException(nameof(factory));
            Tags = new List<Func<IDependencyProvider, object>>();
        }


        public ITagDependencyBuilder Tag(Func<IDependencyProvider, object> tag)
        {
            if (tag is null)
                throw new ArgumentNullException(nameof(tag));

            ((ICollection<Func<IDependencyProvider, object>>)Tags).Add(tag);

            return this;
        }


        public IDependencyFactory BuildDependency(IDependencyProvider provider)
        {
            if (provider is null)
                throw new ArgumentNullException(nameof(provider));

            return Tags.Any()
                ? new TagSingletonDependencyFactory(Constructible, Factory,
                    TagDependencyProviderExtensions.Tagged(Tags.Select(tag =>
                    {
                        return new KeyValuePair<Type?, object>(null, tag(provider)
                            ?? throw new NullReferenceException("At least one of the tag function return null."));
                    })), true)
                : new SingletonDependencyFactory(Constructible, Factory, true);
        }


    }
}
