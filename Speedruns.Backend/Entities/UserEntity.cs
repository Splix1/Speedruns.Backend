namespace Speedruns.Backend.Entities
{
    public class UserEntity : BaseEntity
    {
        public string UserName { get; set; }
        public string ImageUrl { get; set; }
        public string YoutubeLink { get; set; }
        public string TwitchLink { get; set; }
        
        public List<RunEntity> Runs { get; set; } = new List<RunEntity>();
    }
}
