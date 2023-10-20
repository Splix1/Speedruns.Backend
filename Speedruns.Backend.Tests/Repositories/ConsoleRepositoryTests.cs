

using NSubstitute;
using Speedruns.Backend.Interfaces;
using Speedruns.Backend.Models;

namespace Speedruns.Backend.Tests.Repositories
{
    public class ConsoleRepositoryTests
    {
        [Fact]
        public async Task ShouldReturnListOfConsoles()
        {
            var repositoryMock = Substitute.For<IConsolesRepository>();

            repositoryMock.GetAll().Returns(new List<ConsoleModel>() { new ConsoleModel
            {
                Id = 1,
                Name = "DummyConsole",
            }
            });

            var consoles = await repositoryMock.GetAll();

            Assert.IsType<List<ConsoleModel>>(consoles);
        }
    }
}
