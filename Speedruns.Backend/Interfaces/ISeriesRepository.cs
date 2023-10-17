using Speedruns.Backend.Models;

namespace Speedruns.Backend.Interfaces
{
    public interface ISeriesRepository
    {
        public Task<List<SeriesModel>> GetAll();
        public Task<SeriesModel> GetById(long id);
        public Task<SeriesModel> GetByName(string name);
    }
}
