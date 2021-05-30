using Mimp.SeeSharper.DependencyInjection.Abstraction;
using Mimp.SeeSharper.DependencyInjection.Tag.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Mimp.SeeSharper.DependencyInjection.Transient
{
    public class TransientDependencyBuilder : ITagDependencyBuilder
    {


        public Func<IDependencyContext, Type, bool> Constructible { get; }

        public Func<IDependencyContext, Type, object> Factory { get; }

        public IEnumerable<object> Tags { get; }


        public TransientDependencyBuilder(
            Func<IDependencyContext, Type, bool> constructible,
            Func<IDependencyContext, Type, object> factory
        )
        {
            Constructible = constructible ?? throw new ArgumentNullException(nameof(constructible));
            Factory = factory ?? throw new ArgumentNullException(nameof(factory));
            Tags = new List<object>();
        }


        public ITagDependencyBuilder Tag(object tag)
        {
            if (tag is null)
                throw new ArgumentNullException(nameof(tag));

            ((ICollection<object>)Tags).Add(tag);

            return this;
        }


        public IDependencyFactory BuildDependency()
        {
            return Tags.Any() ?
                new TagTransientDependencyFactory(Constructible, Factory,
                    TagDependencyProviderExtensions.Tagged(Tags.Select(t => new KeyValuePair<Type?, object>(null, t))), true)
                : new TransientDependencyFactory(Constructible, Factory, true);
        }


    }
}
