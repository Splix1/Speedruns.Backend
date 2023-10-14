namespace Speedruns.Backend.Models
{
    public class RunModel : BaseModel
    {
        public DateTime Date { get; set; } = DateTime.UtcNow;
        public long UserId { get; set; }
        public long ConsoleId { get; set; }
        public long GameId { get; set; }
        public long CommentId { get; set; }

        public UserModel User { get; set; }
        public ConsoleModel Console { get; set; }
        public GameModel Game { get; set; }
        public List<CommentModel> Comments { get; set; } = new List<CommentModel>();
    }
}
