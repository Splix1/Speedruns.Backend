using NSubstitute;
using Speedruns.Backend.Interfaces;

namespace Speedruns.Backend.Tests._Fixtures.Controllers
{
    public class ConsoleControllerFixture
    {
        private IConsolesRepository _consolesRepository;

        public ConsoleControllerFixture()
        {
            _consolesRepository = Substitute.For<IConsolesRepository>();
        }

        internal IConsolesRepository Repository => _consolesRepository;

        public void ResetSubstitutes()
        {
            _consolesRepository = Substitute.For<IConsolesRepository>();
        }
    }
}
