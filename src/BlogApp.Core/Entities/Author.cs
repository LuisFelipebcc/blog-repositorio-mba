using Microsoft.AspNetCore.Identity;

namespace BlogApp.Core.Entities
{
    public class Author : IdentityUser
    {
        public ICollection<Post> Posts { get; set; } = new List<Post>();
    }
}
