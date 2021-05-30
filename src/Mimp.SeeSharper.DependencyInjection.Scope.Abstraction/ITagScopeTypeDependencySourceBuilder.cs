using System;

namespace Mimp.SeeSharper.DependencyInjection.Scope.Abstraction
{
    public interface ITagScopeTypeDependencySourceBuilder : ITagScopeTypeDependencyBuilder, ITagScopeDependencySourceBuilder, IScopeTypeDependencySourceBuilder
    {


        public new ITagScopeTypeDependencySourceBuilder AddScope(object? scope);

        public new ITagScopeTypeDependencySourceBuilder Tag(object tag);

        public new ITagScopeTypeDependencySourceBuilder As(Type type);

        public new ITagScopeTypeDependencySourceBuilder As(object tag, Type type);


    }
}
