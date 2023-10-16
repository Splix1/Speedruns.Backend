using Speedruns.Backend.Models;

namespace Speedruns.Backend.Interfaces
{
    public interface IGamesRepository
    {
        public Task<List<GameModel>> GetAll();
        public Task<GameModel> GetById(long id);
        public Task<GameModel> GetByName(string name);
    }
}
