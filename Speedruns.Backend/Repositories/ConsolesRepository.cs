using Microsoft.EntityFrameworkCore;
using Speedruns.Backend.Interfaces;
using Speedruns.Backend.Entities;

namespace Speedruns.Backend.Repositories
{
    public class ConsolesRepository : IConsolesRepository
    {
        private readonly DbSet<ConsoleEntity> _consoles;
        public ConsolesRepository(SpeedrunsContext context)
        {
            _consoles = context.Set<ConsoleEntity>();
        }
        public async Task<List<ConsoleEntity>> GetAll()
        {
            return await _consoles.ToListAsync();
        }
    }
}
