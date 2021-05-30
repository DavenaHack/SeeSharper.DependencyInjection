using Mimp.SeeSharper.DependencyInjection.Abstraction;
using System;

namespace Mimp.SeeSharper.DependencyInjection
{
    public static class DependencySourceBuilderExtensions
    {


        public static IDependencySourceBuilder AddSource(this IDependencySourceBuilder builder, Action<IDependencySourceBuilder> buildSource)
        {
            if (builder is null)
                throw new ArgumentNullException(nameof(builder));
            if (buildSource is null)
                throw new ArgumentNullException(nameof(buildSource));

            return builder.AddSource(() =>
            {
                var sourceBuilder = new DependencySourceBuilder();
                buildSource(sourceBuilder);
                return sourceBuilder.BuildSource();
            });
        }


    }
}
