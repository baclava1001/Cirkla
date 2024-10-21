using Cirkla_DAL;
using Cirkla_DAL.Models.Users;
using Microsoft.EntityFrameworkCore;

namespace Cirkla_API.Repositories
{
    public class UserRepository : IUserRepository
    {
        // TODO: Add transactions
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<User> Add(User user)
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

        public async Task<User> Remove(User user)
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
