using Speedruns.Backend.Entities;
using Speedruns.Backend.Repositories;
using Speedruns.Backend.Tests.Database;

namespace Speedruns.Backend.Tests._Fixtures.Repositories
{
    public class SeriesRepositoryFixture
    {
        private DbContextMock<SeriesEntity> _dbContextMock;
        private SeriesRepository _seriesRepository;

        public SeriesRepositoryFixture()
        {
            _dbContextMock = new DbContextMock<SeriesEntity>(new List<SeriesEntity>
            {
                new SeriesEntity { Id = 1, Name = "Series 1", Players = 100 },
                new SeriesEntity { Id = 2, Name = "Series 2", Players = 200 }
            });

            _seriesRepository = new SeriesRepository(_dbContextMock.Context);
        }

        internal SeriesRepository Repository => _seriesRepository;
    }
}
