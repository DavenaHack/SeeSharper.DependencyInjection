using Microsoft.Extensions.Configuration;
using Mimp.SeeSharper.DependencyInjection.Abstraction;
using Mimp.SeeSharper.DependencyInjection.Extensions.Configuration.Abstraction;
using Mimp.SeeSharper.DependencyInjection.Scope;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Mimp.SeeSharper.DependencyInjection.Extensions.Configuration
{
    public class ConfigurationDependencySourceBuilder : IConfigurationDependencySourceBuilder
    {


        public IConfigurationDependencyFactoryBuilder FactoryBuilder { get; }


        public ConfigurationDependencySourceBuilder(IConfigurationDependencyFactoryBuilder factoryBuilder)
        {
            FactoryBuilder = factoryBuilder ?? throw new ArgumentNullException(nameof(factoryBuilder));
        }


        public virtual IDependencySource GetSource(IConfiguration rootConfiguration, IConfiguration sourceConfiguration)
        {
            var builder = new DependencySourceBuilder();

            foreach (var src in GetSources(rootConfiguration, sourceConfiguration))
                builder.AddSource(src);

            foreach (var dependency in GetDependencies(rootConfiguration, sourceConfiguration))
                builder.AddDependency(dependency);

            var source = builder.BuildSource();

            var scopes = GetScopes(rootConfiguration, sourceConfiguration);
            if (scopes.Any())
                source = source.Scoped(scopes);

            return source;
        }


        private IEnumerable<IDependencySource> GetSources(IConfiguration rootConfiguration, IConfiguration sourceConfiguration)
        {
            var sources = sourceConfiguration.GetSection("sources");
            var i = 0;
            foreach (var source in sources.GetChildren())
            {
                if (!int.TryParse(source.Key, out var j) || i != j)
                    throw new InvalidOperationException($"{sources.Path} has to be a enumerable");
                yield return GetSource(rootConfiguration, source);
            }
        }

        private IEnumerable<IDependencyFactory> GetDependencies(IConfiguration rootConfiguration, IConfiguration dependencyConfiguration)
        {
            var dependencies = dependencyConfiguration.GetSection("dependencies");
            var i = 0;
            foreach (var source in dependencies.GetChildren())
            {
                if (!int.TryParse(source.Key, out var j) || i != j)
                    throw new InvalidOperationException($"{dependencies.Path} has to be a enumerable");
                yield return FactoryBuilder.GetFactory(rootConfiguration, source);
            }
        }


        protected virtual IEnumerable<object> GetScopes(IConfiguration rootConfiguration, IConfiguration sourceConfiguration)
        {
            var scopes = sourceConfiguration.GetSection("scopes");
            var i = 0;
            foreach (var scope in scopes.GetChildren())
            {
                if (!int.TryParse(scope.Key, out var j) || i != j)
                    throw new InvalidOperationException($"{scopes.Path} has to be a enumerable");
                if (scope.Value is null)
                    throw new InvalidOperationException($"{scope.Path} has to be a string");
                yield return scope.Value;
            }
        }


    }
}
