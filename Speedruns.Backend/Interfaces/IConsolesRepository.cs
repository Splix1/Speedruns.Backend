using Speedruns.Backend.Entities;

namespace Speedruns.Backend.Interfaces
{
    public interface IConsolesRepository
    {
        public Task<List<ConsoleEntity>> GetAll();
    }
}
