namespace Speedruns.Backend.Models
{
    public class ConsoleEntity : BaseEntity
    {
        public string Name { get; set; }
        public long ConsoleId { get; set; }

        public List<GameConsoleEntity> GameConsoles { get; set; } = new List<GameConsoleEntity>();
        public List<RunEntity> Runs { get; set; } = new List<RunEntity>();
    }
}
