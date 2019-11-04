
using System.Collections.Generic;

namespace FareportalTestAssignment.Responses
{
    public class CommentResponse
    {
        public List<Comment> CommentsList { get; set; }
    }

    public class Comment
    {
        public int postId { get; set; }
        public int id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string body { get; set; }
    }
}
