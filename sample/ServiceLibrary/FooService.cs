namespace ServiceLibrary
{
    public class FooService : IFooService
    {


        private readonly string _foo;


        public FooService(string foo)
        {
            _foo = foo;
        }

        public string Foo()
        {
            return _foo;
        }


    }
}
