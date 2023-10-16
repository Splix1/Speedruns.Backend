using Speedruns.Backend.Interfaces;
using Speedruns.Backend.Models;

namespace Speedruns.Backend.Repositories
{
    public class ConsolesRepository : IConsolesRepository
    {
        private readonly IConsolesRepository _consoles;
        public ConsolesRepository(IConsolesRepository consoles)
        {
            _consoles = consoles;
        }

        public async Task<List<ConsoleModel>> GetAll()
        {
            return await _consoles.GetAll();
        }
    }
}
