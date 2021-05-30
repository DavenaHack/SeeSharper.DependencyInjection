using System;

namespace Mimp.SeeSharper.DependencyInjection.Abstraction
{
    public static class DependencyExtensions
    {


        public static void Use(this IDependency dependency, Action<object> use)
        {
            if (dependency is null)
                throw new ArgumentNullException(nameof(dependency));
            if (use is null)
                throw new ArgumentNullException(nameof(use));

            try
            {
                use(dependency.Dependency);
            }
            finally
            {
                if (dependency is IDisposable d)
                    d.Dispose();
            }
        }

        public static R Use<R>(this IDependency dependency, Func<object, R> use)
        {
            if (dependency is null)
                throw new ArgumentNullException(nameof(dependency));
            if (use is null)
                throw new ArgumentNullException(nameof(use));

            try
            {
                return use(dependency.Dependency);
            }
            finally
            {
                if (dependency is IDisposable d)
                    d.Dispose();
            }
        }


    }
}
