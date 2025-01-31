﻿using Cirkla_DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace Cirkla_DAL.Repositories.Items
{
    public class ItemRepository : IItemRepository
    {
        private readonly AppDbContext _context;

        public ItemRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Item> Create(Item item)
        {
            await _context.AddAsync(item);
            return item;
        }

        public async Task<IEnumerable<Item>> GetAll()
        {
            return await _context.Items
                .Include(i => i.Pictures)
                .OrderBy(i => i.Name)
                .ToListAsync();
        }

        public async Task<IEnumerable<Item>> GetAllByOwnerId(string ownerId)
        {
            return _context.Items
                .Include(i => i.Pictures)
                .Where(i => i.OwnerId == ownerId)
                .OrderBy(i => i.Name)
                .ToList();
        }

        public async Task<Item> Get(int id)
        {
            return await _context.Items
                .Include(i => i.Pictures)
                .Include(i => i.Owner)
                .FirstOrDefaultAsync(i => i.Id == id);
        }

        public Task<Item> Delete(Item item)
        {
            _context.Remove(item);
            return Task.FromResult(item);
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<Item> Update(Item item)
        {
            _context.Update(item);
            return await Task.FromResult(item);
        }
    }
}
