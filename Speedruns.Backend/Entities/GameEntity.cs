namespace Speedruns.Backend.Entities
{
    public class GameEntity : BaseEntity
    {
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public int ReleaseYear { get; set; }
        public int Players { get; set; }
        public int RunsPublished { get; set; }
        public long? SeriesId { get; set; }

        public List<GameConsoleEntity> GameConsoles { get; set; } = new List<GameConsoleEntity>();
        public List<RunEntity> Runs { get; set; } = new List<RunEntity>();
        public SeriesEntity Series { get; set; }
    }
}
