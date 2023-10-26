using Speedruns.Backend.Entities;
using Speedruns.Backend.Repositories;
using Speedruns.Backend.Tests.Database;

namespace Speedruns.Backend.Tests.Repositories
{
    public class SeriesRepositoryTests
    {
        private DbContextMock<SeriesEntity> _dbContextMock;
        private SeriesRepository _seriesRepository;

        public SeriesRepositoryTests()
        {
            _dbContextMock = new DbContextMock<SeriesEntity>(new List<SeriesEntity>
            {
                new SeriesEntity { Id = 1, Name = "Series 1", Players = 100 },
                new SeriesEntity { Id = 2, Name = "Series 2", Players = 200 }
            });

            _seriesRepository = new SeriesRepository(_dbContextMock.Context);
        }

        [Fact]
        public async Task ShouldReturnListOfSeries()
        {
            var series = await _seriesRepository.GetAll();

            Assert.NotNull(series);
            Assert.IsType<List<SeriesEntity>>(series);
            Assert.Equal(2, series.Count);
            Assert.Equal("Series 1", series.First().Name);
            Assert.Equal("Series 2", series.Last().Name);
        }

        [Fact]
        public async Task ShouldReturnSeriesById()
        {
            var series = await _seriesRepository.GetById(1);

            Assert.NotNull(series);
            Assert.IsType<SeriesEntity>(series);
            Assert.Equal("Series 1", series.Name);
            Assert.Equal(100, series.Players);
        }

        [Fact]
        public async Task ShouldReturnSeriesByName()
        {
            var series = await _seriesRepository.GetByName("Series 1");

            Assert.NotNull(series);
            Assert.IsType<SeriesEntity>(series);
            Assert.Equal("Series 1", series.Name);
        }
    }
}
