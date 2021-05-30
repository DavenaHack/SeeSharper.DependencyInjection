using System;

namespace Mimp.SeeSharper.DependencyInjection.Test.Mock
{
    public class ObjectWithDependency
    {


        public const int DefaultInt = 3;


        public DisposeObject DisposeObject { get; set; }

        public string StringProperty { get; set; }

        public int IntProperty { get; set; }


        public ObjectWithDependency(DisposeObject disposeObject, string stringProperty, int intProperty = DefaultInt)
        {
            DisposeObject = disposeObject ?? throw new ArgumentNullException(nameof(disposeObject));
            StringProperty = stringProperty ?? throw new ArgumentNullException(nameof(stringProperty));
            IntProperty = intProperty;
        }


    }
}
