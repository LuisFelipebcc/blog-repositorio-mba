namespace BlogApp.Core.Entities
{
    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime PublishedDate { get; set; } = DateTime.UtcNow;

        public string AuthorId { get; set; }
        public Author Author { get; set; }
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    }
}