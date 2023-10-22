

using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Speedruns.Backend.Controllers;
using Speedruns.Backend.Entities;
using Speedruns.Backend.Interfaces;
using System.Net;
using Xunit.Abstractions;

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

        [Fact]
        public async Task ShouldReturn404GetById()
        {
            var repositoryMock = Substitute.For<IUserRepository>();

            repositoryMock.GetById(Arg.Any<long>()).Returns(callInfo => (UserEntity)null!);


            var controller = new UserController(repositoryMock);

            var response = await controller.GetById(1);

            var result = response.Result as NotFoundObjectResult;            

            Assert.NotNull(result);
            Assert.Equal((int)HttpStatusCode.NotFound, result.StatusCode);

        }

        [Fact]
        public async Task ShouldReturn201CreateUser()
        {
            var repositoryMock = Substitute.For<IUserRepository>();

            var mockUser = new UserEntity { UserName = "Test" };

            repositoryMock.CreateUser(mockUser).Returns(mockUser);

            var controller = new UserController(repositoryMock);

            var response = await controller.CreateUser(mockUser);

            var result = response.Result as CreatedAtActionResult;

            Assert.NotNull(result);
            Assert.Equal((int)HttpStatusCode.Created, result.StatusCode);
        }

        [Fact]
        public async Task ShouldReturn500CreateUser()
        {
            var repositoryMockWithError = Substitute.For<IUserRepository>();

            var mockUser = new UserEntity { UserName = "Test" };

            repositoryMockWithError.CreateUser(mockUser).ThrowsAsync(new Exception("Internal error"));

            var controller = new UserController(repositoryMockWithError);

            var response = await controller.CreateUser(mockUser);

            var result = response.Result as StatusCodeResult;

            Assert.NotNull(result);
            Assert.Equal((int)HttpStatusCode.InternalServerError, result.StatusCode);
        }

        [Fact]
        public async Task ShouldReturn200UpdateUser()
        {
            var repositoryMock = Substitute.For<IUserRepository>();

            var mockUser = new UserEntity { Id = 1, UserName = "Test" };

            repositoryMock.GetById(Arg.Any<long>()).Returns(mockUser);
            repositoryMock.UpdateUser(Arg.Any<long>(), Arg.Any<UserEntity>()).Returns(callInfo => callInfo.Arg<UserEntity>());

            var controller = new UserController(repositoryMock);
           
            var response = await controller.UpdateUser(mockUser.Id, mockUser);

            var result = response.Result as OkObjectResult;


            Assert.NotNull(result);
            Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);
        }

        [Fact]
        public async Task ShouldReturn500UpdateUser()
        {
            var repositoryMockWithError = Substitute.For<IUserRepository>();

            var mockUser = new UserEntity { Id = 1, UserName = "Test" };

            repositoryMockWithError.GetById(Arg.Any<long>()).Returns(mockUser);
            repositoryMockWithError.UpdateUser(Arg.Any<long>(), Arg.Any<UserEntity>()).ThrowsAsync(new Exception("Internal error"));

            var controller = new UserController(repositoryMockWithError);

            var response = await controller.UpdateUser(mockUser.Id, mockUser);

            var result = response.Result as StatusCodeResult;

            Assert.NotNull(result);
            Assert.Equal((int)HttpStatusCode.InternalServerError, result.StatusCode);
        }

        [Fact]
        public async Task ShouldReturn200DeleteUser()
        {
            var repositoryMock = Substitute.For<IUserRepository>();

            var mockUser = new UserEntity { Id = 1, UserName = "Test" };

            repositoryMock.GetById(Arg.Any<long>()).Returns(mockUser);


            var controller = new UserController(repositoryMock);

            var response = await controller.DeleteUser(mockUser.Id);
           
            var result = response as OkObjectResult;

            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async Task ShouldReturn500DeleteUser()
        {
            var repositoryMockWithError = Substitute.For<IUserRepository>();

            var mockUser = new UserEntity { Id = 1, UserName = "Test" };

            repositoryMockWithError.GetById(Arg.Any<long>()).Returns(mockUser);
            repositoryMockWithError.DeleteUser(Arg.Any<UserEntity>()).ThrowsAsync(new Exception("Internal error"));

            var controller = new UserController(repositoryMockWithError);

            var response = await controller.DeleteUser(mockUser.Id);

            var result = response as StatusCodeResult;

            Assert.NotNull(result);
            Assert.Equal((int)HttpStatusCode.InternalServerError, result.StatusCode);

        }

    }
}
