
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
    public class GameControllerTests : IClassFixture<GameControllerFixture>
    {
        private readonly GameControllerFixture _fixture;

        public GameControllerTests(GameControllerFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task ShouldReturn200GetAll()
        {
            _fixture.ResetSubstitutes();
            _fixture.Repository.GetAll().Returns(new List<GameEntity> { new GameEntity { Id = 1 } });

            var controller = new GameController(_fixture.Repository);

            var response = await controller.GetAll();

            var result = response.Result as OkObjectResult;

            Assert.NotNull(result);
            Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);
        }

        [Fact]
        public async Task ShouldReturn500GetAll()
        {
            _fixture.ResetSubstitutes();
            _fixture.Repository.GetAll().ThrowsAsync(new Exception("Error"));

            var controller = new GameController(_fixture.Repository);

            var response = await controller.GetAll();

            var result = response.Result as StatusCodeResult;

            Assert.NotNull(result);
            Assert.Equal((int)HttpStatusCode.InternalServerError, result.StatusCode);
        }

        [Fact]
        public async Task ShouldReturn200GetById()
        {
            _fixture.ResetSubstitutes();
            _fixture.Repository.GetById(Arg.Any<long>()).Returns(new GameEntity { Id = 1 });

            var controller = new GameController(_fixture.Repository);

            var response = await controller.GetById(1);

            var result = response.Result as OkObjectResult;

            Assert.NotNull(result);
            Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);
        }

        [Fact]
        public async Task ShouldReturn404GetById()
        {
            _fixture.ResetSubstitutes();
            var controller = new GameController(_fixture.Repository);

            var response = await controller.GetById(1);

            var result = response.Result as NotFoundObjectResult;

            Assert.NotNull(result);
            Assert.Equal((int)HttpStatusCode.NotFound, result.StatusCode);
        }

        [Fact]
        public async Task ShouldReturn500GetById()
        {
            _fixture.ResetSubstitutes();
            _fixture.Repository.GetById(Arg.Any<long>()).ThrowsAsync(new Exception("Error"));

            var controller = new GameController(_fixture.Repository);

            var response = await controller.GetById(1);

            var result = response.Result as StatusCodeResult;

            Assert.NotNull(result);
            Assert.Equal((int)HttpStatusCode.InternalServerError, result.StatusCode);
        }

        [Fact]
        public async Task ShouldReturn200GetByName()
        {
            _fixture.ResetSubstitutes();
            _fixture.Repository.GetByName(Arg.Any<string>()).Returns(new GameEntity { Id = 1, Name = "Super Mario 64" });

            var controller = new GameController(_fixture.Repository);

            var response = await controller.GetByName("Super Mario 64");

            var result = response.Result as OkObjectResult;

            Assert.NotNull(result);
            Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);
        }

        [Fact]
        public async Task ShouldReturn404GetByName()
        {
            _fixture.ResetSubstitutes();
            var controller = new GameController(_fixture.Repository);

            var response = await controller.GetByName("Super Mario 64");

            var result = response.Result as NotFoundObjectResult;

            Assert.NotNull(result);
            Assert.Equal((int)HttpStatusCode.NotFound, result.StatusCode);
        }

        [Fact]
        public async Task ShouldReturn500GetByName()
        {
            _fixture.ResetSubstitutes();
            _fixture.Repository.GetByName(Arg.Any<string>()).ThrowsAsync(new Exception("Error"));

            var controller = new GameController(_fixture.Repository);

            var response = await controller.GetByName("Super Mario 64");

            var result = response.Result as StatusCodeResult; 
            
            Assert.NotNull(result);
            Assert.Equal((int)HttpStatusCode.InternalServerError, result.StatusCode);
        }
    }
}
