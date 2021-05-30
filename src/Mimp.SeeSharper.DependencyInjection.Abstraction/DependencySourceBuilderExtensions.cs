using System;

namespace Mimp.SeeSharper.DependencyInjection.Abstraction
{
    public static class DependencySourceBuilderExtensions
    {


        public static IDependencySourceBuilder AddSource(this IDependencySourceBuilder builder, IDependencySource source)
        {
            if (builder is null)
                throw new ArgumentNullException(nameof(builder));
            if (source is null)
                throw new ArgumentNullException(nameof(source));

            return builder.AddSource(() => source);
        }

        public static IDependencySourceBuilder AddDependency(this IDependencySourceBuilder builder, IDependencyFactory factory)
        {
            if (builder is null)
                throw new ArgumentNullException(nameof(builder));
            if (factory is null)
                throw new ArgumentNullException(nameof(factory));

            return builder.AddDependency(() => factory);
        }


    }
}
