using BlogApp.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogApp.Domain.Interfaces
{
    public interface ICommentRepository
    {
        Task<Comment> GetByIdAsync(int id);
        Task<IEnumerable<Comment>> GetCommentsByPostIdAsync(int postId);
        Task AddAsync(Comment comment);
        Task UpdateAsync(Comment comment);
        Task DeleteAsync(int id);
    }
}
