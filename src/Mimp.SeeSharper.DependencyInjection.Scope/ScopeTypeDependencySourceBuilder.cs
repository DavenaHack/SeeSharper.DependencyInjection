using Mimp.SeeSharper.DependencyInjection.Abstraction;
using Mimp.SeeSharper.DependencyInjection.Scope.Abstraction;
using Mimp.SeeSharper.DependencyInjection.Tag.Abstraction;
using System;

namespace Mimp.SeeSharper.DependencyInjection.Scope
{
    public class TagScopeTypeDependencySourceBuilder : TagTypeTagDependencySourceBuilder, ITagScopeTypeDependencySourceBuilder
    {


        public new ITagScopeTypeDependencyBuilder Builder => (ITagScopeTypeDependencyBuilder)base.Builder;


        public TagScopeTypeDependencySourceBuilder(IDependencySourceBuilder sourceBuilder, ITagScopeTypeDependencyBuilder builder)
            : base(sourceBuilder, builder) { }


        public ITagScopeTypeDependencySourceBuilder AddScope(Func<IDependencyProvider, IScope> scope)
        {
            Builder.AddScope(scope);
            return this;
        }

        ITagScopeTypeDependencySourceBuilder ITagScopeTypeDependencySourceBuilder.AddScope(Func<IDependencyProvider, IScope> scope) => AddScope(scope);

        ITagScopeTypeDependencyBuilder ITagScopeTypeDependencyBuilder.AddScope(Func<IDependencyProvider, IScope> scope) => AddScope(scope);

        ITagScopeDependencyBuilder ITagScopeDependencyBuilder.AddScope(Func<IDependencyProvider, IScope> scope) => AddScope(scope);

        IScopeTypeDependencySourceBuilder IScopeTypeDependencySourceBuilder.AddScope(Func<IDependencyProvider, IScope> scope) => AddScope(scope);

        IScopeTypeDependencyBuilder IScopeTypeDependencyBuilder.AddScope(Func<IDependencyProvider, IScope> scope) => AddScope(scope);

        IScopeDependencySourceBuilder IScopeDependencySourceBuilder.AddScope(Func<IDependencyProvider, IScope> scope) => AddScope(scope);

        IScopeDependencyBuilder IScopeDependencyBuilder.AddScope(Func<IDependencyProvider, IScope> scope) => AddScope(scope);

        ITagScopeDependencySourceBuilder ITagScopeDependencySourceBuilder.AddScope(Func<IDependencyProvider, IScope> scope) => AddScope(scope);


        public new ITagScopeTypeDependencySourceBuilder Tag(Func<IDependencyProvider, object> tag)
        {
            return (ITagScopeTypeDependencySourceBuilder)base.Tag(tag);
        }

        ITagScopeTypeDependencySourceBuilder ITagScopeTypeDependencySourceBuilder.Tag(Func<IDependencyProvider, object> tag) => Tag(tag);

        ITagScopeTypeDependencyBuilder ITagScopeTypeDependencyBuilder.Tag(Func<IDependencyProvider, object> tag) => Tag(tag);

        ITagScopeDependencySourceBuilder ITagScopeDependencySourceBuilder.Tag(Func<IDependencyProvider, object> tag) => Tag(tag);

        ITagScopeDependencyBuilder ITagScopeDependencyBuilder.Tag(Func<IDependencyProvider, object> tag) => Tag(tag);


        public new ITagScopeTypeDependencySourceBuilder As(Type type)
        {
            return (ITagScopeTypeDependencySourceBuilder)base.As(type);
        }

        ITagScopeTypeDependencySourceBuilder ITagScopeTypeDependencySourceBuilder.As(Type type) => As(type);

        ITagScopeTypeDependencyBuilder ITagScopeTypeDependencyBuilder.As(Type type) => As(type);

        IScopeTypeDependencySourceBuilder IScopeTypeDependencySourceBuilder.As(Type type) => As(type);

        IScopeTypeDependencyBuilder IScopeTypeDependencyBuilder.As(Type type) => As(type);


        public new ITagScopeTypeDependencySourceBuilder As(Func<IDependencyProvider, object> tag, Type type)
        {
            return (ITagScopeTypeDependencySourceBuilder)base.As(tag, type);
        }

        ITagScopeTypeDependencySourceBuilder ITagScopeTypeDependencySourceBuilder.As(Func<IDependencyProvider, object> tag, Type type) => As(tag, type);

        ITagScopeTypeDependencyBuilder ITagScopeTypeDependencyBuilder.As(Func<IDependencyProvider, object> tag, Type type) => As(tag, type);


    }
}
