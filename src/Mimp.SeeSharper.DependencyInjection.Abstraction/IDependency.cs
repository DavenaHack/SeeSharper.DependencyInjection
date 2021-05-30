using System;

namespace Mimp.SeeSharper.DependencyInjection.Abstraction
{
    /// <summary>
    /// The <see cref="IDependency"/> manage the lifetime of a dependency.
    /// 
    /// If the dependency is <see cref="IDisposable"/> it should dispose after the dependency has done his job.
    /// </summary>
    public interface IDependency
    {


        public object Dependency { get; }


    }
}
