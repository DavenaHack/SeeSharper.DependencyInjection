using System;

namespace Mimp.SeeSharper.DependencyInjection.Abstraction
{
    [Serializable]
    public class DependencyInjectionException : Exception
    {


        public DependencyInjectionException() { }

        public DependencyInjectionException(string? message)
            : base(message) { }

        public DependencyInjectionException(string? message, Exception? inner)
            : base(message, inner) { }

        protected DependencyInjectionException(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context
        ) : base(info, context) { }


    }
}
