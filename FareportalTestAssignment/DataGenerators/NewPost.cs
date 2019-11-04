namespace FareportalTestAssignment.DataGenerators
{
    public class NewPost
    {
        public int userId { get; set; }
        public string title { get; set; }
        public string body { get; set; }


        public static NewPost GenerateObject()
        {
            NewPost post = new NewPost();

            post.userId = 10;
            post.title = "Test Post";
            post.body = Faker.Lorem.Paragraph(100); ;

            return post;
        }
    }
}
