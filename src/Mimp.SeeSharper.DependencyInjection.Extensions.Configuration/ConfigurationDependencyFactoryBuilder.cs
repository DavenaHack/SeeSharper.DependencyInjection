using Microsoft.Extensions.Configuration;
using Mimp.SeeSharper.DependencyInjection.Abstraction;
using Mimp.SeeSharper.DependencyInjection.Extensions.Configuration.Abstraction;
using Mimp.SeeSharper.DependencyInjection.Instantiation;
using Mimp.SeeSharper.DependencyInjection.Scope;
using Mimp.SeeSharper.DependencyInjection.Scope.Abstraction;
using Mimp.SeeSharper.DependencyInjection.Singleton;
using Mimp.SeeSharper.DependencyInjection.Tag.Abstraction;
using Mimp.SeeSharper.DependencyInjection.Transient;
using Mimp.SeeSharper.ObjectDescription.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Mimp.SeeSharper.DependencyInjection.Extensions.Configuration
{
    public class ConfigurationDependencyFactoryBuilder : IConfigurationDependencyFactoryBuilder
    {


        public Func<string, Type> ResolveType { get; }


        public Func<IDependencyProvider, IConfigurationSection, IScope?> ResolveScope { get; }


        public ConfigurationDependencyFactoryBuilder(Func<string, Type> resolveType, Func<IDependencyProvider, IConfigurationSection, IScope?> resolveScope)
        {
            ResolveType = resolveType ?? throw new ArgumentNullException(nameof(resolveType));
            ResolveScope = resolveScope ?? throw new ArgumentNullException(nameof(resolveScope));
        }


        public IDependencyFactory GetFactory(IDependencyProvider provider, IConfiguration rootConfiguration, IConfiguration dependencyConfiguration)
        {
            if (rootConfiguration is null)
                throw new ArgumentNullException(nameof(rootConfiguration));
            if (dependencyConfiguration is null)
                throw new ArgumentNullException(nameof(dependencyConfiguration));

            var builder = GetBuilder(provider, rootConfiguration, dependencyConfiguration);

            ConfigureBuilder(provider, builder, rootConfiguration, dependencyConfiguration);

            return builder.BuildDependency(provider);
        }


        protected virtual IDependencyBuilder GetBuilder(IDependencyProvider provider, IConfiguration rootConfiguration, IConfiguration dependencyConfiguration)
        {
            var lifetime = dependencyConfiguration.GetSection("lifetime");
            if (lifetime.Value is null)
                throw new InvalidOperationException($"{lifetime.Path} required a lifetime.");
            var type = dependencyConfiguration.GetSection("type");
            if (type.Value is null)
                throw new InvalidOperationException($"{type.Path} required a type.");

            var dependencyType = ResolveType(type.Value);
            var instantiateValues = dependencyConfiguration.GetSection("instantiate").ToDescription();
            var initializeValues = dependencyConfiguration.GetSection("initialize").ToDescription();

            return (lifetime.Value.ToLower()) switch
            {
                "singleton" or "single" =>
                    new SingletonTypeDependencyBuilder(dependencyType,
                        InstantiationDependencySourceBuilderExtensions.InstantiateInitialize(dependencyType, instantiateValues, initializeValues)),
                "scope" or "scoped" =>
                    new ScopeTypeDependencyBuilder(dependencyType,
                        InstantiationDependencySourceBuilderExtensions.InstantiateInitialize(dependencyType, instantiateValues, initializeValues)),
                "transient" =>
                    new TransientTypeDependencyBuilder(dependencyType,
                        InstantiationDependencySourceBuilderExtensions.Construct(dependencyType, instantiateValues, initializeValues)),
                _ => throw new InvalidOperationException($"Unkown lifetime: {lifetime.Value}"),
            };
        }


        protected virtual void ConfigureBuilder(IDependencyProvider provider, IDependencyBuilder builder, IConfiguration rootConfiguration, IConfiguration dependencyConfiguration)
        {
            ConfigureTags(provider, builder, rootConfiguration, dependencyConfiguration);
            ConfigureTypes(provider, builder, rootConfiguration, dependencyConfiguration);
            ConfigureTaggedTypes(provider, builder, rootConfiguration, dependencyConfiguration);
            ConfigureScopes(provider, builder, rootConfiguration, dependencyConfiguration);
        }


        protected virtual IEnumerable<object> GetTags(IDependencyProvider provider, IConfiguration rootConfiguration, IConfiguration dependencyConfiguration)
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

        protected virtual void SetTags(IDependencyProvider provider, ITagDependencyBuilder builder, IEnumerable<object> tags, IConfiguration rootConfiguration, IConfiguration dependencyConfiguration)
        {
            foreach (var tag in tags)
                builder.Tag(tag);
        }

        protected virtual void ConfigureTags(IDependencyProvider provider, IDependencyBuilder builder, IConfiguration rootConfiguration, IConfiguration dependencyConfiguration)
        {
            var tags = GetTags(provider, rootConfiguration, dependencyConfiguration);
            if (tags.Any())
                if (builder is ITagDependencyBuilder tagBuilder)
                    SetTags(provider, tagBuilder, tags, rootConfiguration, dependencyConfiguration);
                else
                    throw new NotSupportedException($"Tags are configured, but {builder} don't support it");
        }


        protected virtual IEnumerable<Type> GetTypes(IDependencyProvider provider, IConfiguration rootConfiguration, IConfiguration dependencyConfiguration)
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

        protected virtual void SetTypes(IDependencyProvider provider, ITypeDependencyBuilder builder, IEnumerable<Type> types, IConfiguration rootConfiguration, IConfiguration dependencyConfiguration)
        {
            foreach (var type in types)
                builder.As(type);
        }

        protected virtual void ConfigureTypes(IDependencyProvider provider, IDependencyBuilder builder, IConfiguration rootConfiguration, IConfiguration dependencyConfiguration)
        {
            var types = GetTypes(provider, rootConfiguration, dependencyConfiguration);
            if (types.Any())
                if (builder is ITypeDependencyBuilder typeBuilder)
                    SetTypes(provider, typeBuilder, types, rootConfiguration, dependencyConfiguration);
                else
                    throw new NotSupportedException($"Types are configured, but {builder} don't support it");
        }


        protected virtual IEnumerable<KeyValuePair<object, Type>> GetTaggedTypes(IDependencyProvider provider, IConfiguration rootConfiguration, IConfiguration dependencyConfiguration)
        {
            var types = dependencyConfiguration.GetSection("taggedTypes");
            foreach (var type in types.GetChildren())
            {
                if (type.Value is null)
                    throw new InvalidOperationException($"{type.Path} has to be a string");
                yield return new KeyValuePair<object, Type>(type.Key, ResolveType(type.Value));
            }
        }

        protected virtual void SetTaggedTypes(IDependencyProvider provider, ITagTypeDependencyBuilder builder, IEnumerable<KeyValuePair<object, Type>> taggedTypes, IConfiguration rootConfiguration, IConfiguration dependencyConfiguration)
        {
            foreach (var pair in taggedTypes)
                builder.As(pair.Key, pair.Value);
        }

        protected virtual void ConfigureTaggedTypes(IDependencyProvider provider, IDependencyBuilder builder, IConfiguration rootConfiguration, IConfiguration dependencyConfiguration)
        {
            var types = GetTaggedTypes(provider, rootConfiguration, dependencyConfiguration);
            if (types.Any())
                if (builder is ITagTypeDependencyBuilder typeBuilder)
                    SetTaggedTypes(provider, typeBuilder, types, rootConfiguration, dependencyConfiguration);
                else
                    throw new NotSupportedException($"Types are configured, but {builder} don't support it");
        }


        protected virtual IEnumerable<IScope> GetScopes(IDependencyProvider provider, IConfiguration rootConfiguration, IConfiguration dependencyConfiguration)
        {
            var scopes = dependencyConfiguration.GetSection("scopes");
            var i = 0;
            foreach (var value in scopes.GetChildren())
            {
                if (!int.TryParse(value.Key, out var j) || i != j)
                    throw new InvalidOperationException($"{scopes.Path} has to be a enumerable");

                var scope = ResolveScope(provider, value);
                if (scope is not null)
                    yield return scope;
            }
        }

        protected virtual void SetScopes(IDependencyProvider provider, IScopeDependencyBuilder builder, IEnumerable<IScope> scopes, IConfiguration rootConfiguration, IConfiguration dependencyConfiguration)
        {
            foreach (var scope in scopes)
                builder.AddScope(scope);
        }

        protected virtual void ConfigureScopes(IDependencyProvider provider, IDependencyBuilder builder, IConfiguration rootConfiguration, IConfiguration dependencyConfiguration)
        {
            var scopes = GetScopes(provider, rootConfiguration, dependencyConfiguration);
            if (scopes.Any())
                if (builder is IScopeDependencyBuilder scopeBuilder)
                    SetScopes(provider, scopeBuilder, scopes, rootConfiguration, dependencyConfiguration);
                else
                    throw new NotSupportedException($"Scopes are configured, but {builder} don't support it");
        }


    }
}
