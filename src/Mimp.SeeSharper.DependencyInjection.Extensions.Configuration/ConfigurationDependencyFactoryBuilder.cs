using Microsoft.Extensions.Configuration;
using Mimp.SeeSharper.DependencyInjection.Abstraction;
using Mimp.SeeSharper.DependencyInjection.Extensions.Configuration.Abstraction;
using Mimp.SeeSharper.DependencyInjection.Instantiation;
using Mimp.SeeSharper.DependencyInjection.Scope;
using Mimp.SeeSharper.DependencyInjection.Scope.Abstraction;
using Mimp.SeeSharper.DependencyInjection.Singleton;
using Mimp.SeeSharper.DependencyInjection.Tag.Abstraction;
using Mimp.SeeSharper.DependencyInjection.Transient;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Mimp.SeeSharper.DependencyInjection.Extensions.Configuration
{
    public class ConfigurationDependencyFactoryBuilder : IConfigurationDependencyFactoryBuilder
    {


        public Func<string, Type> ResolveType { get; }


        public ConfigurationDependencyFactoryBuilder(Func<string, Type> resolveType)
        {
            ResolveType = resolveType ?? throw new ArgumentNullException(nameof(resolveType));
        }


        public IDependencyFactory GetFactory(IConfiguration rootConfiguration, IConfiguration dependencyConfiguration)
        {
            if (rootConfiguration is null)
                throw new ArgumentNullException(nameof(rootConfiguration));
            if (dependencyConfiguration is null)
                throw new ArgumentNullException(nameof(dependencyConfiguration));

            var builder = GetBuilder(rootConfiguration, dependencyConfiguration);

            ConfigureBuilder(builder, rootConfiguration, dependencyConfiguration);

            return builder.BuildDependency();
        }


        protected virtual IDependencyBuilder GetBuilder(IConfiguration rootConfiguration, IConfiguration dependencyConfiguration)
        {
            var lifetime = dependencyConfiguration.GetSection("lifetime");
            if (lifetime.Value is null)
                throw new InvalidOperationException($"{lifetime.Path} required a lifetime.");
            var type = dependencyConfiguration.GetSection("type");
            if (type.Value is null)
                throw new InvalidOperationException($"{type.Path} required a type.");

            var dependencyType = ResolveType(type.Value);
            var instantiateValues = ConfigurationToInstantiateValues(dependencyConfiguration.GetSection("instantiate"));
            var initializeValues = ConfigurationToInstantiateValues(dependencyConfiguration.GetSection("initialize"));

            return (lifetime.Value.ToLower()) switch
            {
                "singleton" or "single" =>
                    new SingletonTypeDependencyBuilder(dependencyType,
                        InstantiationDependencySourceBuilderExtensions.InstantiateInitialize(dependencyType, instantiateValues, initializeValues)),
                "scope" or "scoped" =>
                    new ScopeTypeDependencyBuilder(dependencyType,
                        InstantiationDependencySourceBuilderExtensions.InstantiateInitialize(dependencyType, instantiateValues, initializeValues),
                        ScopeDependencyProviderExtensions.UseScopeVerifier),
                "transient" =>
                    new TransientTypeDependencyBuilder(dependencyType,
                        InstantiationDependencySourceBuilderExtensions.Construct(dependencyType, instantiateValues, initializeValues)),
                _ => throw new InvalidOperationException($"Unkown lifetime: {lifetime.Value}"),
            };
        }

        protected IDictionary<string, object> ConfigurationToInstantiateValues(IConfiguration configuration)
        {
            var dictionary = new Dictionary<string, object>();
            foreach (var child in configuration.GetChildren())
                dictionary[child.Key] = child.Value is not null ? child.Value
                    : ConfigurationToInstantiateValues(child);
            return dictionary;
        }


        protected virtual void ConfigureBuilder(IDependencyBuilder builder, IConfiguration rootConfiguration, IConfiguration dependencyConfiguration)
        {
            ConfigureTags(builder, rootConfiguration, dependencyConfiguration);
            ConfigureTypes(builder, rootConfiguration, dependencyConfiguration);
            ConfigureTaggedTypes(builder, rootConfiguration, dependencyConfiguration);
            ConfigureScopes(builder, rootConfiguration, dependencyConfiguration);
        }


        protected virtual IEnumerable<object> GetTags(IConfiguration rootConfiguration, IConfiguration dependencyConfiguration)
        {
            var tags = dependencyConfiguration.GetSection("tags");
            var i = 0;
            foreach (var tag in tags.GetChildren())
            {
                if (!int.TryParse(tag.Key, out var j) || i != j)
                    throw new InvalidOperationException($"{tags.Path} has to be a enumerable");
                if (tag.Value is null)
                    throw new InvalidOperationException($"{tag.Path} has to be a string");
                yield return tag.Value;
            }
        }

        protected virtual void SetTags(ITagDependencyBuilder builder, IEnumerable<object> tags, IConfiguration rootConfiguration, IConfiguration dependencyConfiguration)
        {
            foreach (var tag in tags)
                builder.Tag(tag);
        }

        protected virtual void ConfigureTags(IDependencyBuilder builder, IConfiguration rootConfiguration, IConfiguration dependencyConfiguration)
        {
            var tags = GetTags(rootConfiguration, dependencyConfiguration);
            if (tags.Any())
                if (builder is ITagDependencyBuilder tagBuilder)
                    SetTags(tagBuilder, tags, rootConfiguration, dependencyConfiguration);
                else
                    throw new NotSupportedException($"Tags are configured, but {builder} don't support it");
        }


        protected virtual IEnumerable<Type> GetTypes(IConfiguration rootConfiguration, IConfiguration dependencyConfiguration)
        {
            var types = dependencyConfiguration.GetSection("types");
            var i = 0;
            foreach (var tag in types.GetChildren())
            {
                if (!int.TryParse(tag.Key, out var j) || i != j)
                    throw new InvalidOperationException($"{types.Path} has to be a enumerable");
                if (tag.Value is null)
                    throw new InvalidOperationException($"{tag.Path} has to be a string");
                yield return ResolveType(tag.Value);
            }
        }

        protected virtual void SetTypes(ITypeDependencyBuilder builder, IEnumerable<Type> types, IConfiguration rootConfiguration, IConfiguration dependencyConfiguration)
        {
            foreach (var type in types)
                builder.As(type);
        }

        protected virtual void ConfigureTypes(IDependencyBuilder builder, IConfiguration rootConfiguration, IConfiguration dependencyConfiguration)
        {
            var types = GetTypes(rootConfiguration, dependencyConfiguration);
            if (types.Any())
                if (builder is ITypeDependencyBuilder typeBuilder)
                    SetTypes(typeBuilder, types, rootConfiguration, dependencyConfiguration);
                else
                    throw new NotSupportedException($"Types are configured, but {builder} don't support it");
        }


        protected virtual IEnumerable<KeyValuePair<object, Type>> GetTaggedTypes(IConfiguration rootConfiguration, IConfiguration dependencyConfiguration)
        {
            var types = dependencyConfiguration.GetSection("taggedTypes");
            foreach (var type in types.GetChildren())
            {
                if (type.Value is null)
                    throw new InvalidOperationException($"{type.Path} has to be a string");
                yield return new KeyValuePair<object, Type>(type.Key, ResolveType(type.Value));
            }
        }

        protected virtual void SetTaggedTypes(ITagTypeDependencyBuilder builder, IEnumerable<KeyValuePair<object, Type>> taggedTypes, IConfiguration rootConfiguration, IConfiguration dependencyConfiguration)
        {
            foreach (var pair in taggedTypes)
                builder.As(pair.Key, pair.Value);
        }

        protected virtual void ConfigureTaggedTypes(IDependencyBuilder builder, IConfiguration rootConfiguration, IConfiguration dependencyConfiguration)
        {
            var types = GetTaggedTypes(rootConfiguration, dependencyConfiguration);
            if (types.Any())
                if (builder is ITagTypeDependencyBuilder typeBuilder)
                    SetTaggedTypes(typeBuilder, types, rootConfiguration, dependencyConfiguration);
                else
                    throw new NotSupportedException($"Types are configured, but {builder} don't support it");
        }


        protected virtual IEnumerable<object> GetScopes(IConfiguration rootConfiguration, IConfiguration dependencyConfiguration)
        {
            var scopes = dependencyConfiguration.GetSection("scopes");
            var i = 0;
            foreach (var scope in scopes.GetChildren())
            {
                if (!int.TryParse(scope.Key, out var j) || i != j)
                    throw new InvalidOperationException($"{scopes.Path} has to be a enumerable");
                if (scope.Value is null)
                    throw new InvalidOperationException($"{scope.Path} has to be a string");

                var scopeParts = scope.Value.Split('.');
                yield return scopeParts.Length > 1 ? SubScope.Create(scopeParts) : (object)scopeParts[0];
            }
        }

        protected virtual void SetScopes(IScopeDependencyBuilder builder, IEnumerable<object> scopes, IConfiguration rootConfiguration, IConfiguration dependencyConfiguration)
        {
            foreach (var scope in scopes)
                builder.AddScope(scope);
        }

        protected virtual void ConfigureScopes(IDependencyBuilder builder, IConfiguration rootConfiguration, IConfiguration dependencyConfiguration)
        {
            var scopes = GetScopes(rootConfiguration, dependencyConfiguration);
            if (scopes.Any())
                if (builder is IScopeDependencyBuilder scopeBuilder)
                    SetScopes(scopeBuilder, scopes, rootConfiguration, dependencyConfiguration);
                else
                    throw new NotSupportedException($"Scopes are configured, but {builder} don't support it");
        }


    }
}
