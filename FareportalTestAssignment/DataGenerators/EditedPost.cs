namespace FareportalTestAssignment.DataGenerators
{
    public class EditedPost
    {
        public int userId { get; set; }
        public int id { get; set; }
        public string title { get; set; }
        public string body { get; set; }


        public static EditedPost GenerateObject(int id)
        {
            EditedPost post = new EditedPost();

            post.id = id;
            post.userId = 10;
            post.title = "Edited Post";
            post.body = Faker.Lorem.Paragraph(50);

            return post;
        }
    }
}
