using Speedruns.Backend.Entities;
using Speedruns.Backend.Repositories;
using Speedruns.Backend.Tests.Database;

namespace Speedruns.Backend.Tests._Fixtures.Repositories
{
    public class UserRepositoryFixture
    {
        private DbContextMock<UserEntity> _dbContextMock;
        private UserRepository _userRepository;

        public UserRepositoryFixture()
        {
            _dbContextMock = new DbContextMock<UserEntity>(new List<UserEntity>
            {
                new UserEntity { Id = 1, UserName = "Test 1", Runs = new List<RunEntity>{ new RunEntity { Id = 1, UserId = 1, ConsoleId = 1, GameId = 1, Time = 123 } } },
                new UserEntity { Id = 2, UserName = "Test 2" },
            });

            _userRepository = new UserRepository(_dbContextMock.Context);
        }

        internal UserRepository Repository => _userRepository;

        public void ResetRepository()
        {
            _dbContextMock = new DbContextMock<UserEntity>(new List<UserEntity>
            {
                new UserEntity { Id = 1, UserName = "Test 1", Runs = new List<RunEntity>{ new RunEntity { Id = 1, UserId = 1, ConsoleId = 1, GameId = 1, Time = 123 } } },
                new UserEntity { Id = 2, UserName = "Test 2" },
            });

            _userRepository = new UserRepository(_dbContextMock.Context);
        }
    }
}
