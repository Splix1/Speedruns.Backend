using Speedruns.Backend.Entities;

namespace Speedruns.Backend.Interfaces
{
    public interface ISeriesRepository
    {
        public Task<List<SeriesEntity>> GetAll();
        public Task<SeriesEntity> GetById(long id);
        public Task<SeriesEntity> GetByName(string name);
    }
}
