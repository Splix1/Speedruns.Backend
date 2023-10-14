namespace Speedruns.Backend.Models
{
    public class GameModel : BaseModel
    {
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public int ReleaseYear { get; set; }
        public string Series {  get; set; }
        public int Players { get; set; }
        public int RunsPublished { get; set; }

        public RunModel Run { get; set; }
      
    }
}
