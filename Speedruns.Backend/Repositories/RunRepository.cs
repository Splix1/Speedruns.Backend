using Microsoft.EntityFrameworkCore;
using Speedruns.Backend.Interfaces;
using Speedruns.Backend.Models;

namespace Speedruns.Backend.Repositories
{
    public class RunRepository
    {
        private readonly SpeedrunsContext _context;
        private readonly DbSet<RunModel> _runs;

        public RunRepository(SpeedrunsContext context)
        {
            _context = context;
            _runs = context.Runs;
        }

        public async Task<List<RunModel>> GetAll()
        {
            return await _runs.ToListAsync();
        }

        public async Task<RunModel> GetById(long id)
        {
            return await _runs.FindAsync(id);
        }

        public async Task CreateRun(RunModel run)
        {
            _runs.Add(run);
            await _context.SaveChangesAsync();
        }

        
    }
}
