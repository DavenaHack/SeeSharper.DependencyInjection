using System;

namespace Mimp.SeeSharper.DependencyInjection.Test.Mock
{
    public class DisposeObject : IDisposable
    {


        public bool Disposed { get; private set; }


        public void Dispose()
        {
            Disposed = true;
        }


    }
}
