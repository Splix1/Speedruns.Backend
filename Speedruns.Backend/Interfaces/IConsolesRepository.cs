using Speedruns.Backend.Models;

namespace Speedruns.Backend.Interfaces
{
    public interface IConsolesRepository
    {
        public Task<List<ConsoleEntity>> GetAll();
    }
}
