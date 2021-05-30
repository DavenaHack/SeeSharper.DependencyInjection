using System;

namespace Mimp.SeeSharper.DependencyInjection.Abstraction
{
    public interface IDependencySourceBuilder
    {


        public IDependencySourceBuilder AddSource(Func<IDependencySource> source);

        public IDependencySourceBuilder AddDependency(Func<IDependencyFactory> factory);

        public IDependencySource BuildSource();


    }
}
