using NSubstitute;
using Speedruns.Backend.Interfaces;

namespace Speedruns.Backend.Tests._Fixtures.Controllers
{
    public class GameControllerFixture
    {
        private IGamesRepository _gamesRepository;

        public GameControllerFixture()
        {
            _gamesRepository = Substitute.For<IGamesRepository>();
        }

        internal IGamesRepository Repository => _gamesRepository;

        public void ResetSubstitutes()
        {
            _gamesRepository = Substitute.For<IGamesRepository>();
        }
    }
}
