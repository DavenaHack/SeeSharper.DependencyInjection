using Microsoft.Extensions.Configuration;
using Mimp.SeeSharper.DependencyInjection.Abstraction;
using Mimp.SeeSharper.DependencyInjection.Extensions.Configuration.Abstraction;
using Mimp.SeeSharper.DependencyInjection.Scope;
using Mimp.SeeSharper.DependencyInjection.Scope.Abstraction;
using System;
using System.Collections.Generic;

namespace Mimp.SeeSharper.DependencyInjection.Extensions.Configuration
{
    public class ConfigurationDependencySourceBuilder : IConfigurationDependencySourceBuilder
    {


        public IConfigurationDependencyFactoryBuilder FactoryBuilder { get; }


        public Func<IDependencyProvider, IConfigurationSection, IScope?> ResolveScope { get; }


        public ConfigurationDependencySourceBuilder(IConfigurationDependencyFactoryBuilder factoryBuilder, Func<IDependencyProvider, IConfigurationSection, IScope?> resolveScope)
        {
            FactoryBuilder = factoryBuilder ?? throw new ArgumentNullException(nameof(factoryBuilder));
            ResolveScope = resolveScope ?? throw new ArgumentNullException(nameof(resolveScope));
        }


        public virtual IDependencySource GetSource(IDependencyProvider provider, IConfiguration rootConfiguration, IConfiguration sourceConfiguration)
        {
            var builder = new DependencySourceBuilder();

            foreach (var src in GetSources(provider, rootConfiguration, sourceConfiguration))
                builder.AddSource(src);

            foreach (var dependency in GetDependencies(provider, rootConfiguration, sourceConfiguration))
                builder.AddDependency(dependency);

            var source = builder.BuildSource(provider);

            var scope = GetScope(provider, rootConfiguration, sourceConfiguration);
            if (scope is not null)
                source = source.Scoped(scope);

            return source;
        }


        private IEnumerable<IDependencySource> GetSources(IDependencyProvider provider, IConfiguration rootConfiguration, IConfiguration sourceConfiguration)
        {
            var sources = sourceConfiguration.GetSection("sources");
            var i = 0;
            foreach (var source in sources.GetChildren())
            {
                if (!int.TryParse(source.Key, out var j) || i != j)
                    throw new InvalidOperationException($"{sources.Path} has to be a enumerable");
                yield return GetSource(provider, rootConfiguration, source);
            }
        }

        private IEnumerable<IDependencyFactory> GetDependencies(IDependencyProvider provider, IConfiguration rootConfiguration, IConfiguration dependencyConfiguration)
        {
            var dependencies = dependencyConfiguration.GetSection("dependencies");
            var i = 0;
            foreach (var source in dependencies.GetChildren())
            {
                if (!int.TryParse(source.Key, out var j) || i != j)
                    throw new InvalidOperationException($"{dependencies.Path} has to be a enumerable");
                yield return FactoryBuilder.GetFactory(provider, rootConfiguration, source);
            }
        }


        protected virtual IScope? GetScope(IDependencyProvider provider, IConfiguration rootConfiguration, IConfiguration sourceConfiguration)
        {
            return ResolveScope(provider, sourceConfiguration.GetSection("scope"));
        }


    }
}
