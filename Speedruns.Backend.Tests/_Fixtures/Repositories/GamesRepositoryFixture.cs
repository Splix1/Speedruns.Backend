using Speedruns.Backend.Entities;
using Speedruns.Backend.Repositories;
using Speedruns.Backend.Tests.Database;

namespace Speedruns.Backend.Tests._Fixtures.Repositories
{
    public class GamesRepositoryFixture
    {
        private DbContextMock<GameEntity> _dbContextMock;
        private GamesRepository _gamesRepository;

        public GamesRepositoryFixture()
        {
            _dbContextMock = new DbContextMock<GameEntity>(new List<GameEntity>
            {
                new GameEntity { Id = 1, Name = "Super Mario 64", ReleaseYear = 1996, Players = 100, RunsPublished = 1000 },
                new GameEntity { Id = 2, Name = "Kingdom Hearts 2 Final Mix", ReleaseYear = 2007, Players = 50, RunsPublished = 1000 },
            });

            _gamesRepository = new GamesRepository(_dbContextMock.Context);
        }

        internal GamesRepository Repository => _gamesRepository;
    }
}
