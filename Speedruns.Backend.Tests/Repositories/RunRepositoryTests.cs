using Speedruns.Backend.Entities;
using Speedruns.Backend.Tests._Fixtures.Repositories;

namespace Speedruns.Backend.Tests.Repositories
{
    public class RunRepositoryTests : IClassFixture<RunRepositoryFixture>
    {
        private readonly RunRepositoryFixture _fixture;

        public RunRepositoryTests(RunRepositoryFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task ShouldReturnListOfRuns()
        {
            _fixture.ResetRepository();
            var runs = await _fixture.RunRepository.GetAll();

            Assert.NotNull(runs);
            Assert.IsType<List<RunEntity>>(runs);
            Assert.Equal(2, runs.Count);
        }

        [Fact]
        public async Task ShouldReturnRunById()
        {
            _fixture.ResetRepository();
            var run = await _fixture.RunRepository.GetById(1);

            Assert.NotNull(run);
            Assert.IsType<RunEntity>(run);
            Assert.Equal(1, run.Id);
        }

        [Fact]
        public async Task ShouldReturnNullRunById()
        {
            _fixture.ResetRepository();
            var run = await _fixture.RunRepository.GetById(100);

            Assert.Null(run);
        }

        [Fact]
        public async Task ShouldReturnRunsByUser()
        {
            _fixture.ResetRepository();
            var runs = await _fixture.RunRepository.GetUserRuns(1);

            Assert.NotNull(runs);
            Assert.IsType<List<RunEntity>>(runs);
            Assert.Equal(2, runs.Count);
        }

        [Fact]
        public async Task ShouldReturnEmptyListRunsByUser()
        {
            _fixture.ResetRepository();
            var runs = await _fixture.RunRepository.GetUserRuns(100);

            Assert.Empty(runs);
        }

        [Fact]
        public async Task ShouldCreateRun()
        {
            _fixture.ResetRepository();
            var newRun = new RunEntity { Id = 3, GameId = 1, UserId = 1 };

            var game = await _fixture.GameRepository.GetById(newRun.GameId);

            await _fixture.RunRepository.CreateRun(newRun, game);

            var run = await _fixture.RunRepository.GetById(newRun.Id);

            Assert.NotNull(run);
            Assert.IsType<RunEntity>(run);
            Assert.Equal(3, run.Id);
        }

        [Fact]
        public async Task ShouldUpdateRun()
        {
            _fixture.ResetRepository();
            var newRun = new RunEntity { Id = 1, UserId = 1, GameId = 1, Console = new ConsoleEntity { Name = "PlayStation 2" }, Date = DateTime.UtcNow };

            var runToUpdate = await _fixture.RunRepository.GetById(newRun.Id);

            var oldDate = runToUpdate.Date;

            await _fixture.RunRepository.UpdateRun(runToUpdate, newRun);

            var run = await _fixture.RunRepository.GetById(1);

            Assert.NotNull(run);
            Assert.IsType<RunEntity>(run);
            Assert.Equal(1, run.Id);
            Assert.Equal("PlayStation 2", run.Console.Name);
            Assert.NotEqual(oldDate, runToUpdate.Date);
        }

        [Fact]
        public async Task ShouldDeleteRun()
        {
            _fixture.ResetRepository();
            var runToDelete = await _fixture.RunRepository.GetById(1);

            await _fixture.RunRepository.DeleteRun(runToDelete);

            var run = await _fixture.RunRepository.GetById(1);

            Assert.Null(run);
        }
    }
}
