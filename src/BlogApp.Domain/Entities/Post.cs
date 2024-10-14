using System;
using System.Collections.Generic;

namespace BlogApp.Domain.Entities
{
    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime PublishedDate { get; set; }

        // FK para o autor (usuário que criou o post)
        public string AuthorId { get; set; }
        public virtual User Author { get; set; }

        // Relacionamento com comentários
        public virtual ICollection<Comment> Comments { get; set; }
    }
}
