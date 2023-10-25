using Microsoft.EntityFrameworkCore;
using Speedruns.Backend.Interfaces;
using Speedruns.Backend.Entities;

namespace Speedruns.Backend.Repositories
{
    public class SeriesRepository : ISeriesRepository
    {
        private readonly SpeedrunsContext _context;
        private readonly DbSet<SeriesEntity> _series;

        public SeriesRepository(SpeedrunsContext context)
        {
            _context = context;
            _series = _context.Set<SeriesEntity>();
        }

        public async Task<List<SeriesEntity>> GetAll()
        {
            return await _series.Include(x => x.Games).ThenInclude(x => x.GameConsoles).ThenInclude(x => x.Console).ToListAsync();
        }

        public async Task<SeriesEntity> GetById(long id)
        {
            return await _series.Include(x => x.Games).ThenInclude(x => x.GameConsoles).ThenInclude(x => x.Console).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<SeriesEntity> GetByName(string name)
        {
            return await _series.Include(x => x.Games).ThenInclude(x => x.GameConsoles).ThenInclude(x => x.Console).FirstOrDefaultAsync(x => x.Name == name);
        }
    }
}
