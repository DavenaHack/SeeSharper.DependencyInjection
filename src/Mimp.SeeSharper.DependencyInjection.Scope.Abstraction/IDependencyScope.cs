using System;

namespace Mimp.SeeSharper.DependencyInjection.Scope.Abstraction
{
    public interface IDependencyScope : IDisposable
    {


        public event EventHandler? OnDisposing;

        public event EventHandler? OnDisposed;


        public object Scope { get; }

        public IScopeDependencyProvider Provider { get; }


    }
}
