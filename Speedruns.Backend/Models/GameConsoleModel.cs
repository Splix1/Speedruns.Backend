namespace Speedruns.Backend.Models
{
    public class GameConsoleModel : BaseModel
    {
        public long GameId { get; set; }
        public long ConsoleId { get; set; }

        public GameModel Game { get; set; }
        public ConsoleModel Console { get; set; }
        public RunModel Run { get; set; }
    }
}
