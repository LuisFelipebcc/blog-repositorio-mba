using BlogApp.Domain.Entities;
using System.Threading.Tasks;

namespace BlogApp.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetByIdAsync(string id);
        Task UpdateAsync(User user);
    }
}
