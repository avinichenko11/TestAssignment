using System.Collections.Generic;

namespace FareportalTestAssignment.Responses
{
    public class PostsResponse
    {
        public List<Post> PostsList { get; set; }
    }

    public class Post
    {
        public int userId { get; set; }
        public int id { get; set; }
        public string title { get; set; }
        public string body { get; set; }
    }
}