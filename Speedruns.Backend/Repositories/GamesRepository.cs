using Microsoft.EntityFrameworkCore;
using Speedruns.Backend.Interfaces;
using Speedruns.Backend.Models;

namespace Speedruns.Backend.Repositories
{
    public class GamesRepository
    {
        private readonly SpeedrunsContext _context;
        private readonly DbSet<GameModel> _games;

        public GamesRepository(SpeedrunsContext context)
        {
            _context = context;
            _games = context.Games;
        }

       
    }
}
