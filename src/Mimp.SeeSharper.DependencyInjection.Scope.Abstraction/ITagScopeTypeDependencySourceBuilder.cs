using Mimp.SeeSharper.DependencyInjection.Abstraction;
using Mimp.SeeSharper.DependencyInjection.Tag.Abstraction;
using System;

namespace Mimp.SeeSharper.DependencyInjection.Scope.Abstraction
{
    public interface ITagScopeTypeDependencySourceBuilder : ITagScopeTypeDependencyBuilder, ITagScopeDependencySourceBuilder, IScopeTypeDependencySourceBuilder, ITagTypeTagDependencySourceBuilder
    {


        public new ITagScopeTypeDependencySourceBuilder AddScope(Func<IDependencyProvider, IScope> scope);

        public new ITagScopeTypeDependencySourceBuilder Tag(Func<IDependencyProvider, object> tag);

        public new ITagScopeTypeDependencySourceBuilder As(Type type);

        public new ITagScopeTypeDependencySourceBuilder As(Func<IDependencyProvider, object> tag, Type type);


    }
}
