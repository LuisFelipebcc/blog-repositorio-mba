using Microsoft.AspNetCore.Identity;

namespace BlogApp.Domain.Entities
{
    public class User : IdentityUser
    {
        public string FullName { get; set; }

        // Relacionamentos
        public virtual ICollection<Post> Posts { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
    }
}
