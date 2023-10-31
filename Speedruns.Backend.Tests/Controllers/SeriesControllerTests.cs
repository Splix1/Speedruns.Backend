using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Speedruns.Backend.Controllers;
using Speedruns.Backend.Entities;
using Speedruns.Backend.Tests._Fixtures.Controllers;
using System.Net;

namespace Speedruns.Backend.Tests.Controllers
{
    public class SeriesControllerTests : IClassFixture<SeriesControllerFixture>
    {
        private readonly SeriesControllerFixture _fixture;

        public SeriesControllerTests(SeriesControllerFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task ShouldReturn200GetAll()
        {
            _fixture.ResetSubstitutes();

            _fixture.Repository.GetAll().Returns(new List<SeriesEntity>
            {
                new SeriesEntity { Id = 1 },
                new SeriesEntity { Id = 2 }
            });

            var controller = new SeriesController(_fixture.Repository);
            
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

            var controller = new SeriesController(_fixture.Repository);

            var response = await controller.GetAll();

            var result = response.Result as StatusCodeResult;

            Assert.NotNull(result);
            Assert.Equal((int)HttpStatusCode.InternalServerError, result.StatusCode);
        }

        [Fact]
        public async Task ShouldReturn200GetById()
        {
            _fixture.ResetSubstitutes();
            var series = new SeriesEntity { Id = 1 };

            _fixture.Repository.GetById(Arg.Any<long>()).Returns(series);

            var controller = new SeriesController(_fixture.Repository);

            var response = await controller.GetById(1);

            var result = response.Result as OkObjectResult;

            Assert.NotNull(result);
            Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);
        }

        [Fact]
        public async Task ShouldReturn404GetById()
        {
            _fixture.ResetSubstitutes();

            var controller = new SeriesController(_fixture.Repository);

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

            var controller = new SeriesController(_fixture.Repository);

            var response = await controller.GetById(1);

            var result = response.Result as StatusCodeResult;

            Assert.NotNull(result);
            Assert.Equal((int)HttpStatusCode.InternalServerError, result.StatusCode);
        }

        [Fact]
        public async Task ShouldReturn200GetByName()
        {
            _fixture.ResetSubstitutes();

            var series = new SeriesEntity { Id = 1, Name = "Super Mario Bros." };

            _fixture.Repository.GetByName(Arg.Any<string>()).Returns(series);

            var controller = new SeriesController(_fixture.Repository);

            var response = await controller.GetByName("Super Mario Bros.");

            var result = response.Result as OkObjectResult;

            Assert.NotNull(result);
            Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);
        }

        [Fact]
        public async Task ShouldReturn404GetByName()
        {
            _fixture.ResetSubstitutes();

            var controller = new SeriesController(_fixture.Repository);

            var response = await controller.GetByName("Super Mario Bros.");

            var result = response.Result as NotFoundObjectResult;

            Assert.NotNull(result);
            Assert.Equal((int)HttpStatusCode.NotFound, result.StatusCode);
        }

        [Fact]
        public async Task ShouldReturn500GetByName()
        {
            _fixture.ResetSubstitutes();

            _fixture.Repository.GetByName(Arg.Any<string>()).ThrowsAsync(new Exception("Error"));

            var controller = new SeriesController(_fixture.Repository);

            var response = await controller.GetByName("Super Mario Bros.");

            var result = response.Result as StatusCodeResult;

            Assert.NotNull(result);
            Assert.Equal((int)HttpStatusCode.InternalServerError, result.StatusCode);
        }
    }
}
