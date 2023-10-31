using NSubstitute;
using Speedruns.Backend.Interfaces;

namespace Speedruns.Backend.Tests._Fixtures.Controllers
{
    public class UserControllerFixture
    {
        private IUserRepository _userRepository;

        public UserControllerFixture()
        {
            _userRepository = Substitute.For<IUserRepository>();
        }

        internal IUserRepository Repository => _userRepository;

        public void ResetSubstitutes()
        {
            _userRepository = Substitute.For<IUserRepository>();
        }
    }
}
