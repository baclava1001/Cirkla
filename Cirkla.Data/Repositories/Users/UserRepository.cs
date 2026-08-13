using Cirkla.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Cirkla.Data.Repositories.Users
{
    public class UserRepository(AppDbContext context) : IUserRepository
    {
        public async Task<User> Create(User user)
        {
            await context.AddAsync(user);
            return user;
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await context.Users
                .OrderBy(u => u.UserName)
                .ToListAsync();
        }

        public async Task<User> Get(string id)
        {
            return await context.Users
                .FindAsync(id);
        }

        public async Task<User> Delete(User user)
        {
            context.Users.Remove(user);
            return user;
        }

        public async Task<User> Update(User user)
        {
            context.Users.Update(user);
            return user;
        }
    }
}
