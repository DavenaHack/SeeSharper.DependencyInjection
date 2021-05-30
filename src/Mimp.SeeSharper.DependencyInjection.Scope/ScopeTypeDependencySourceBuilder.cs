using Mimp.SeeSharper.DependencyInjection.Abstraction;
using Mimp.SeeSharper.DependencyInjection.Scope.Abstraction;
using Mimp.SeeSharper.DependencyInjection.Tag.Abstraction;
using System;

namespace Mimp.SeeSharper.DependencyInjection.Scope
{
    public class ScopeTypeDependencySourceBuilder : ITagScopeTypeDependencySourceBuilder
    {


        public IDependencySourceBuilder SourceBuilder { get; }

        public ScopeTypeDependencyBuilder Builder { get; }


        public Type Type => Builder.Type;


        public ScopeTypeDependencySourceBuilder(
            IDependencySourceBuilder sourceBuilder,
            ScopeTypeDependencyBuilder builder
        )
        {
            SourceBuilder = sourceBuilder ?? throw new ArgumentNullException(nameof(sourceBuilder));
            Builder = builder ?? throw new ArgumentNullException(nameof(builder));
        }


        public ITagScopeTypeDependencySourceBuilder AddScope(object? scope)
        {
            Builder.AddScope(scope);
            return this;
        }

        ITagScopeTypeDependencyBuilder ITagScopeTypeDependencyBuilder.AddScope(object? scope) => AddScope(scope);

        ITagScopeDependencySourceBuilder ITagScopeDependencySourceBuilder.AddScope(object? scope) => AddScope(scope);

        ITagScopeDependencyBuilder ITagScopeDependencyBuilder.AddScope(object? scope) => AddScope(scope);

        IScopeTypeDependencySourceBuilder IScopeTypeDependencySourceBuilder.AddScope(object? scope) => AddScope(scope);

        IScopeTypeDependencyBuilder IScopeTypeDependencyBuilder.AddScope(object? scope) => AddScope(scope);

        IScopeDependencySourceBuilder IScopeDependencySourceBuilder.AddScope(object? scope) => AddScope(scope);

        IScopeDependencyBuilder IScopeDependencyBuilder.AddScope(object? scope) => AddScope(scope);


        public ITagScopeTypeDependencySourceBuilder Tag(object tag)
        {
            Builder.Tag(tag);
            return this;
        }

        ITagScopeTypeDependencyBuilder ITagScopeTypeDependencyBuilder.Tag(object tag) => Tag(tag);

        ITagScopeDependencySourceBuilder ITagScopeDependencySourceBuilder.Tag(object tag) => Tag(tag);

        ITagScopeDependencyBuilder ITagScopeDependencyBuilder.Tag(object tag) => Tag(tag);

        ITagDependencySourceBuilder ITagDependencySourceBuilder.Tag(object tag) => Tag(tag);

        ITagDependencyBuilder ITagDependencyBuilder.Tag(object tag) => Tag(tag);


        public ITagScopeTypeDependencySourceBuilder As(Type type)
        {
            Builder.As(type);
            return this;
        }

        ITagScopeTypeDependencyBuilder ITagScopeTypeDependencyBuilder.As(Type type) => As(type);

        ITagTypeDependencyBuilder ITagTypeDependencyBuilder.As(Type type) => As(type);

        IScopeTypeDependencySourceBuilder IScopeTypeDependencySourceBuilder.As(Type type) => As(type);

        IScopeTypeDependencyBuilder IScopeTypeDependencyBuilder.As(Type type) => As(type);

        ITypeDependencySourceBuilder ITypeDependencySourceBuilder.As(Type type) => As(type);

        ITypeDependencyBuilder ITypeDependencyBuilder.As(Type type) => As(type);


        public ITagScopeTypeDependencySourceBuilder As(object tag, Type type)
        {
            Builder.As(tag, type);
            return this;
        }

        ITagScopeTypeDependencyBuilder ITagScopeTypeDependencyBuilder.As(object tag, Type type) => As(tag, type);

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
