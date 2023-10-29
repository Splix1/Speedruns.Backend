using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Speedruns.Backend.Controllers;
using Speedruns.Backend.Entities;
using Speedruns.Backend.Interfaces;
using System.Net;

namespace Speedruns.Backend.Tests.Controllers
{
    public class SeriesControllerTests
    {
        [Fact]
        public async Task ShouldReturn200GetAll()
        {
            var seriesRepositoryMock = Substitute.For<ISeriesRepository>();

            seriesRepositoryMock.GetAll().Returns(new List<SeriesEntity>
            {
                new SeriesEntity { Id = 1 },
                new SeriesEntity { Id = 2 }
            });

            var controller = new SeriesController(seriesRepositoryMock);
            
            var response = await controller.GetAll();

            var result = response.Result as OkObjectResult;

            Assert.NotNull(result);
            Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);
        }

        [Fact]
        public async Task ShouldReturn500GetAll()
        {
            var seriesRepositoryMockWithError = Substitute.For<ISeriesRepository>();

            seriesRepositoryMockWithError.GetAll().ThrowsAsync(new Exception("Error"));

            var controller = new SeriesController(seriesRepositoryMockWithError);

            var response = await controller.GetAll();

            var result = response.Result as StatusCodeResult;

            Assert.NotNull(result);
            Assert.Equal((int)HttpStatusCode.InternalServerError, result.StatusCode);
        }
    }
}
