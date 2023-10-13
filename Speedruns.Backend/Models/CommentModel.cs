namespace Speedruns.Backend.Models
{
    public class CommentModel : BaseModel
    {
        public DateTime Date { get; set; } = DateTime.UtcNow;
        public string Text { get; set; }
        public long RunId { get; set; }
        public long UserId { get; set; }

        public RunModel Run { get; set; }
        public UserModel User { get; set; }
    }
}
