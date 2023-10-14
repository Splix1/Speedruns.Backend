namespace Speedruns.Backend.Models
{
    public class ConsoleModel : BaseModel
    {
        public string Name { get; set; }

        public List<RunModel> Runs { get; set; } = new List<RunModel>();
    }
}
