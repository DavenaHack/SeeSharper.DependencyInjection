using Mimp.SeeSharper.DependencyInjection.Abstraction;
using Mimp.SeeSharper.DependencyInjection.Tag.Abstraction;
using System;

namespace Mimp.SeeSharper.DependencyInjection.Transient
{
    public class TransientDependencySourceBuilder : ITagDependencySourceBuilder
    {


        public IDependencySourceBuilder SourceBuilder { get; }

        public TransientDependencyBuilder Builder { get; }


        public TransientDependencySourceBuilder(
            IDependencySourceBuilder sourceBuilder,
            TransientDependencyBuilder builder
        )
        {
            SourceBuilder = sourceBuilder ?? throw new ArgumentNullException(nameof(sourceBuilder));
            Builder = builder ?? throw new ArgumentNullException(nameof(builder));
        }


        public ITagDependencySourceBuilder Tag(object tag)
        {
            Builder.Tag(tag);
            return this;
        }

        ITagDependencyBuilder ITagDependencyBuilder.Tag(object tag) => Tag(tag);


        public IDependencyFactory BuildDependency()
        {
            return Builder.BuildDependency();
        }



        public IDependencySourceBuilder AddDependency(Func<IDependencyFactory> factory)
        {
            SourceBuilder.AddDependency(factory);
            return this;
        }

        public IDependencySourceBuilder AddSource(Func<IDependencySource> source)
        {
            SourceBuilder.AddSource(source);
            return this;
        }


        public IDependencySource BuildSource()
        {
            return SourceBuilder.BuildSource();
        }


    }
}
