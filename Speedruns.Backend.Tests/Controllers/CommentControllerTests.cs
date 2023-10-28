using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Speedruns.Backend.Controllers;
using Speedruns.Backend.Entities;
using Speedruns.Backend.Interfaces;
using System.Net;

namespace Speedruns.Backend.Tests.Controllers
{
    public class CommentControllerTests
    {
        [Fact]
        public async Task ShouldReturn200GetAll()
        {
            var commentsRepositoryMock = Substitute.For<ICommentsRepository>();
            var runsRepositoryMock = Substitute.For<IRunRepository>();

            var run = new RunEntity { Id = 1 };

            runsRepositoryMock.GetById(Arg.Any<long>()).Returns(run);
            commentsRepositoryMock.GetComments(Arg.Any<long>()).Returns(new List<CommentEntity>
            {
                new CommentEntity { Id = 1, Text = "Test 1" },
                new CommentEntity { Id = 2, Text = "Test 2" }
            });

            var controller = new CommentController(commentsRepositoryMock, runsRepositoryMock);

            var response = await controller.GetAll(1);

            var result = response.Result as OkObjectResult;

            Assert.NotNull(result);
            Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);
        }

        [Fact]
        public async Task ShouldReturn404GetAll()
        {
            var commentsRepositoryMock = Substitute.For<ICommentsRepository>();
            var runsRepositoryMock = Substitute.For<IRunRepository>();

            var controller = new CommentController(commentsRepositoryMock, runsRepositoryMock);

            var response = await controller.GetAll(1);

            var result = response.Result as NotFoundObjectResult;

            Assert.NotNull(result);
            Assert.Equal((int)HttpStatusCode.NotFound, result.StatusCode);
        }

        [Fact]
        public async Task ShouldReturn500GetAll()
        {
            var commentsRepositoryMock = Substitute.For<ICommentsRepository>();
            var runsRepositoryMock = Substitute.For<IRunRepository>();

            runsRepositoryMock.GetById(Arg.Any<long>()).ThrowsAsync(new Exception("Error"));

            var controller = new CommentController(commentsRepositoryMock, runsRepositoryMock);

            var response = await controller.GetAll(1);

            var result = response.Result as StatusCodeResult;

            Assert.NotNull(result);
            Assert.Equal((int)HttpStatusCode.InternalServerError, result.StatusCode);
        }

        [Fact]
        public async Task ShouldReturn201Created()
        {
            var commentsRepositoryMock = Substitute.For<ICommentsRepository>();
            var runsRepositoryMock = Substitute.For<IRunRepository>();

            var comment = new CommentEntity { Id = 1, Text = "Test" };

            commentsRepositoryMock.AddComment(Arg.Any<CommentEntity>()).Returns(comment);
            commentsRepositoryMock.GetCommentById(Arg.Any<long>()).Returns(comment);

            var controller = new CommentController(commentsRepositoryMock, runsRepositoryMock);

            var response = await controller.AddComment(comment);

            var result = response.Result as CreatedAtActionResult;

            Assert.NotNull(result);
            Assert.Equal((int)HttpStatusCode.Created, result.StatusCode);
        }

        [Fact]
        public async Task ShouldReturn500Created()
        {
            var commentsRepositoryMockWithError = Substitute.For<ICommentsRepository>();
            var runsRepositoryMock = Substitute.For<IRunRepository>();

            var comment = new CommentEntity { Id = 1 };

            commentsRepositoryMockWithError.AddComment(Arg.Any<CommentEntity>()).ThrowsAsync(new Exception("Error"));

            var controller = new CommentController(commentsRepositoryMockWithError, runsRepositoryMock);

            var response = await controller.AddComment(comment);

            var result = response.Result as StatusCodeResult;

            Assert.NotNull(result);

            Assert.Equal((int)HttpStatusCode.InternalServerError, result.StatusCode);
        }


        [Fact]
        public async Task ShouldReturn200Updated()
        {
            var commentsRepositoryMock = Substitute.For<ICommentsRepository>();
            var runsRepositoryMock = Substitute.For<IRunRepository>();

            var comment = new CommentEntity { Id = 1 };

            commentsRepositoryMock.GetCommentById(Arg.Any<long>()).Returns(comment);

            var controller = new CommentController(commentsRepositoryMock, runsRepositoryMock);

            var response = await controller.UpdateComment(comment);

            var result = response.Result as OkObjectResult;

            Assert.NotNull(result);
            Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);
        }

        [Fact]
        public async Task ShouldReturn404Updated()
        {
            var commentsRepositoryMock = Substitute.For<ICommentsRepository>();
            var runsRepositoryMock = Substitute.For<IRunRepository>();

            var comment = new CommentEntity { Id = 1 };

            var controller = new CommentController(commentsRepositoryMock, runsRepositoryMock);

            var response = await controller.UpdateComment(comment);

            var result = response.Result as NotFoundObjectResult;

            Assert.NotNull(result);
            Assert.Equal((int)HttpStatusCode.NotFound, result.StatusCode);
        }

        [Fact]
        public async Task ShouldReturn500Updated()
        {
            var commentsRepositoryMock = Substitute.For<ICommentsRepository>();
            var runsRepositoryMock = Substitute.For<IRunRepository>();

            commentsRepositoryMock.GetCommentById(Arg.Any<long>()).ThrowsAsync(new Exception("Error"));

            var comment = new CommentEntity { Id = 1 };

            var controller = new CommentController(commentsRepositoryMock, runsRepositoryMock);

            var response = await controller.UpdateComment(comment);

            var result = response.Result as StatusCodeResult;

            Assert.NotNull(result);
            Assert.Equal((int)HttpStatusCode.InternalServerError, result.StatusCode);
        }
    }
}
