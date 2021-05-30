using Mimp.SeeSharper.DependencyInjection.Abstraction;
using Mimp.SeeSharper.DependencyInjection.Tag.Abstraction;
using Mimp.SeeSharper.Instantiation.Abstraction;
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


        public bool Instantiable(Type type, object? instantiateValues)
        {
            if (type is null)
                throw new ArgumentNullException(nameof(type));

            if (instantiateValues is null)
                return Provider.UseElse(type, _ => true, false);

            if (instantiateValues is not IEnumerable<KeyValuePair<string?, object?>> keyValues)
                return false;

            if (!keyValues.Any())
                return Provider.UseElse(type, _ => true, false);

            if (keyValues.All(pair => string.Equals(pair.Key, TagKey, StringComparison.InvariantCultureIgnoreCase)))
                return Provider.UseElse(keyValues.First().Value!, type, _ => true, false);

            return false;
        }

        public object? Instantiate(Type type, object? instantiateValues, out object? ignoredInstantiateValues)
        {
            if (type is null)
                throw new ArgumentNullException(nameof(type));
            if (!Instantiable(type, instantiateValues))
                throw InstantiationException.GetNotMatchingTypeException(this, type);

            if (instantiateValues is null || instantiateValues is IEnumerable<KeyValuePair<string, object>> keyValues && !keyValues.Any())
            {
                var dependency = Provider.Provide(type);
                if (dependency is not null)
                {
                    ignoredInstantiateValues = instantiateValues;
                    return dependency.Dependency;
                }
            }

            throw InstantiationException.GetCanNotInstantiateExeption(type, instantiateValues);
        }

        public void Initialize(object? instance, object? initializeValues, out object? ignoredInitializeValues)
        {
            ignoredInitializeValues = initializeValues;
        }


    }
}
