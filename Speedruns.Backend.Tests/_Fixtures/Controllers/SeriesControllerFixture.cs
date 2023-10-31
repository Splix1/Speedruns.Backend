using NSubstitute;
using Speedruns.Backend.Interfaces;

namespace Speedruns.Backend.Tests._Fixtures.Controllers
{
    public class SeriesControllerFixture
    {
        private ISeriesRepository _seriesRepository;

        public SeriesControllerFixture()
        {
            _seriesRepository = Substitute.For<ISeriesRepository>();
        }

        internal ISeriesRepository Repository => _seriesRepository;

        public void ResetSubstitutes()
        {
            _seriesRepository = Substitute.For<ISeriesRepository>();
        }
    }
}
