using Microsoft.EntityFrameworkCore;

namespace Speedruns.Backend.Models
{
    public class SpeedrunsContext: DbContext
    {
        public SpeedrunsContext(DbContextOptions<SpeedrunsContext> options): base(options)
        {
        }

        public DbSet<UserModel> Users { get; set; }

    }
}
