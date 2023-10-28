using Microsoft.AspNetCore.Mvc;
using NSubstitute;
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
    }
}
