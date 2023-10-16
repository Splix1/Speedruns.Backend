using Microsoft.EntityFrameworkCore;
using Speedruns.Backend.Interfaces;
using Speedruns.Backend.Models;

namespace Speedruns.Backend.Repositories
{
    public class ConsolesRepository : IConsolesRepository
    {
        private readonly DbSet<ConsoleModel> _consoles;
        public ConsolesRepository(SpeedrunsContext context)
        {
            _consoles = context.Consoles;
        }
        public async Task<List<ConsoleModel>> GetAll()
        {
            return await _consoles.ToListAsync();
        }
    }
}
