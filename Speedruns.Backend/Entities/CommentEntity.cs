namespace Speedruns.Backend.Entities
{
    public class CommentEntity : BaseEntity
    {
        public DateTime Date { get; set; } = DateTime.UtcNow;
        public string Text { get; set; }
        public long RunId { get; set; }
        public long UserId { get; set; }

        public RunEntity Run { get; set; }
        public UserEntity User { get; set; }
    }
}
