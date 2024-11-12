namespace BlogApp.Core.Entities
{
    public class Comment
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime CommentedDate { get; set; } = DateTime.UtcNow;

        public int PostId { get; set; }
        public Post Post { get; set; }
        public string AuthorId { get; set; }
        public Author Author { get; set; }
    }
}
