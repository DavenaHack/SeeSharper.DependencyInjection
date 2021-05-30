using Mimp.SeeSharper.DependencyInjection.Abstraction;
using Mimp.SeeSharper.DependencyInjection.Singleton;
using Mimp.SeeSharper.DependencyInjection.Tag.Abstraction;
using System;

namespace Mimp.SeeSharper.DependencyInjection.Tag
{
    public static class TagDependencySourceBuilderExtensions
    {


        public static IDependencySourceBuilder UseTagVerifier(
            this IDependencySourceBuilder builder,
            Func<IDependencyProvider, ITagVerifier> verifierFactory
        )
        {
            if (builder is null)
                throw new ArgumentNullException(nameof(builder));
            if (verifierFactory is null)
                throw new ArgumentNullException(nameof(verifierFactory));

            return builder.AddSingleton(verifierFactory)
                .Tag(TagDependencyProviderExtensions.TagVerifierTag);
        }

        public static IDependencySourceBuilder UseTagVerifier(
            this IDependencySourceBuilder builder
        )
        {
            if (builder is null)
                throw new ArgumentNullException(nameof(builder));

            return builder.UseTagVerifier(GetDefaultTagVerifier);
        }

        public static ITagVerifier GetDefaultTagVerifier(IDependencyProvider provider)
        {
            return new TagVerifier();
        }


    }
}
