using Speedruns.Backend.Controllers;
using NSubstitute;
using Speedruns.Backend.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using NSubstitute.ExceptionExtensions;
using Speedruns.Backend.Entities;
using Speedruns.Backend.Tests._Fixtures.Controllers;

namespace Speedruns.Backend.Tests.Controllers
{
    public class ConsoleControllerTests : IClassFixture<ConsoleControllerFixture>
    {

        private readonly ConsoleControllerFixture _fixture;

        public ConsoleControllerTests(ConsoleControllerFixture fixture)
        {
            _fixture = fixture;
        }


        [Fact]
        public async Task ShouldReturn200()
        {
            _fixture.ResetSubstitutes();
            _fixture.Repository.GetAll().Returns(new List<ConsoleEntity>() { new ConsoleEntity
            {
                Id = 1,
                Name = "DummyConsole",
            }
            });

            // create new instance of controller using mock repository
            var controller = new ConsoleController(_fixture.Repository);

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
            _fixture.ResetSubstitutes();
            _fixture.Repository.GetAll().ThrowsAsync(new Exception("Internal error"));

            var controller = new ConsoleController(_fixture.Repository);

            var response = await controller.GetAll();

            var result = response.Result as StatusCodeResult;


            Assert.NotNull(result);
            Assert.Equal((int)HttpStatusCode.InternalServerError, result.StatusCode);
        }
    }
}
