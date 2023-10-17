﻿using Microsoft.EntityFrameworkCore;
using Speedruns.Backend.Interfaces;
using Speedruns.Backend.Models;

namespace Speedruns.Backend.Repositories
{
    public class RunRepository : IRunRepository
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
            return await _runs.Include(x => x.Game).ThenInclude(x => x.Series).Include(x => x.Console).ToListAsync();
        }

        public async Task<RunModel> GetById(long id)
        {
            return await _runs.Where(x => x.Id == id).Include(x => x.Game).ThenInclude(x => x.Series).Include(x => x.Console).FirstOrDefaultAsync();
        }

        public async Task<List<RunModel>> GetUserRuns(long? id)
        {
            return await _runs.Where(run => run.UserId == id).Include(x => x.Game).ThenInclude(x => x.Series).Include(x => x.Console).ToListAsync();
        }

        public async Task<RunModel> CreateRun(RunModel run)
        {
            var game = await _context.Games.FindAsync(run.GameId);
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

        public async Task<RunModel> UpdateRun(RunModel runToUpdate, RunModel run)
        {
            
            runToUpdate.Date = run.Date;
            runToUpdate.Console = run.Console;

            await _context.SaveChangesAsync();
            return runToUpdate;
        }

        public async Task DeleteRun(RunModel run)
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
