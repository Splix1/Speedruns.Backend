using Speedruns.Backend.Entities;
using Speedruns.Backend.Interfaces;
using Speedruns.Backend.Repositories;
using Speedruns.Backend.Tests.Database;

namespace Speedruns.Backend.Tests.Repositories
{
    public class RunRepositoryTests
    {
        private DbContextMock<RunEntity> _dbContextMock;
        private RunRepository _runRepository;

        public RunRepositoryTests()
        {
            _dbContextMock = new DbContextMock<RunEntity>(new List<RunEntity>
            {
                new RunEntity { Id = 1, UserId = 1, GameId = 1, Time = 123 },
                new RunEntity { Id = 2, UserId = 1, GameId = 2, Time = 123 },
            });

            _runRepository = new RunRepository(_dbContextMock.Context);
        }

        [Fact]
        public async Task ShouldReturnListOfRuns()
        {
            var runs = await _runRepository.GetAll();

            Assert.NotNull(runs);
            Assert.IsType<List<RunEntity>>(runs);
            Assert.Equal(2, runs.Count);
        }

        [Fact]
        public async Task ShouldReturnRunById()
        {
            var run = await _runRepository.GetById(1);

            Assert.NotNull(run);
            Assert.IsType<RunEntity>(run);
            Assert.Equal(1, run.Id);
        }

        [Fact]
        public async Task ShouldReturnNullRunById()
        {
            var run = await _runRepository.GetById(100);

            Assert.Null(run);
        }

        [Fact]
        public async Task ShouldReturnRunsByUser()
        {
            var runs = await _runRepository.GetUserRuns(1);

            Assert.NotNull(runs);
            Assert.IsType<List<RunEntity>>(runs);
            Assert.Equal(2, runs.Count);
        }
    }
}
