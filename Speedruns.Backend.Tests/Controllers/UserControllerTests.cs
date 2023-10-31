

using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Speedruns.Backend.Controllers;
using Speedruns.Backend.Entities;
using Speedruns.Backend.Interfaces;
using Speedruns.Backend.Tests._Fixtures.Controllers;
using System.Net;
using Xunit.Abstractions;

namespace Speedruns.Backend.Tests.Controllers
{
    public class UserControllerTests : IClassFixture<UserControllerFixture>
    {

        private readonly UserControllerFixture _fixture;

        public UserControllerTests(UserControllerFixture fixture) 
        {  
            _fixture = fixture; 
        }

        [Fact]
        public async Task ShouldReturn200GetAll()
        {
            _fixture.ResetSubstitutes();

            _fixture.Repository.GetAll().Returns(new List<UserEntity>() { new UserEntity()
            {
                Id = 1,
                UserName = "Test",
            } });

            var controller = new UserController(_fixture.Repository);

            var response = await controller.GetAll();

            var result = response.Result as OkObjectResult;

            Assert.NotNull(result);
            Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);
        }

        [Fact]
        public async Task ShouldReturn500GetAll()
        {
            _fixture.ResetSubstitutes();

            _fixture.Repository.GetAll().ThrowsAsync(new Exception("Internal errro"));

            var controller = new UserController(_fixture.Repository);

            var response = await controller.GetAll();

            var result = response.Result as StatusCodeResult;

            Assert.NotNull(result);
            Assert.Equal((int)HttpStatusCode.InternalServerError, result.StatusCode);
        }

        [Fact]
        public async Task ShouldReturn200GetById()
        {
            _fixture.ResetSubstitutes();

            _fixture.Repository.GetById(1).Returns(new UserEntity() {
                Id = 1,
                UserName = "Test",
            });

            var controller = new UserController(_fixture.Repository);

            var response = await controller.GetById(1);

            var result = response.Result as OkObjectResult;

            Assert.NotNull(result);
            Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);
        }

        [Fact]
        public async Task ShouldReturn500GetById()
        {
            _fixture.ResetSubstitutes();

            _fixture.Repository.GetById(1).ThrowsAsync(new Exception("Internal error"));

            var controller = new UserController(_fixture.Repository);

            var response = await controller.GetById(1);

            var result = response.Result as StatusCodeResult;

            Assert.NotNull(result);
            Assert.Equal((int)HttpStatusCode.InternalServerError, result.StatusCode);
        }

        [Fact]
        public async Task ShouldReturn404GetById()
        {
            _fixture.ResetSubstitutes();

            var controller = new UserController(_fixture.Repository);

            var response = await controller.GetById(1);

            var result = response.Result as NotFoundObjectResult;            

            Assert.NotNull(result);
            Assert.Equal((int)HttpStatusCode.NotFound, result.StatusCode);

        }

        [Fact]
        public async Task ShouldReturn201CreateUser()
        {
            _fixture.ResetSubstitutes();

            var mockUser = new UserEntity { UserName = "Test" };

            _fixture.Repository.CreateUser(mockUser).Returns(mockUser);

            var controller = new UserController(_fixture.Repository);

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
            _fixture.ResetSubstitutes();

            var mockUser = new UserEntity { Id = 1, UserName = "Test" };

            _fixture.Repository.GetById(Arg.Any<long>()).Returns(mockUser);
            _fixture.Repository.UpdateUser(Arg.Any<long>(), Arg.Any<UserEntity>()).Returns(callInfo => callInfo.Arg<UserEntity>());

            var controller = new UserController(_fixture.Repository);
           
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
        public async Task ShouldReturn404UpdateUser()
        {
            var repositoryMockWithError = Substitute.For<IUserRepository>();

            var mockUser = new UserEntity { Id = 1, UserName = "Test" };

            var controller = new UserController(repositoryMockWithError);

            var response = await controller.UpdateUser(mockUser.Id, mockUser);

            var result = response.Result as NotFoundObjectResult; 
            
            Assert.NotNull(result);
            Assert.Equal((int)HttpStatusCode.NotFound, result.StatusCode);
        }

        [Fact]
        public async Task ShouldReturn200DeleteUser()
        {
            _fixture.ResetSubstitutes();

            var mockUser = new UserEntity { Id = 1, UserName = "Test" };

            _fixture.Repository.GetById(Arg.Any<long>()).Returns(mockUser);


            var controller = new UserController(_fixture.Repository);

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

        [Fact]
        public async Task ShouldReturn404DeleteUser()
        {
            _fixture.ResetSubstitutes();

            var mockUser = new UserEntity { Id = 1, UserName = "Test" };

            var controller = new UserController(_fixture.Repository);

            var response = await controller.DeleteUser(mockUser.Id);

            var result = response as NotFoundObjectResult;

            Assert.NotNull(result);
            Assert.Equal((int)HttpStatusCode.NotFound, result.StatusCode);
        }

    }
}
