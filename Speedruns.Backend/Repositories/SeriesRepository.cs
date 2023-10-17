using Microsoft.EntityFrameworkCore;
using Speedruns.Backend.Interfaces;
using Speedruns.Backend.Models;

namespace Speedruns.Backend.Repositories
{
    public class SeriesRepository : ISeriesRepository
    {
        private readonly SpeedrunsContext _context;
        private readonly DbSet<SeriesModel> _series;

        public SeriesRepository(SpeedrunsContext context)
        {
            _context = context;
            _series = _context.Series;
        }

        public async Task<List<SeriesModel>> GetAll()
        {
            return await _series.Include(x => x.Games).ThenInclude(x => x.GameConsoles).ThenInclude(x => x.Console).ToListAsync();
        }

        public async Task<SeriesModel> GetById(long id)
        {
            return await _series.Include(x => x.Games).ThenInclude(x => x.GameConsoles).ThenInclude(x => x.Console).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<SeriesModel> GetByName(string name)
        {
            return await _series.Include(x => x.Games).ThenInclude(x => x.GameConsoles).ThenInclude(x => x.Console).FirstOrDefaultAsync(x => x.Name == name);
        }
    }
}
