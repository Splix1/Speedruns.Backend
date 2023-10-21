using Microsoft.EntityFrameworkCore;

namespace Speedruns.Backend.Entities
{
    public class SpeedrunsContext: DbContext
    {
        public SpeedrunsContext(DbContextOptions<SpeedrunsContext> options): base(options)
        {
        }

        public DbSet<UserEntity> Users { get; set; }
        public DbSet<GameEntity> Games { get; set; }
        public DbSet<RunEntity> Runs { get; set; }
        public DbSet<CommentEntity> Comments { get; set; }
        public DbSet<ConsoleEntity> Consoles { get; set; }
        public DbSet<GameConsoleEntity> GameConsoles { get; set; }
        public DbSet<SeriesEntity> Series { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserEntity>()
                .HasMany(x => x.Runs)
                .WithOne(x => x.User)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
