using Speedruns.Backend.Controllers;
using NSubstitute;
using Speedruns.Backend.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Speedruns.Backend.Tests
{
    public class ConsoleControllerTests
    {
        [Fact]
        public async Task ShouldReturn200()
        {
            // create mock of repository
            var repositoryMock = Substitute.For<IConsolesRepository>();

            // create new instance of controller using mock repository
            var controller = new ConsoleController(repositoryMock);

            // make request using controller
            var response = await controller.GetAll();

            // store result in a variable to test
            var result = response.Result as OkObjectResult;

            Assert.NotNull(result);
            Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);
        }
    }
}
