using System;

namespace Mimp.SeeSharper.DependencyInjection.Abstraction
{
    [Serializable]
    public class InvalidInvokeException : DependencyInjectionException
    {


        public InvalidInvokeException() { }

        public InvalidInvokeException(string? message)
            : base(message) { }

        public InvalidInvokeException(string? message, Exception? inner)
            : base(message, inner) { }

        protected InvalidInvokeException(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context
        ) : base(info, context) { }


    }
}
