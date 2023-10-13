namespace Speedruns.Backend.Models
{
    public class UserModel : BaseModel
    {
        public string UserName { get; set; }
        public string ImageUrl { get; set; }
        public string YoutubeLink { get; set; }
        public string TwitchLink { get; set; }
        
        public RunModel Runs { get; set; }
    }
}
