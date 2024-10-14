using BlogApp.Domain.Entities;
using BlogApp.Domain.Interfaces;

namespace BlogApp.Services
{
    public class CommentService
    {
        private readonly ICommentRepository _commentRepository;

        public CommentService(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public async Task<IEnumerable<Comment>> GetCommentsByPostIdAsync(int postId)
        {
            return await _commentRepository.GetCommentsByPostIdAsync(postId);
        }

        public async Task<Comment> GetCommentByIdAsync(int id)
        {
            return await _commentRepository.GetByIdAsync(id);
        }

        public async Task AddCommentAsync(Comment comment)
        {
            await _commentRepository.AddAsync(comment);
        }

        public async Task UpdateCommentAsync(Comment comment)
        {
            await _commentRepository.UpdateAsync(comment);
        }

        public async Task DeleteCommentAsync(int id)
        {
            await _commentRepository.DeleteAsync(id);
        }
    }
}
