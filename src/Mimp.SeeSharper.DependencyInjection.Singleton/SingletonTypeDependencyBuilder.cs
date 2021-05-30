using Mimp.SeeSharper.DependencyInjection.Abstraction;
using Mimp.SeeSharper.DependencyInjection.Tag.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Mimp.SeeSharper.DependencyInjection.Singleton
{
    public class SingletonTypeDependencyBuilder : ITagTypeTagDependencyBuilder
    {


        public Type Type { get; }

        public Func<IDependencyContext, Type, Action<object>, object> Factory { get; }

        public IEnumerable<KeyValuePair<Type?, object>> Tags { get; }

        public IEnumerable<Type> Types { get; }


        public SingletonTypeDependencyBuilder(
            Type type,
            Func<IDependencyContext, Type, Action<object>, object> factory
        )
        {
            Type = type ?? throw new ArgumentNullException(nameof(type));
            Factory = factory ?? throw new ArgumentNullException(nameof(factory));
            Tags = new List<KeyValuePair<Type?, object>>();
            Types = new List<Type>();
        }


        public ITagTypeTagDependencyBuilder Tag(object tag)
        {
            if (tag is null)
                throw new ArgumentNullException(nameof(tag));

            ((ICollection<KeyValuePair<Type?, object>>)Tags)
                .Add(new KeyValuePair<Type?, object>(null, tag));

            return this;
        }

        ITagDependencyBuilder ITagDependencyBuilder.Tag(object tag) => Tag(tag);


        public ITagTypeTagDependencyBuilder As(Type type)
        {
            if (type is null)
                throw new ArgumentNullException(nameof(type));

            ((ICollection<Type>)Types).Add(type);

            return this;
        }

        ITagTypeDependencyBuilder ITagTypeDependencyBuilder.As(Type type) => As(type);

        ITypeDependencyBuilder ITypeDependencyBuilder.As(Type type) => As(type);


        public ITagTypeTagDependencyBuilder As(object tag, Type type)
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
            return Tags.Any()
                ? new TagSingletonDependencyFactory(constructible, Factory,
                    TagDependencyProviderExtensions.Tagged(Tags), true)
                : new SingletonDependencyFactory(constructible, Factory, true);
        }


    }
}
