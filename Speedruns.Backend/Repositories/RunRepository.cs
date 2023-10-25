using Microsoft.EntityFrameworkCore;
using Speedruns.Backend.Interfaces;
using Speedruns.Backend.Entities;

namespace Speedruns.Backend.Repositories
{
    public class RunRepository : IRunRepository
    {
        private readonly SpeedrunsContext _context;
        private readonly DbSet<RunEntity> _runs;
        private readonly IGamesRepository _gamesRepository;

        public RunRepository(SpeedrunsContext context, IGamesRepository gameRepository)
        {
            _context = context;
            _runs = context.Set<RunEntity>();
            _gamesRepository = gameRepository;
        }

        public async Task<List<RunEntity>> GetAll()
        {
            return await _runs.Include(x => x.Game).ThenInclude(x => x.Series).Include(x => x.Console).ToListAsync();
        }

        public async Task<RunEntity> GetById(long id)
        {
            return await _runs.Where(x => x.Id == id).Include(x => x.Game).ThenInclude(x => x.Series).Include(x => x.Console).FirstOrDefaultAsync();
        }

        public async Task<List<RunEntity>> GetUserRuns(long? id)
        {
            return await _runs.Where(run => run.UserId == id).Include(x => x.Game).ThenInclude(x => x.Series).Include(x => x.Console).ToListAsync();
        }

        public async Task<RunEntity> CreateRun(RunEntity run)
        {

            var game = await _gamesRepository.GetById(run.GameId);
            if(game == null)
            {
                return null;
            }
            game.RunsPublished++;
            
            var userRuns = await GetUserRuns(run.UserId);
            var userGameRuns = userRuns.Where(x => x.GameId == run.GameId).ToList();

            if (!userGameRuns.Any())
            {
                game.Players++;
            }
            
            _runs.Add(run);

            await _context.SaveChangesAsync();

            return run;
        }

        public async Task<RunEntity> UpdateRun(RunEntity runToUpdate, RunEntity run)
        {
            
            runToUpdate.Date = run.Date;
            runToUpdate.Console = run.Console;

            await _context.SaveChangesAsync();
            return runToUpdate;
        }

        public async Task DeleteRun(RunEntity run)
        {
            var game = await _context.Games.FindAsync(run.GameId);
            game.RunsPublished--;

            var userRuns = await GetUserRuns(run.UserId);
            var userGameRuns = userRuns.Where(x => x.GameId == run.GameId).ToList();
            if (userGameRuns.Count < 2)
            {
                game.Players--;
            }

            _runs.Remove(run);
            await _context.SaveChangesAsync();
        }
    }
}
