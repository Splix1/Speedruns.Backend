using Speedruns.Backend.Entities;
using Speedruns.Backend.Repositories;
using Speedruns.Backend.Tests.Database;

namespace Speedruns.Backend.Tests.Repositories
{
    public class UserRepositoryTests
    {
        private DbContextMock<UserEntity> _dbContextMock;
        private UserRepository _userRepository;

        public UserRepositoryTests()
        {
            _dbContextMock = new DbContextMock<UserEntity>(new List<UserEntity>
            {
                new UserEntity { Id = 1, UserName = "Test 1", Runs = new List<RunEntity>{ new RunEntity { Id = 1, UserId = 1, ConsoleId = 1, GameId = 1, Time = 123 } } },
                new UserEntity { Id = 2, UserName = "Test 2" },
            });

            _userRepository = new UserRepository(_dbContextMock.Context);
        }

        [Fact]
        public async Task ShouldReturnListOfUsers()
        {
            var users = await _userRepository.GetAll();

            Assert.NotNull(users);
            Assert.True(users.First().Runs.Any());
            Assert.Equal(2, users.Count);
        }

        [Fact]
        public async Task ShouldReturnUserById()
        {
            var user = await _userRepository.GetById(1);

            Assert.NotNull(user);
            Assert.True(user.Runs.Any());
            Assert.Equal(1, user.Id);
        }

        [Fact]
        public async Task ShouldReturnNullUserById()
        {
            var user = await _userRepository.GetById(100);

            Assert.Null(user);
        }

        [Fact]
        public async Task ShouldReturnUserByName()
        {
            var user = await _userRepository.GetByName("Test 1");

            Assert.NotNull(user);
            Assert.Equal("Test 1", user.UserName);
            Assert.True(user.Runs.Any());
        }

        [Fact]
        public async Task ShouldReturnNullUserByName()
        {
            var user = await _userRepository.GetByName("Test 3");

            Assert.Null(user);
        }


    }
}
