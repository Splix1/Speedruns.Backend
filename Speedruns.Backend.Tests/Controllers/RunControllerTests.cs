using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Speedruns.Backend.Controllers;
using Speedruns.Backend.Entities;
using Speedruns.Backend.Interfaces;
using System.Net;

namespace Speedruns.Backend.Tests.Controllers
{
    public class RunControllerTests
    {

        [Fact]
        public async Task ShouldReturn200GetAll()
        {
            var runRepositoryMock = Substitute.For<IRunRepository>();
            var gamesRepositoryMock = Substitute.For<IGamesRepository>();


            runRepositoryMock.GetAll().Returns(new List<RunEntity>
            {
                new RunEntity { Id = 1, Date = DateTime.UtcNow, UserId = 1, ConsoleId = 1, GameId = 1, Time = 123 },
                new RunEntity { Id = 2, Date = DateTime.UtcNow, UserId = 1, ConsoleId = 2, GameId = 2, Time = 123}
            });

            var controller = new RunController(runRepositoryMock, gamesRepositoryMock);

            var response = await controller.GetAll();

            var result = response.Result as OkObjectResult;
            

            Assert.NotNull(result);
            Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);
        }

        [Fact]
        public async Task ShouldReturn500GetAll()
        {
            var runRepositoryMockWithError = Substitute.For<IRunRepository>();
            var gamesRepositoryMock = Substitute.For<IGamesRepository>();

            runRepositoryMockWithError.GetAll().ThrowsAsync(new Exception("Error"));

            var controller = new RunController(runRepositoryMockWithError, gamesRepositoryMock);

            var response = await controller.GetAll();

            var result = response.Result as StatusCodeResult;

            Assert.NotNull(result);
            Assert.Equal((int)HttpStatusCode.InternalServerError, result.StatusCode);
        }

