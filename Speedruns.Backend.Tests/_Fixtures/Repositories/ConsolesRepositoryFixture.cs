using Speedruns.Backend.Entities;
using Speedruns.Backend.Repositories;
using Speedruns.Backend.Tests.Database;

namespace Speedruns.Backend.Tests._Fixtures.Repositories
{
    public class ConsolesRepositoryFixture
    {
        private DbContextMock<ConsoleEntity> _dbContextMock;
        private ConsolesRepository _consoleRepository;

        public ConsolesRepositoryFixture()
        {
            _dbContextMock = new DbContextMock<ConsoleEntity>(new List<ConsoleEntity>
            {
                new ConsoleEntity { Id = 1, Name = "Console 1" },
                new ConsoleEntity { Id = 2, Name = "Console 2" },
            });
            _consoleRepository = new ConsolesRepository(_dbContextMock.Context);
        }

        internal ConsolesRepository Repository => _consoleRepository;
    }
}
