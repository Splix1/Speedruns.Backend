using Microsoft.AspNetCore.Mvc;
using NSubstitute;
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

            commentsRepositoryMock.GetComments(Arg.Any<long>()).Returns(new List<CommentEntity>
            {
                new CommentEntity { Id = 1, Text = "Test 1" },
                new CommentEntity { Id = 2, Text = "Test 2" }
            });

            var controller = new CommentController(commentsRepositoryMock);

            var response = await controller.GetAll(1);

            var result = response.Result as OkObjectResult;

            Assert.NotNull(result);
            Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);
        }
    }
}
