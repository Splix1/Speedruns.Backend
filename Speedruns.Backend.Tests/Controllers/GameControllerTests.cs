
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Speedruns.Backend.Controllers;
using Speedruns.Backend.Entities;
using Speedruns.Backend.Interfaces;
using System.Net;

namespace Speedruns.Backend.Tests.Controllers
{
    public class GameControllerTests
    {
        [Fact]
        public async Task ShouldReturn200GetAll()
        {
            var gamesRepositoryMock = Substitute.For<IGamesRepository>();

            gamesRepositoryMock.GetAll().Returns(new List<GameEntity> { new GameEntity { Id = 1 } });

            var controller = new GameController(gamesRepositoryMock);

            var response = await controller.GetAll();

            var result = response.Result as OkObjectResult;

            Assert.NotNull(result);
            Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);
        }

        [Fact]
        public async Task ShouldReturn500GetAll()
        {
            var gamesRepositoryMockWithError = Substitute.For<IGamesRepository>();

            gamesRepositoryMockWithError.GetAll().ThrowsAsync(new Exception("Error"));

            var controller = new GameController(gamesRepositoryMockWithError);

            var response = await controller.GetAll();

            var result = response.Result as StatusCodeResult;

            Assert.NotNull(result);
            Assert.Equal((int)HttpStatusCode.InternalServerError, result.StatusCode);
        }

        [Fact]
        public async Task ShouldReturn200GetById()
        {
            var gamesRepositoryMock = Substitute.For<IGamesRepository>();

            gamesRepositoryMock.GetById(Arg.Any<long>()).Returns(new GameEntity { Id = 1 });

            var controller = new GameController(gamesRepositoryMock);

            var response = await controller.GetById(1);

            var result = response.Result as OkObjectResult;

            Assert.NotNull(result);
            Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);
        }
    }
}
