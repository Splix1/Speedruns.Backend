using Speedruns.Backend.Entities;

namespace Speedruns.Backend.Interfaces
{
    public interface IGamesRepository
    {
        public Task<List<GameEntity>> GetAll();
        public Task<GameEntity> GetById(long id);
        public Task<GameEntity> GetByName(string name);
    }
}
