using Mimp.SeeSharper.DependencyInjection.Tag.Abstraction;

namespace Mimp.SeeSharper.DependencyInjection.Scope.Abstraction
{
    public interface ITagScopeDependencySourceBuilder : ITagScopeDependencyBuilder, IScopeDependencySourceBuilder, ITagDependencySourceBuilder
    {


        public new ITagScopeDependencySourceBuilder AddScope(object? scope);

        public new ITagScopeDependencySourceBuilder Tag(object tag);


    }
}
