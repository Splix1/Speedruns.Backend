using Speedruns.Backend.Controllers;
using NSubstitute;
using Speedruns.Backend.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using NSubstitute.ExceptionExtensions;
using Speedruns.Backend.Entities;

namespace Speedruns.Backend.Tests.Controllers
{
    public class ConsoleControllerTests
    {
        [Fact]
        public async Task ShouldReturn200()
        {
            // create mock of repository
            var repositoryMock = Substitute.For<IConsolesRepository>();

            repositoryMock.GetAll().Returns(new List<ConsoleEntity>() { new ConsoleEntity
            {
                Id = 1,
                Name = "DummyConsole",
            }
            });

            // create new instance of controller using mock repository
            var controller = new ConsoleController(repositoryMock);

            // make request using controller
            var response = await controller.GetAll();

            // store result in a variable to test
            var result = response.Result as OkObjectResult;

            Assert.NotNull(result);
            Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);
        }

        [Fact]
        public async Task ShouldReturn500()
        {
            var repositoryMockWithError = Substitute.For<IConsolesRepository>();

            repositoryMockWithError.GetAll().ThrowsAsync(new Exception("Internal error"));

            var controller = new ConsoleController(repositoryMockWithError);

            var response = await controller.GetAll();

            var result = response.Result as StatusCodeResult;


            Assert.NotNull(result);
            Assert.Equal((int)HttpStatusCode.InternalServerError, result.StatusCode);
        }
    }
}
