using Mimp.SeeSharper.DependencyInjection.Abstraction;
using System;

namespace Mimp.SeeSharper.DependencyInjection.Tag.Abstraction
{
    public class TagDependencySourceBuilder : ITagDependencySourceBuilder
    {


        public IDependencySourceBuilder SourceBuilder { get; }

        public ITagDependencyBuilder Builder { get; }


        public TagDependencySourceBuilder(
            IDependencySourceBuilder sourceBuilder,
            ITagDependencyBuilder builder
        )
        {
            SourceBuilder = sourceBuilder ?? throw new ArgumentNullException(nameof(sourceBuilder));
            Builder = builder ?? throw new ArgumentNullException(nameof(builder));
        }


        public ITagDependencySourceBuilder Tag(Func<IDependencyProvider, object> tag)
        {
            Builder.Tag(tag);
            return this;
        }

        ITagDependencyBuilder ITagDependencyBuilder.Tag(Func<IDependencyProvider, object> tag) => Tag(tag);


        public IDependencyFactory BuildDependency(IDependencyProvider provider)
        {
            return Builder.BuildDependency(provider);
        }



        public IDependencySourceBuilder AddDependency(Func<IDependencyProvider, IDependencyFactory> factory)
        {
            SourceBuilder.AddDependency(factory);
            return this;
        }

        public IDependencySourceBuilder AddSource(Func<IDependencyProvider, IDependencySource> source)
        {
            SourceBuilder.AddSource(source);
            return this;
        }


        public IDependencySource BuildSource(IDependencyProvider provider)
        {
            return SourceBuilder.BuildSource(provider);
        }


    }
}
