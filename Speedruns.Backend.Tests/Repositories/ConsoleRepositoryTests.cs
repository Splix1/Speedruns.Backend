using Speedruns.Backend.Entities;
using Speedruns.Backend.Tests._Fixtures.Repositories;

namespace Speedruns.Backend.Tests.Repositories
{
    public class ConsoleRepositoryTests : IClassFixture<ConsolesRepositoryFixture>
    {
        private readonly ConsolesRepositoryFixture _fixture;

        public ConsoleRepositoryTests(ConsolesRepositoryFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task ShouldReturnListOfConsoles()
        {
            var consoles = await _fixture.Repository.GetAll();

            Assert.NotNull(consoles);
            Assert.IsType<List<ConsoleEntity>>(consoles);
            Assert.Equal(2, consoles.Count);
        }
    }

}
