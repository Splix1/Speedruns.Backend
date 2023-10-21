namespace Speedruns.Backend.Models
{
    public class GameConsoleEntity : BaseEntity
    {
        public long GameId { get; set; }
        public long ConsoleId { get; set; }

        public GameEntity Game { get; set; }
        public ConsoleEntity Console { get; set; }
    }
}
