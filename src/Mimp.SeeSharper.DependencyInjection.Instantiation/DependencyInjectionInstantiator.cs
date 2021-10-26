using Mimp.SeeSharper.DependencyInjection.Abstraction;
using Mimp.SeeSharper.DependencyInjection.Tag.Abstraction;
using Mimp.SeeSharper.Instantiation.Abstraction;
using Mimp.SeeSharper.ObjectDescription;
using Mimp.SeeSharper.ObjectDescription.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Mimp.SeeSharper.DependencyInjection.Instantiation
{
    public class DependencyInjectionInstantiator : IInstantiator
    {


        public IDependencyProvider Provider { get; }

        public string TagKey { get; }


        public DependencyInjectionInstantiator(IDependencyProvider provider, string tagKey)
        {
            Provider = provider ?? throw new ArgumentNullException(nameof(provider));
            TagKey = tagKey ?? throw new ArgumentNullException(nameof(tagKey));
            if (string.IsNullOrWhiteSpace(TagKey))
                throw new ArgumentException($"{nameof(TagKey)} is empty.", nameof(tagKey));
        }


        public bool Instantiable(Type type, IObjectDescription description)
        {
            if (type is null)
                throw new ArgumentNullException(nameof(type));
            if (description is null)
                throw new ArgumentNullException(nameof(description));

            if (description.IsNullOrEmpty())
                return Provider.UseElse(type, _ => true, false);

            if (description.HasValue)
                return false;

            if (description.Children.All(pair => string.Equals(pair.Key, TagKey, StringComparison.InvariantCultureIgnoreCase)))
            {
                var tag = description.Children.First().Value;
                if (!tag.HasValue || tag.Value is null)
                    return false;

                return Provider.UseElse(tag.Value, type, _ => true, false);
            }

            return false;
        }


        public object? Instantiate(Type type, IObjectDescription description, out IObjectDescription? ignored)
        {
            if (type is null)
                throw new ArgumentNullException(nameof(type));
            if (description is null)
                throw new ArgumentNullException(nameof(description));
            if (!Instantiable(type, description))
                throw InstantiationException.GetNotMatchingTypeException(this, type, description);

            if (description.IsNullOrEmpty())
            {
                var dependency = Provider.Provide(type);
                if (dependency is not null)
                {
                    ignored = null;
                    return dependency.Dependency;
                }
            }

            if (description.Children.All(pair => string.Equals(pair.Key, TagKey, StringComparison.InvariantCultureIgnoreCase)))
            {
                var tag = description.Children.First().Value;
                if (tag.HasValue && tag.Value is not null)
                {
                    var dependency = Provider.Provide(tag.Value, type);
                    if (dependency is not null)
                    {
                        ignored = null;
                        return dependency.Dependency;
                    }
                }
            }

            throw InstantiationException.GetCanNotInstantiateException(type, description);
        }


        public object? Initialize(Type type, object? instance, IObjectDescription description, out IObjectDescription? ignored)
        {
            if (type is null)
                throw new ArgumentNullException(nameof(type));
            if (description is null)
                throw new ArgumentNullException(nameof(description));

            if (instance is null)
                return Instantiate(type, description, out ignored);

            ignored = description.IsNullOrEmpty() ? null : description;
            return instance;
        }


    }
}
