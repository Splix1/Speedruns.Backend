using Speedruns.Backend.Entities;
using Speedruns.Backend.Tests._Fixtures.Repositories;

namespace Speedruns.Backend.Tests.Repositories
{
    public class GamesRepositoryTests : IClassFixture<GamesRepositoryFixture>
    {
       private readonly GamesRepositoryFixture _fixture;

        public GamesRepositoryTests(GamesRepositoryFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task ShouldReturnListOfGames()
        {
            var games = await _fixture.Repository.GetAll();

            Assert.NotNull(games);
            Assert.IsType<List<GameEntity>>(games);
            Assert.Equal(2, games.Count);
        }

        [Fact]
        public async Task ShouldReturnGameById()
        {
            var game = await _fixture.Repository.GetById(1);

            Assert.NotNull(game);
            Assert.IsType<GameEntity>(game);
            Assert.Equal(1, game.Id);
            Assert.Equal("Super Mario 64", game.Name);
        }

        [Fact]
        public async Task ShouldReturnGameByName()
        {
            var game = await _fixture.Repository.GetByName("Kingdom Hearts 2 Final Mix");

            Assert.NotNull(game);
            Assert.IsType<GameEntity>(game);
            Assert.Equal("Kingdom Hearts 2 Final Mix", game.Name);
        }
    }
}
