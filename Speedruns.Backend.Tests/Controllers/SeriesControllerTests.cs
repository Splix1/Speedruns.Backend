﻿using Microsoft.AspNetCore.Mvc;
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

        [Fact]
        public async Task ShouldReturn200GetById()
        {
            var seriesRepositoryMock = Substitute.For<ISeriesRepository>();

            var series = new SeriesEntity { Id = 1 };

            seriesRepositoryMock.GetById(Arg.Any<long>()).Returns(series);

            var controller = new SeriesController(seriesRepositoryMock);

            var response = await controller.GetById(1);

            var result = response.Result as OkObjectResult;

            Assert.NotNull(result);
            Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);
        }

        [Fact]
        public async Task ShouldReturn404GetById()
        {
            var seriesRepositoryMock = Substitute.For<ISeriesRepository>();

            var controller = new SeriesController(seriesRepositoryMock);

            var response = await controller.GetById(1);

            var result = response.Result as NotFoundObjectResult;

            Assert.NotNull(result);
            Assert.Equal((int)HttpStatusCode.NotFound, result.StatusCode);
        }

        [Fact]
        public async Task ShouldReturn500GetById()
        {
            var seriesRepositoryMockWithError = Substitute.For<ISeriesRepository>();

            seriesRepositoryMockWithError.GetById(Arg.Any<long>()).ThrowsAsync(new Exception("Error"));

            var controller = new SeriesController(seriesRepositoryMockWithError);

            var response = await controller.GetById(1);

            var result = response.Result as StatusCodeResult;

            Assert.NotNull(result);
            Assert.Equal((int)HttpStatusCode.InternalServerError, result.StatusCode);
        }

        [Fact]
        public async Task ShouldReturn200GetByName()
        {
            var seriesRepositoryMock = Substitute.For<ISeriesRepository>();

            var series = new SeriesEntity { Id = 1, Name = "Super Mario Bros." };

            seriesRepositoryMock.GetByName(Arg.Any<string>()).Returns(series);

            var controller = new SeriesController(seriesRepositoryMock);

            var response = await controller.GetByName("Super Mario Bros.");

            var result = response.Result as OkObjectResult;

            Assert.NotNull(result);
            Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);
        }

        [Fact]
        public async Task ShouldReturn404GetByName()
        {
            var seriesRepositoryMock = Substitute.For<ISeriesRepository>();

            var controller = new SeriesController(seriesRepositoryMock);

            var response = await controller.GetByName("Super Mario Bros.");

            var result = response.Result as NotFoundObjectResult;

            Assert.NotNull(result);
            Assert.Equal((int)HttpStatusCode.NotFound, result.StatusCode);
        }

        [Fact]
        public async Task ShouldReturn500GetByName()
        {
            var seriesRepositoryMockWithError = Substitute.For<ISeriesRepository>();

            seriesRepositoryMockWithError.GetByName(Arg.Any<string>()).ThrowsAsync(new Exception("Error"));

            var controller = new SeriesController(seriesRepositoryMockWithError);

            var response = await controller.GetByName("Super Mario Bros.");

            var result = response.Result as StatusCodeResult;

            Assert.NotNull(result);
            Assert.Equal((int)HttpStatusCode.InternalServerError, result.StatusCode);
        }
    }
}
