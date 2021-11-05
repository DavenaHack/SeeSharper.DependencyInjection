using Mimp.SeeSharper.DependencyInjection.Abstraction;
using System;

namespace Mimp.SeeSharper.DependencyInjection.Tag.Abstraction
{
    public class TagTypeTagDependencySourceBuilder : ITagTypeTagDependencySourceBuilder
    {


        public IDependencySourceBuilder SourceBuilder { get; }

        public ITagTypeTagDependencyBuilder Builder { get; }


        public Type Type => Builder.Type;


        public TagTypeTagDependencySourceBuilder(
            IDependencySourceBuilder sourceBuilder,
            ITagTypeTagDependencyBuilder builder
        )
        {
            SourceBuilder = sourceBuilder ?? throw new ArgumentNullException(nameof(sourceBuilder));
            Builder = builder ?? throw new ArgumentNullException(nameof(builder));
        }


        public ITagTypeTagDependencySourceBuilder Tag(Func<IDependencyProvider, object> tag)
        {
            Builder.Tag(tag);
            return this;
        }

        ITagDependencySourceBuilder ITagDependencySourceBuilder.Tag(Func<IDependencyProvider, object> tag) => Tag(tag);

        ITagDependencyBuilder ITagDependencyBuilder.Tag(Func<IDependencyProvider, object> tag) => Tag(tag);

        ITagTypeTagDependencyBuilder ITagTypeTagDependencyBuilder.Tag(Func<IDependencyProvider, object> tag) => Tag(tag);


        public ITagTypeTagDependencySourceBuilder As(Type type)
        {
            Builder.As(type);
            return this;
        }

        ITypeDependencySourceBuilder ITypeDependencySourceBuilder.As(Type type) => As(type);

        ITypeDependencyBuilder ITypeDependencyBuilder.As(Type type) => As(type);

        ITagTypeDependencyBuilder ITagTypeDependencyBuilder.As(Type type) => As(type);

        ITagTypeTagDependencyBuilder ITagTypeTagDependencyBuilder.As(Type type) => As(type);


        public ITagTypeTagDependencySourceBuilder As(Func<IDependencyProvider, object> tag, Type type)
        {
            Builder.As(tag, type);
            return this;
        }

        ITagTypeDependencyBuilder ITagTypeDependencyBuilder.As(Func<IDependencyProvider, object> tag, Type type) => As(tag, type);

        ITagTypeTagDependencyBuilder ITagTypeTagDependencyBuilder.As(Func<IDependencyProvider, object> tag, Type type) => As(tag, type);


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
