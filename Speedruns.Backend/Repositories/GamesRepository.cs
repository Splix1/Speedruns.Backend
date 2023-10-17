using Microsoft.EntityFrameworkCore;
using Speedruns.Backend.Interfaces;
using Speedruns.Backend.Models;

namespace Speedruns.Backend.Repositories
{
    public class GamesRepository : IGamesRepository
    {
        private readonly DbSet<GameModel> _games;

        public GamesRepository(SpeedrunsContext context)
        {
            _games = context.Games;
        }

        public async Task<List<GameModel>> GetAll()
        {
            return await _games.Include(x => x.Series).Include(x => x.GameConsoles).ThenInclude(x => x.Console).ToListAsync();
        }

        public async Task<GameModel> GetById(long id)
        {
            return await _games.Where(x => x.Id == id).Include(x => x.Series).Include(x => x.GameConsoles).ThenInclude(x => x.Console).FirstOrDefaultAsync();
        }

        public async Task<GameModel> GetByName(string name)
        {
            return await _games.Include(x => x.Series).Include(x => x.GameConsoles).ThenInclude(x => x.Console).FirstOrDefaultAsync(x => x.Name == name);
        }

    }
}
