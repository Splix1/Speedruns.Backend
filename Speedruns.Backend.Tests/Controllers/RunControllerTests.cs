using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Speedruns.Backend.Controllers;
using Speedruns.Backend.Entities;
using Speedruns.Backend.Interfaces;
using Speedruns.Backend.Tests._Fixtures.Controllers;
using System.Net;

namespace Speedruns.Backend.Tests.Controllers
{
    public class RunControllerTests : IClassFixture<RunControllerFixture>
    {
        private readonly RunControllerFixture _fixture;

        public RunControllerTests(RunControllerFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task ShouldReturn200GetAll()
        {
            _fixture.ResetSubstitutes();
            _fixture.RunRepository.GetAll().Returns(new List<RunEntity>
            {
                new RunEntity { Id = 1, Date = DateTime.UtcNow, UserId = 1, ConsoleId = 1, GameId = 1, Time = 123 },
                new RunEntity { Id = 2, Date = DateTime.UtcNow, UserId = 1, ConsoleId = 2, GameId = 2, Time = 123}
            });

            var controller = new RunController(_fixture.RunRepository, _fixture.GamesRepository);

            var response = await controller.GetAll();

            var result = response.Result as OkObjectResult;
            

            Assert.NotNull(result);
            Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);
        }

        [Fact]
        public async Task ShouldReturn500GetAll()
        {

            _fixture.ResetSubstitutes();
            _fixture.RunRepository.GetAll().ThrowsAsync(new Exception("Error"));

            var controller = new RunController(_fixture.RunRepository, _fixture.GamesRepository);

            var response = await controller.GetAll();

            var result = response.Result as StatusCodeResult;

            Assert.NotNull(result);
            Assert.Equal((int)HttpStatusCode.InternalServerError, result.StatusCode);
        }

        [Fact]
        public async Task ShouldReturn200GetById()
        {

            _fixture.ResetSubstitutes();
            _fixture.RunRepository.GetById(1).Returns(new RunEntity { Id = 1 });

            var controller = new RunController(_fixture.RunRepository, _fixture.GamesRepository);

            var response = await controller.GetById(1);

            var result = response.Result as OkObjectResult;

            Assert.NotNull(result);
            Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);
        }

        [Fact]
        public async Task ShouldReturn404GetById()
        {
            _fixture.ResetSubstitutes();
            var controller = new RunController(_fixture.RunRepository, _fixture.GamesRepository);

            var response = await controller.GetById(1);

            var result = response.Result as NotFoundObjectResult;

            Assert.NotNull(result);
            Assert.Equal((int)HttpStatusCode.NotFound, result.StatusCode);
        }

        [Fact]
        public async Task ShouldReturn500GetById()
        {
            _fixture.ResetSubstitutes();
            _fixture.RunRepository.GetById(1).ThrowsAsync(new Exception("Error"));

            var controller = new RunController(_fixture.RunRepository, _fixture.GamesRepository);

            var response = await controller.GetById(1);

            var result = response.Result as StatusCodeResult;

            Assert.NotNull(result);
            Assert.Equal((int)HttpStatusCode.InternalServerError, result.StatusCode);
        }

        [Fact]
        public async Task ShouldReturn201Created()
        {
            _fixture.ResetSubstitutes();
            var game = new GameEntity { Id = 1 };
            var run = new RunEntity { Id = 1, UserId = 1, GameId = game.Id, Game = game };

            _fixture.RunRepository.GetUserRuns(1).Returns(new List<RunEntity> {
                new RunEntity { Id = 100 }
            });
            _fixture.GamesRepository.GetById(1).Returns(game);
            _fixture.RunRepository.CreateRun(run, game).Returns(run);
            _fixture.RunRepository.GetById(1).Returns(run);

            var controller = new RunController(_fixture.RunRepository, _fixture.GamesRepository);

            var response = await controller.CreateRun(run);

            var result = response.Result as CreatedAtActionResult;

            Assert.NotNull(result);
            Assert.Equal((int)HttpStatusCode.Created, result.StatusCode);
        }

        [Fact]
        public async Task ShouldReturn500Created()
        {

            _fixture.ResetSubstitutes();
            var game = new GameEntity { Id = 1 };
            var run = new RunEntity { Id = 1, GameId = game.Id, Game = game };

            _fixture.RunRepository.CreateRun(run, game).ThrowsAsync(new Exception("Error"));

            var controller = new RunController(_fixture.RunRepository, _fixture.GamesRepository);

            var response = await controller.CreateRun(run);

            var result = response.Result as StatusCodeResult;

            Assert.NotNull(result);
            Assert.Equal((int)HttpStatusCode.InternalServerError, result.StatusCode);
        }

