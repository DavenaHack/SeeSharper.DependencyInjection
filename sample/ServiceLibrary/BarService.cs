namespace ServiceLibrary
{
    public class BarService : IBarService
    {


        private readonly string _bar;


        public BarService(string bar)
        {
            _bar = bar;
        }

        public string Bar()
        {
            return _bar;
        }


    }
}
