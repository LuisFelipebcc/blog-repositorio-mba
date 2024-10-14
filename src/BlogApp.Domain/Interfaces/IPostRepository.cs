using BlogApp.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogApp.Domain.Interfaces
{
    public interface IPostRepository
    {
        Task<Post> GetByIdAsync(int id);
        Task<IEnumerable<Post>> GetAllAsync();
        Task AddAsync(Post post);
        Task UpdateAsync(Post post);
        Task DeleteAsync(int id);
    }
}