        [Fact]
        public async Task ShouldReturn400Created()
        {

            _fixture.ResetSubstitutes();
            var game = new GameEntity { Id = 1 };
            var run = new RunEntity {  Id = 1, GameId = game.Id, Game = game, UserId = 1 };

            _fixture.RunRepository.GetUserRuns(1).Returns(new List<RunEntity> { run });

            var controller = new RunController(_fixture.RunRepository, _fixture.GamesRepository);

            var response = await controller.CreateRun(run);

            var result = response.Result as BadRequestObjectResult;

            Assert.NotNull(result);
            Assert.Equal((int)HttpStatusCode.BadRequest, result.StatusCode);
        }

        [Fact]
        public async Task ShouldReturn404Created()
        {

            _fixture.ResetSubstitutes();
            var run = new RunEntity { UserId = 1 };

            _fixture.RunRepository.GetUserRuns(1).Returns(new List<RunEntity>
            {
                new RunEntity { Id = 1 }
            });

            var controller = new RunController(_fixture.RunRepository, _fixture.GamesRepository);

            var response = await controller.CreateRun(run);

            var result = response.Result as NotFoundObjectResult;

            Assert.NotNull(result);
            Assert.Equal((int)HttpStatusCode.NotFound, result.StatusCode);
        }

        [Fact]
        public async Task ShouldReturn200Updated()
        {

            _fixture.ResetSubstitutes();
            var runToUpdate = new RunEntity { Id = 1, Time = 123 };
            var updatedRun = new RunEntity { Id = 1, Time = 12345 };

            _fixture.RunRepository.GetById(1).Returns(runToUpdate);
            _fixture.RunRepository.UpdateRun(runToUpdate, updatedRun).Returns(updatedRun);

            var controller = new RunController(_fixture.RunRepository, _fixture.GamesRepository);

            var response = await controller.UpdateRun(runToUpdate.Id, updatedRun);

            var result = response.Result as OkObjectResult;

            Assert.NotNull(result);
            Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);
        }

        [Fact]
        public async Task ShouldReturn404Updated()
        {
            _fixture.ResetSubstitutes();
            var controller = new RunController(_fixture.RunRepository, _fixture.GamesRepository);

            var response = await controller.UpdateRun(1, new RunEntity { Id = 1, Time = 12345 });

            var result = response.Result as NotFoundObjectResult;

            Assert.NotNull(result);
            Assert.Equal((int)HttpStatusCode.NotFound, result.StatusCode);
        }

        [Fact]
        public async Task ShouldReturn500Updated()
        {

            _fixture.ResetSubstitutes();
            _fixture.RunRepository.GetById(Arg.Any<long>()).Returns(new RunEntity { Id = 1, Time = 123 });
            _fixture.RunRepository.UpdateRun(Arg.Any<RunEntity>(), Arg.Any<RunEntity>()).ThrowsAsync(new Exception("Error"));

            var controller = new RunController(_fixture.RunRepository, _fixture.GamesRepository);

            var response = await controller.UpdateRun(1, new RunEntity { Id = 1, Time = 12345 });

            var result = response.Result as StatusCodeResult;

            Assert.NotNull(result);
            Assert.Equal((int)HttpStatusCode.InternalServerError, result.StatusCode);
        }

        [Fact]
        public async Task ShouldReturn200Deleted()
        {

            _fixture.ResetSubstitutes();
            _fixture.RunRepository.GetById(Arg.Any<long>()).Returns(new RunEntity { Id = 1 });

            var controller = new RunController(_fixture.RunRepository, _fixture.GamesRepository);

            var response = await controller.DeleteRun(1);

            var result = response as OkObjectResult;

            Assert.NotNull(result);
            Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);
        }

        [Fact]
        public async Task ShouldReturn404Deleted()
        {

            _fixture.ResetSubstitutes();
            var controller = new RunController(_fixture.RunRepository, _fixture.GamesRepository);

            var response = await controller.DeleteRun(1);

            var result = response as NotFoundObjectResult;

            Assert.NotNull(result);
            Assert.Equal((int)HttpStatusCode.NotFound, result.StatusCode);
        }

        [Fact]
        public async Task ShouldReturn500Deleted()
        {

            _fixture.ResetSubstitutes();
            _fixture.RunRepository.GetById(Arg.Any<long>()).Returns(new RunEntity { Id = 1 });
            _fixture.RunRepository.DeleteRun(Arg.Any<RunEntity>()).ThrowsAsync(new Exception("Error"));

            var controller = new RunController(_fixture.RunRepository, _fixture.GamesRepository);

            var response = await controller.DeleteRun(1);

            var result = response as StatusCodeResult;

            Assert.NotNull(result);
            Assert.Equal((int)HttpStatusCode.InternalServerError, result.StatusCode);
        }
    }
}
