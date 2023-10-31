using Speedruns.Backend.Entities;
using Speedruns.Backend.Tests._Fixtures.Repositories;

namespace Speedruns.Backend.Tests.Repositories
{
    public class SeriesRepositoryTests : IClassFixture<SeriesRepositoryFixture>
    {
        private readonly SeriesRepositoryFixture _fixture;

        public SeriesRepositoryTests(SeriesRepositoryFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task ShouldReturnListOfSeries()
        {
            var series = await _fixture.Repository.GetAll();

            Assert.NotNull(series);
            Assert.IsType<List<SeriesEntity>>(series);
            Assert.Equal(2, series.Count);
            Assert.Equal("Series 1", series.First().Name);
            Assert.Equal("Series 2", series.Last().Name);
        }

        [Fact]
        public async Task ShouldReturnSeriesById()
        {
            var series = await _fixture.Repository.GetById(1);

            Assert.NotNull(series);
            Assert.IsType<SeriesEntity>(series);
            Assert.Equal("Series 1", series.Name);
            Assert.Equal(100, series.Players);
        }

        [Fact]
        public async Task ShouldReturnSeriesByName()
        {
            var series = await _fixture.Repository.GetByName("Series 1");

            Assert.NotNull(series);
            Assert.IsType<SeriesEntity>(series);
            Assert.Equal("Series 1", series.Name);
        }
    }
}
