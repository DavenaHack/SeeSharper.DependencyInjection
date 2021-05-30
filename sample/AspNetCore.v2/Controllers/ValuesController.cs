using Microsoft.AspNetCore.Mvc;
using ServiceLibrary;

namespace AspNetCore.v2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ValuesController : ControllerBase
    {


        private readonly IFooService _foo;

        private readonly IBarService _bar;


        public ValuesController(IFooService foo, IBarService bar)
        {
            _foo = foo;
            _bar = bar;
        }


        [HttpGet]
        public string Get()
        {
            return $"{_foo.Foo()}.{_bar.Bar()}";
        }


    }
}
