﻿

using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Speedruns.Backend.Controllers;
using Speedruns.Backend.Entities;
using Speedruns.Backend.Interfaces;
using System.Net;

namespace Speedruns.Backend.Tests.Controllers
{
    public class UserControllerTests
    {
        [Fact]
        public async Task ShouldReturn200GetAll()
        {
            var repositoryMock = Substitute.For<IUserRepository>();

            repositoryMock.GetAll().Returns(new List<UserEntity>() { new UserEntity()
            {
                Id = 1,
                UserName = "Test",
            } });

            var controller = new UserController(repositoryMock);

            var response = await controller.GetAll();

            var result = response.Result as OkObjectResult;

            Assert.NotNull(result);
            Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);
        }

        [Fact]
        public async Task ShouldReturn500GetAll()
        {
            var repositoryMockWithError = Substitute.For<IUserRepository>();

            repositoryMockWithError.GetAll().ThrowsAsync(new Exception("Internal errro"));

            var controller = new UserController(repositoryMockWithError);

            var response = await controller.GetAll();

            var result = response.Result as StatusCodeResult;

            Assert.NotNull(result);
            Assert.Equal((int)HttpStatusCode.InternalServerError, result.StatusCode);
        }

        [Fact]
        public async Task ShouldReturn200GetById()
        {
            var repositoryMock = Substitute.For<IUserRepository>();

            repositoryMock.GetById(1).Returns(new UserEntity() {
                Id = 1,
                UserName = "Test",
            });

            var controller = new UserController(repositoryMock);

            var response = await controller.GetById(1);

            var result = response.Result as OkObjectResult;

            Assert.NotNull(result);
            Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);
        }

        [Fact]
        public async Task ShouldReturn500GetById()
        {
            var repositoryMockWithError = Substitute.For<IUserRepository>();

            repositoryMockWithError.GetById(1).ThrowsAsync(new Exception("Internal error"));

            var controller = new UserController(repositoryMockWithError);

            var response = await controller.GetById(1);

            var result = response.Result as StatusCodeResult;

            Assert.NotNull(result);
            Assert.Equal((int)HttpStatusCode.InternalServerError, result.StatusCode);
        }

      
    }
}
