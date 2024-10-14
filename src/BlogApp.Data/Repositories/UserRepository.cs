using BlogApp.Data.Data;
using BlogApp.Domain.Entities;
using BlogApp.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<User> GetByIdAsync(string id)
        {
            return await _context.Users.Include(u => u.Posts)
                                       .Include(u => u.Comments)
                                       .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task UpdateAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }
    }
}
