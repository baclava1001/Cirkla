using Cirkla_DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace Cirkla_DAL.Repositories.Users
{
    public class UserRepository : IUserRepository
    {
        // TODO: Create transactions?
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<User> Create(User user)
        {
            await _context.AddAsync(user);
            return user;
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await _context.Users
                .OrderBy(u => u.UserName)
                .ToListAsync();
        }

        public async Task<User> Get(string id)
        {
            return await _context.Users
                .FindAsync(id);
        }

        public async Task<User> Delete(User user)
        {
            _context.Users.Remove(user);
            return user;
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<User> Update(User user)
        {
            _context.Users.Update(user);
            return user;
        }
    }
}
