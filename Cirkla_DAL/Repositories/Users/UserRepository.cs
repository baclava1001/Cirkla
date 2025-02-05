using Cirkla_DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace Cirkla_DAL.Repositories.Users
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

        public async Task SaveChanges()
        {
            await context.SaveChangesAsync();
        }

        public async Task<User> Update(User user)
        {
            context.Users.Update(user);
            return user;
        }
    }
}
