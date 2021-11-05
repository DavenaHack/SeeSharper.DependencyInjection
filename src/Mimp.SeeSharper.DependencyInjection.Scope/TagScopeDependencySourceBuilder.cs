using Mimp.SeeSharper.DependencyInjection.Abstraction;
using Mimp.SeeSharper.DependencyInjection.Scope.Abstraction;
using Mimp.SeeSharper.DependencyInjection.Tag.Abstraction;
using System;

namespace Mimp.SeeSharper.DependencyInjection.Scope
{
    public class TagScopeDependencySourceBuilder : TagDependencySourceBuilder, ITagScopeDependencySourceBuilder
    {


        public new ITagScopeDependencyBuilder Builder => (ITagScopeDependencyBuilder)base.Builder;


        public TagScopeDependencySourceBuilder(IDependencySourceBuilder sourceBuilder, ITagScopeDependencyBuilder builder)
            : base(sourceBuilder, builder) { }


        public ITagScopeDependencySourceBuilder AddScope(Func<IDependencyProvider, IScope> scope)
        {
            Builder.AddScope(scope);
            return this;
        }

        ITagScopeDependencyBuilder ITagScopeDependencyBuilder.AddScope(Func<IDependencyProvider, IScope> scope) => AddScope(scope);

        IScopeDependencySourceBuilder IScopeDependencySourceBuilder.AddScope(Func<IDependencyProvider, IScope> scope) => AddScope(scope);

        IScopeDependencyBuilder IScopeDependencyBuilder.AddScope(Func<IDependencyProvider, IScope> scope) => AddScope(scope);


        public new ITagScopeDependencySourceBuilder Tag(Func<IDependencyProvider, object> tag)
        {
            return (ITagScopeDependencySourceBuilder)base.Tag(tag);
        }

        ITagScopeDependencySourceBuilder ITagScopeDependencySourceBuilder.Tag(Func<IDependencyProvider, object> tag) => Tag(tag);

        ITagScopeDependencyBuilder ITagScopeDependencyBuilder.Tag(Func<IDependencyProvider, object> tag) => Tag(tag);


    }
}
