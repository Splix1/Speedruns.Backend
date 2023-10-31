using NSubstitute;
using Speedruns.Backend.Entities;
using Speedruns.Backend.Interfaces;
using Speedruns.Backend.Repositories;
using Speedruns.Backend.Tests.Database;

namespace Speedruns.Backend.Tests._Fixtures.Repositories
{
    public class RunRepositoryFixture
    {
        private DbContextMock<RunEntity> _dbContextMock;
        private RunRepository _runRepository;
        private IGamesRepository _gamesRepository;

        public RunRepositoryFixture()
        {
            _dbContextMock = new DbContextMock<RunEntity>(new List<RunEntity>
            {
                new RunEntity { Id = 1, UserId = 1, GameId = 1, Time = 123, Console = new ConsoleEntity { Name = "PlayStation 1" } },
                new RunEntity { Id = 2, UserId = 1, GameId = 2, Time = 123 },
            });

            _gamesRepository = Substitute.For<IGamesRepository>();
            _gamesRepository
                .GetById(1)
                .Returns(new GameEntity
                {
                    Id = 1,
                    Name = "Super Mario 64",
                    ReleaseYear = 1996,
                    Players = 100,
                    RunsPublished = 1000
                });

            _runRepository = new RunRepository(_dbContextMock.Context, _gamesRepository);
        }

        internal RunRepository RunRepository => _runRepository;
        internal IGamesRepository GameRepository => _gamesRepository;

        public void ResetRepository()
        {
            _dbContextMock = new DbContextMock<RunEntity>(new List<RunEntity>
            {
                new RunEntity { Id = 1, UserId = 1, GameId = 1, Time = 123, Console = new ConsoleEntity { Name = "PlayStation 1" } },
                new RunEntity { Id = 2, UserId = 1, GameId = 2, Time = 123 },
            });

            _gamesRepository = Substitute.For<IGamesRepository>();
            _gamesRepository
                .GetById(1)
                .Returns(new GameEntity
                {
                    Id = 1,
                    Name = "Super Mario 64",
                    ReleaseYear = 1996,
                    Players = 100,
                    RunsPublished = 1000
                });

            _runRepository = new RunRepository(_dbContextMock.Context, _gamesRepository);
        }
    }
}
