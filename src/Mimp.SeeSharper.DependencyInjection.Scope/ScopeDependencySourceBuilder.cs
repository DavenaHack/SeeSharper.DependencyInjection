using Mimp.SeeSharper.DependencyInjection.Abstraction;
using Mimp.SeeSharper.DependencyInjection.Scope.Abstraction;
using Mimp.SeeSharper.DependencyInjection.Tag.Abstraction;
using System;

namespace Mimp.SeeSharper.DependencyInjection.Scope
{
    public class ScopeDependencySourceBuilder : ITagScopeDependencySourceBuilder
    {


        public IDependencySourceBuilder SourceBuilder { get; }

        public ScopeDependencyBuilder Builder { get; }


        public ScopeDependencySourceBuilder(
            IDependencySourceBuilder sourceBuilder,
            ScopeDependencyBuilder builder
        )
        {
            SourceBuilder = sourceBuilder ?? throw new ArgumentNullException(nameof(sourceBuilder));
            Builder = builder ?? throw new ArgumentNullException(nameof(builder));
        }


        public ITagScopeDependencySourceBuilder AddScope(object? scope)
        {
            Builder.AddScope(scope);
            return this;
        }

        ITagScopeDependencyBuilder ITagScopeDependencyBuilder.AddScope(object? scope) => AddScope(scope);

        IScopeDependencySourceBuilder IScopeDependencySourceBuilder.AddScope(object? scope) => AddScope(scope);

        IScopeDependencyBuilder IScopeDependencyBuilder.AddScope(object? scope) => AddScope(scope);


        public ITagScopeDependencySourceBuilder Tag(object tag)
        {
            Builder.Tag(tag);
            return this;
        }

        ITagScopeDependencyBuilder ITagScopeDependencyBuilder.Tag(object tag) => Tag(tag);

        ITagDependencySourceBuilder ITagDependencySourceBuilder.Tag(object tag) => Tag(tag);

        ITagDependencyBuilder ITagDependencyBuilder.Tag(object tag) => Tag(tag);


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
