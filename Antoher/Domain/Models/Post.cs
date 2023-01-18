namespace Antoher.Domain.Models
{
    public class Post
    {
        public int postId { get; set; }

        public string userId { get; set; }

        public string title { get; set; }

        public string plot { get; set; }

        public int likes { get; set; }
    }
}
