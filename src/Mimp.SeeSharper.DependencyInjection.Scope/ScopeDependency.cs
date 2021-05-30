using System;

namespace Mimp.SeeSharper.DependencyInjection.Scope
{
    public class ScopeDependency : BaseDependency
    {


        public object Scope { get; }


        public ScopeDependency(object scope, object dependency)
            : base(dependency)
        {
            Scope = scope ?? throw new ArgumentNullException(nameof(scope));
        }


    }
}
