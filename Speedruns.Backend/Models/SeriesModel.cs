namespace Speedruns.Backend.Models
{
    public class SeriesModel : BaseModel
    {
        public string Name { get; set; }
        public int Players { get; set; }

        public string? ImageUrl { get; set; }

        public List<GameModel> Games { get; set; } = new List<GameModel>();
    }
}
