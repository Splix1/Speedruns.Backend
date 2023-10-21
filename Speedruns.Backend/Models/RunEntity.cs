namespace Speedruns.Backend.Models
{
    public class RunEntity : BaseEntity
    {
        public DateTime Date { get; set; } = DateTime.UtcNow;
        public long? UserId { get; set; }
        public long ConsoleId { get; set; }
        public long GameId { get; set; }
        public long Time {  get; set; }
        public string? UserName { get; set; }

        public UserEntity User { get; set; }
        public ConsoleEntity Console { get; set; }
        public GameEntity Game { get; set; }
        public List<CommentEntity> Comments { get; set; } = new List<CommentEntity>();
    }
}