        [Fact]
        public async Task ShouldReturn200GetById()
        {
            var runRepositoryMock = Substitute.For<IRunRepository>();
            var gamesRepositoryMock = Substitute.For<IGamesRepository>();

            runRepositoryMock.GetById(1).Returns(new RunEntity { Id = 1 });

            var controller = new RunController(runRepositoryMock, gamesRepositoryMock);

            var response = await controller.GetById(1);

            var result = response.Result as OkObjectResult;

            Assert.NotNull(result);
            Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);
        }

        [Fact]
        public async Task ShouldReturn404GetById()
        {
            var runRepositoryMock = Substitute.For<IRunRepository>();
            var gamesRepositoryMock = Substitute.For<IGamesRepository>();

            var controller = new RunController(runRepositoryMock, gamesRepositoryMock);

            var response = await controller.GetById(1);

            var result = response.Result as NotFoundObjectResult;

            Assert.NotNull(result);
            Assert.Equal((int)HttpStatusCode.NotFound, result.StatusCode);
        }

        [Fact]
        public async Task ShouldReturn500GetById()
        {
            var runRepositoryMockWithError = Substitute.For<IRunRepository>();
            var gamesRepositoryMockWithError = Substitute.For<IGamesRepository>();

            runRepositoryMockWithError.GetById(1).ThrowsAsync(new Exception("Error"));

            var controller = new RunController(runRepositoryMockWithError, gamesRepositoryMockWithError);

            var response = await controller.GetById(1);

            var result = response.Result as StatusCodeResult;

            Assert.NotNull(result);
            Assert.Equal((int)HttpStatusCode.InternalServerError, result.StatusCode);
        }

        [Fact]
        public async Task ShouldReturn201Created()
        {
            var runRepositoryMock = Substitute.For<IRunRepository>();
            var gamesRepositoryMock = Substitute.For<IGamesRepository>();
            
            var game = new GameEntity { Id = 1 };
            var run = new RunEntity { Id = 1, UserId = 1, GameId = game.Id, Game = game };

            runRepositoryMock.GetUserRuns(1).Returns(new List<RunEntity> {
                new RunEntity { Id = 100 }
            });
            gamesRepositoryMock.GetById(1).Returns(game);
            runRepositoryMock.CreateRun(run, game).Returns(run);
            runRepositoryMock.GetById(1).Returns(run);

            var controller = new RunController(runRepositoryMock, gamesRepositoryMock);

            var response = await controller.CreateRun(run);

            var result = response.Result as CreatedAtActionResult;

            Assert.NotNull(result);
            Assert.Equal((int)HttpStatusCode.Created, result.StatusCode);
        }

        [Fact]
        public async Task ShouldReturn500Created()
        {
            var runRepositoryMockWithError = Substitute.For<IRunRepository>();
            var gamesRepositoryMockWithError = Substitute.For<IGamesRepository>();

            var game = new GameEntity { Id = 1 };
            var run = new RunEntity { Id = 1, GameId = game.Id, Game = game };

            runRepositoryMockWithError.CreateRun(run, game).ThrowsAsync(new Exception("Error"));

            var controller = new RunController(runRepositoryMockWithError, gamesRepositoryMockWithError);

            var response = await controller.CreateRun(run);

            var result = response.Result as StatusCodeResult;

            Assert.NotNull(result);
            Assert.Equal((int)HttpStatusCode.InternalServerError, result.StatusCode);
        }

        [Fact]
        public async Task ShouldReturn400Created()
        {
            var runRepositoryMock = Substitute.For<IRunRepository>();
            var gamesRepositoryMock = Substitute.For<IGamesRepository>();

            var game = new GameEntity { Id = 1 };
            var run = new RunEntity {  Id = 1, GameId = game.Id, Game = game, UserId = 1 };

            runRepositoryMock.GetUserRuns(1).Returns(new List<RunEntity> { run });

            var controller = new RunController(runRepositoryMock, gamesRepositoryMock);

            var response = await controller.CreateRun(run);

            var result = response.Result as BadRequestObjectResult;

            Assert.NotNull(result);
            Assert.Equal((int)HttpStatusCode.BadRequest, result.StatusCode);
        }

        [Fact]
        public async Task ShouldReturn404Created()
        {
            var runRepositoryMock = Substitute.For<IRunRepository>();
            var gamesRepositoryMock = Substitute.For<IGamesRepository>();

            var run = new RunEntity { UserId = 1 };

            runRepositoryMock.GetUserRuns(1).Returns(new List<RunEntity>
            {
                new RunEntity { Id = 1 }
            });

            var controller = new RunController(runRepositoryMock, gamesRepositoryMock);

            var response = await controller.CreateRun(run);

            var result = response.Result as NotFoundObjectResult;

            Assert.NotNull(result);
            Assert.Equal((int)HttpStatusCode.NotFound, result.StatusCode);
        }

        [Fact]
        public async Task ShouldReturn200Updated()
        {
            var runRepositoryMock = Substitute.For<IRunRepository>();
            var gamesRepositoryMock = Substitute.For<IGamesRepository>();

            var runToUpdate = new RunEntity { Id = 1, Time = 123 };
            var updatedRun = new RunEntity { Id = 1, Time = 12345 };

            runRepositoryMock.GetById(1).Returns(runToUpdate);
            runRepositoryMock.UpdateRun(runToUpdate, updatedRun).Returns(updatedRun);

            var controller = new RunController(runRepositoryMock, gamesRepositoryMock);

            var response = await controller.UpdateRun(runToUpdate.Id, updatedRun);

            var result = response.Result as OkObjectResult;

            Assert.NotNull(result);
            Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);
        }

        [Fact]
        public async Task ShouldReturn404Updated()
        {
            var runRepositoryMock = Substitute.For<IRunRepository>();
            var gamesRepositoryMock = Substitute.For<IGamesRepository>();

            var controller = new RunController(runRepositoryMock, gamesRepositoryMock);

            var response = await controller.UpdateRun(1, new RunEntity { Id = 1, Time = 12345 });

            var result = response.Result as NotFoundObjectResult;

            Assert.NotNull(result);
            Assert.Equal((int)HttpStatusCode.NotFound, result.StatusCode);
        }

        [Fact]
        public async Task ShouldReturn500Updated()
        {
            var runRepositoryMockWithError = Substitute.For<IRunRepository>();
            var gamesRepositoryMock = Substitute.For<IGamesRepository>();

            runRepositoryMockWithError.GetById(Arg.Any<long>()).Returns(new RunEntity { Id = 1, Time = 123 });
            runRepositoryMockWithError.UpdateRun(Arg.Any<RunEntity>(), Arg.Any<RunEntity>()).ThrowsAsync(new Exception("Error"));

            var controller = new RunController(runRepositoryMockWithError, gamesRepositoryMock);

            var response = await controller.UpdateRun(1, new RunEntity { Id = 1, Time = 12345 });

            var result = response.Result as StatusCodeResult;

            Assert.NotNull(result);
            Assert.Equal((int)HttpStatusCode.InternalServerError, result.StatusCode);
        }

        [Fact]
        public async Task ShouldReturn200Deleted()
        {
            var runRepositoryMock = Substitute.For<IRunRepository>();
            var gamesRepositoryMock = Substitute.For<IGamesRepository>();

            runRepositoryMock.GetById(Arg.Any<long>()).Returns(new RunEntity { Id = 1 });

            var controller = new RunController(runRepositoryMock, gamesRepositoryMock);

            var response = await controller.DeleteRun(1);

            var result = response as OkObjectResult;

            Assert.NotNull(result);
            Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);
        }
    }
}
