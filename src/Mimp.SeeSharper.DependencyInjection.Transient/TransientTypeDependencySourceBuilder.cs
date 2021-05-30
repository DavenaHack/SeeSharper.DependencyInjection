using Mimp.SeeSharper.DependencyInjection.Abstraction;
using Mimp.SeeSharper.DependencyInjection.Tag.Abstraction;
using System;

namespace Mimp.SeeSharper.DependencyInjection.Transient
{
    public class TransientTypeDependencySourceBuilder : ITagTypeDependencySourceBuilder
    {


        public IDependencySourceBuilder SourceBuilder { get; }

        public TransientTypeDependencyBuilder Builder { get; }


        public Type Type => Builder.Type;


        public TransientTypeDependencySourceBuilder(
            IDependencySourceBuilder sourceBuilder,
            TransientTypeDependencyBuilder builder
        )
        {
            SourceBuilder = sourceBuilder ?? throw new ArgumentNullException(nameof(sourceBuilder));
            Builder = builder ?? throw new ArgumentNullException(nameof(builder));
        }


        public ITagTypeDependencySourceBuilder Tag(object tag)
        {
            Builder.Tag(tag);
            return this;
        }

        ITagTypeTagDependencyBuilder ITagTypeTagDependencyBuilder.Tag(object tag) => Tag(tag);

        ITagDependencySourceBuilder ITagDependencySourceBuilder.Tag(object tag) => Tag(tag);

        ITagDependencyBuilder ITagDependencyBuilder.Tag(object tag) => Tag(tag);


        public ITagTypeDependencySourceBuilder As(Type type)
        {
            Builder.As(type);
            return this;
        }

        ITagTypeTagDependencyBuilder ITagTypeTagDependencyBuilder.As(Type type) => As(type);

        ITagTypeDependencyBuilder ITagTypeDependencyBuilder.As(Type type) => As(type);

        ITypeDependencySourceBuilder ITypeDependencySourceBuilder.As(Type type) => As(type);

        ITypeDependencyBuilder ITypeDependencyBuilder.As(Type type) => As(type);


        public ITagTypeDependencySourceBuilder As(object tag, Type type)
        {
            Builder.As(tag, type);
            return this;
        }


        ITagTypeTagDependencyBuilder ITagTypeTagDependencyBuilder.As(object tag, Type type) => As(tag, type);

        ITagTypeDependencyBuilder ITagTypeDependencyBuilder.As(object tag, Type type) => As(tag, type);


        public IDependencyFactory BuildDependency()
        {
            return Builder.BuildDependency();
        }



        public IDependencySourceBuilder AddSource(Func<IDependencySource> source)
        {
            SourceBuilder.AddSource(source);
            return this;
        }


        public IDependencySourceBuilder AddDependency(Func<IDependencyFactory> factory)
        {
            SourceBuilder.AddDependency(factory);
            return this;
        }


        public IDependencySource BuildSource()
        {
            return SourceBuilder.BuildSource();
        }


    }
}
