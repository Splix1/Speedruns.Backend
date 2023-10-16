namespace Speedruns.Backend.Models
{
    public class ConsoleModel : BaseModel
    {
        public string Name { get; set; }
        public long ConsoleId { get; set; }

        public List<GameConsoleModel> GameConsoles { get; set; } = new List<GameConsoleModel>();
        public List<RunModel> Runs { get; set; } = new List<RunModel>();
    }
}
