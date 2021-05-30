using Mimp.SeeSharper.DependencyInjection.Abstraction;

namespace Mimp.SeeSharper.DependencyInjection.Tag.Abstraction
{
    public interface ITagDependencySourceBuilder : ITagDependencyBuilder, IDependencySourceBuilder, IDependencyBuilder
    {


        public new ITagDependencySourceBuilder Tag(object tag);


    }
}
