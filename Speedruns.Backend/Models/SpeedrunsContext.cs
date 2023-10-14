using Microsoft.EntityFrameworkCore;

namespace Speedruns.Backend.Models
{
    public class SpeedrunsContext: DbContext
    {
        public SpeedrunsContext(DbContextOptions<SpeedrunsContext> options): base(options)
        {
        }

        public DbSet<UserModel> Users { get; set; }
        public DbSet<GameModel> Games { get; set; }
        public DbSet<RunModel> Runs { get; set; }
        public DbSet<CommentModel> Comments { get; set; }
        public DbSet<ConsoleModel> Consoles { get; set; }
        public DbSet<GameConsoleModel> GameConsoles { get; set; }

    }
}
