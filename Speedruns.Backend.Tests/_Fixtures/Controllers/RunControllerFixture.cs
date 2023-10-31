using NSubstitute;
using Speedruns.Backend.Interfaces;

namespace Speedruns.Backend.Tests._Fixtures.Controllers
{
    public class RunControllerFixture
    {
        private IRunRepository _runRepository;
        private IGamesRepository _gamesRepository;

        public RunControllerFixture()
        {
            _runRepository = Substitute.For<IRunRepository>();
            _gamesRepository = Substitute.For<IGamesRepository>();
        }

        internal IRunRepository RunRepository => _runRepository;
        internal IGamesRepository GamesRepository => _gamesRepository;

        public void ResetSubstitutes()
        {
            _runRepository = Substitute.For<IRunRepository>();
            _gamesRepository = Substitute.For<IGamesRepository>();
        }
    }
}
