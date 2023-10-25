﻿using NSubstitute;
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
        private IGamesRepository _gamesRepository;

        public RunRepositoryTests()
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

        [Fact]
        public async Task ShouldReturnEmptyListRunsByUser()
        {
            var runs = await _runRepository.GetUserRuns(100);

            Assert.Empty(runs);
        }

        [Fact]
        public async Task ShouldCreateRun()
        {
            var newRun = new RunEntity { Id = 3, GameId = 1, UserId = 1 };

            var game = await _gamesRepository.GetById(newRun.GameId);

            await _runRepository.CreateRun(newRun, game);

            var run = await _runRepository.GetById(newRun.Id);

            Assert.NotNull(run);
            Assert.IsType<RunEntity>(run);
            Assert.Equal(3, run.Id);
        }

        [Fact]
        public async Task ShouldUpdateRun()
        {
            var newRun = new RunEntity { Id = 1, UserId = 1, GameId = 1, Console = new ConsoleEntity { Name = "PlayStation 2" } };

            var runToUpdate = await _runRepository.GetById(newRun.Id);

            await _runRepository.UpdateRun(runToUpdate, newRun);

            var run = await _runRepository.GetById(1);

            Assert.NotNull(run);
            Assert.IsType<RunEntity>(run);
            Assert.Equal(1, run.Id);
            Assert.Equal("PlayStation 2", run.Console.Name);
        }
    }
}
