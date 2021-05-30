using Mimp.SeeSharper.DependencyInjection.Abstraction;

namespace Mimp.SeeSharper.DependencyInjection.Tag
{
    public interface ITagDependencyContext : IDependencyContext
    {


        public object Tag { get; }


    }
}
