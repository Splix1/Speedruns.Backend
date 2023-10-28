
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
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
    }
}
