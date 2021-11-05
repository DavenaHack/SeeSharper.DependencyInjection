using System;

namespace Mimp.SeeSharper.DependencyInjection.Abstraction
{
    public interface IDependencySourceBuilder
    {


        public IDependencySourceBuilder AddSource(Func<IDependencyProvider, IDependencySource> source);

        public IDependencySourceBuilder AddDependency(Func<IDependencyProvider, IDependencyFactory> factory);


        public IDependencySource BuildSource(IDependencyProvider provider);


    }
}
