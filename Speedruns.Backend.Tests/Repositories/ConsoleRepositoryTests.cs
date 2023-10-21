using Speedruns.Backend.Entities;
using Speedruns.Backend.Tests.Database;
using Speedruns.Backend.Repositories;

namespace Speedruns.Backend.Tests.Repositories
{
    public class ConsoleRepositoryTests
    {
        private DbContextMock<ConsoleEntity> _dbContextMock;
        private ConsolesRepository _consoleRepository;

        public ConsoleRepositoryTests()
        {
            _dbContextMock = new DbContextMock<ConsoleEntity>(new List<ConsoleEntity>
        {
            new ConsoleEntity { Id = 1, Name = "Console 1" },
            new ConsoleEntity { Id = 2, Name = "Console 2" },
        });
            _consoleRepository = new ConsolesRepository(_dbContextMock.Context);
        }

        [Fact]
        public async Task ShouldReturnListOfConsoles()
        {
            var consoles = await _consoleRepository.GetAll();

            Assert.Equal(2, consoles.Count);
        }
    }

}
