using System;

namespace BlogApp.Domain.Entities
{
    public class Comment
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }

        // FK para o post relacionado
        public int PostId { get; set; }
        public virtual Post Post { get; set; }

        // FK para o autor do comentário (usuário que fez o comentário)
        public string AuthorId { get; set; }
        public virtual User Author { get; set; }
    }
}
