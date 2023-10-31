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
    public class CommentControllerTests : IClassFixture<CommentControllerFixture>
    {
        private readonly CommentControllerFixture _fixture;

        public CommentControllerTests(CommentControllerFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task ShouldReturn200GetAll()
        {
            _fixture.ResetRepositories();
            var run = new RunEntity { Id = 1 };

            _fixture.RunRepository.GetById(Arg.Any<long>()).Returns(run);
            _fixture.CommentsRepository.GetComments(Arg.Any<long>()).Returns(new List<CommentEntity>
            {
                new CommentEntity { Id = 1, Text = "Test 1" },
                new CommentEntity { Id = 2, Text = "Test 2" }
            });

            var controller = new CommentController(_fixture.CommentsRepository, _fixture.RunRepository);

            var response = await controller.GetAll(1);

            var result = response.Result as OkObjectResult;

            Assert.NotNull(result);
            Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);
        }

        [Fact]
        public async Task ShouldReturn404GetAll()
        {
            _fixture.ResetRepositories();
            var controller = new CommentController(_fixture.CommentsRepository, _fixture.RunRepository);

            var response = await controller.GetAll(1);

            var result = response.Result as NotFoundObjectResult;

            Assert.NotNull(result);
            Assert.Equal((int)HttpStatusCode.NotFound, result.StatusCode);
        }

        [Fact]
        public async Task ShouldReturn500GetAll()
        {
            _fixture.ResetRepositories();
            _fixture.RunRepository.GetById(Arg.Any<long>()).ThrowsAsync(new Exception("Error"));

            var controller = new CommentController(_fixture.CommentsRepository, _fixture.RunRepository);

            var response = await controller.GetAll(1);

            var result = response.Result as StatusCodeResult;

            Assert.NotNull(result);
            Assert.Equal((int)HttpStatusCode.InternalServerError, result.StatusCode);
        }

        [Fact]
        public async Task ShouldReturn201Created()
        {
            _fixture.ResetRepositories();
            var comment = new CommentEntity { Id = 1, Text = "Test" };

            _fixture.CommentsRepository.AddComment(Arg.Any<CommentEntity>()).Returns(comment);
            _fixture.CommentsRepository.GetCommentById(Arg.Any<long>()).Returns(comment);

            var controller = new CommentController(_fixture.CommentsRepository, _fixture.RunRepository);

            var response = await controller.AddComment(comment);

            var result = response.Result as CreatedAtActionResult;

            Assert.NotNull(result);
            Assert.Equal((int)HttpStatusCode.Created, result.StatusCode);
        }

        [Fact]
        public async Task ShouldReturn500Created()
        {
            _fixture.ResetRepositories();
            var comment = new CommentEntity { Id = 1 };

            _fixture.CommentsRepository.AddComment(Arg.Any<CommentEntity>()).ThrowsAsync(new Exception("Error"));

            var controller = new CommentController(_fixture.CommentsRepository, _fixture.RunRepository);

            var response = await controller.AddComment(comment);

            var result = response.Result as StatusCodeResult;

            Assert.NotNull(result);

            Assert.Equal((int)HttpStatusCode.InternalServerError, result.StatusCode);
        }


        [Fact]
        public async Task ShouldReturn200Updated()
        {
            _fixture.ResetRepositories();
            var comment = new CommentEntity { Id = 1 };

            _fixture.CommentsRepository.GetCommentById(Arg.Any<long>()).Returns(comment);

            var controller = new CommentController(_fixture.CommentsRepository, _fixture.RunRepository);

            var response = await controller.UpdateComment(comment);

            var result = response.Result as OkObjectResult;

            Assert.NotNull(result);
            Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);
        }

        [Fact]
        public async Task ShouldReturn404Updated()
        {
            _fixture.ResetRepositories();
            var comment = new CommentEntity { Id = 1 };

            var controller = new CommentController(_fixture.CommentsRepository, _fixture.RunRepository);

            var response = await controller.UpdateComment(comment);

            var result = response.Result as NotFoundObjectResult;

            Assert.NotNull(result);
            Assert.Equal((int)HttpStatusCode.NotFound, result.StatusCode);
        }

        [Fact]
        public async Task ShouldReturn500Updated()
        {
            _fixture.ResetRepositories();
            _fixture.CommentsRepository.GetCommentById(Arg.Any<long>()).ThrowsAsync(new Exception("Error"));

            var comment = new CommentEntity { Id = 1 };

            var controller = new CommentController(_fixture.CommentsRepository, _fixture.RunRepository);

            var response = await controller.UpdateComment(comment);

            var result = response.Result as StatusCodeResult;

            Assert.NotNull(result);
            Assert.Equal((int)HttpStatusCode.InternalServerError, result.StatusCode);
        }

        [Fact]
        public async Task ShouldReturn200Deleted()
        {
            _fixture.ResetRepositories();
            var comment = new CommentEntity { Id = 1 };

            _fixture.CommentsRepository.GetCommentById(Arg.Any<long>()).Returns(comment);

            var controller = new CommentController(_fixture.CommentsRepository, _fixture.RunRepository);

            var response = await controller.DeleteComment(1);

            var result = response as OkObjectResult;

            Assert.NotNull(result);
            Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);
        }

        [Fact]
        public async Task ShouldReturn404Deleted()
        {
            _fixture.ResetRepositories();
            var controller = new CommentController(_fixture.CommentsRepository, _fixture.RunRepository);

            var response = await controller.DeleteComment(1);

            var result = response as NotFoundObjectResult;

            Assert.NotNull(result);
            Assert.Equal((int)HttpStatusCode.NotFound, result.StatusCode);
        }

        [Fact]
        public async Task ShouldReturn500Deleted()
        {
            _fixture.ResetRepositories();
            _fixture.CommentsRepository.GetCommentById(Arg.Any<long>()).ThrowsAsync(new Exception("Error"));

            var controller = new CommentController(_fixture.CommentsRepository, _fixture.RunRepository);

            var response = await controller.DeleteComment(1);

            var result = response as StatusCodeResult;

            Assert.NotNull(result);
            Assert.Equal((int)HttpStatusCode.InternalServerError, result.StatusCode);
        }
    }
}
