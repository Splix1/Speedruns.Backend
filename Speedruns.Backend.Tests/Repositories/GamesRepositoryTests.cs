using Speedruns.Backend.Entities;
using Speedruns.Backend.Repositories;
using Speedruns.Backend.Tests.Database;

namespace Speedruns.Backend.Tests.Repositories
{
    public class GamesRepositoryTests
    {
        private DbContextMock<GameEntity> _dbContextMock;
        private GamesRepository _gamesRepository;

        public GamesRepositoryTests()
        {
            _dbContextMock = new DbContextMock<GameEntity>(new List<GameEntity>
            {
                new GameEntity { Id = 1, Name = "Super Mario 64", ReleaseYear = 1996, Players = 100, RunsPublished = 1000 },
                new GameEntity { Id = 2, Name = "Kingdom Hearts 2 Final Mix", ReleaseYear = 2007, Players = 50, RunsPublished = 1000 },
            });

            _gamesRepository = new GamesRepository(_dbContextMock.Context);
        }

        [Fact]
        public async Task ShouldReturnListOfGames()
        {
            var games = await _gamesRepository.GetAll();

            Assert.NotNull(games);
            Assert.IsType<List<GameEntity>>(games);
            Assert.Equal(2, games.Count);
        }

        [Fact]
        public async Task ShouldReturnGameById()
        {
            var game = await _gamesRepository.GetById(1);

            Assert.NotNull(game);
            Assert.IsType<GameEntity>(game);
            Assert.Equal(1, game.Id);
            Assert.Equal("Super Mario 64", game.Name);
        }

        [Fact]
        public async Task ShouldReturnGameByName()
        {
            var game = await _gamesRepository.GetByName("Kingdom Hearts 2 Final Mix");

            Assert.NotNull(game);
            Assert.IsType<GameEntity>(game);
            Assert.Equal("Kingdom Hearts 2 Final Mix", game.Name);
        }
    }
}
