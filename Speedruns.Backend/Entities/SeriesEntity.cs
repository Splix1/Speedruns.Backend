namespace Speedruns.Backend.Entities
{
    public class SeriesEntity : BaseEntity
    {
        public string Name { get; set; }
        public int Players { get; set; }

        public string? ImageUrl { get; set; }

        public List<GameEntity> Games { get; set; } = new List<GameEntity>();
    }
}
