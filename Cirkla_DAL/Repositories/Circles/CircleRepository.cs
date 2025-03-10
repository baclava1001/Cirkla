using Cirkla_DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace Cirkla_DAL.Repositories.Circles;

public class CircleRepository(AppDbContext context) : ICircleRepository
{
    public async Task<Circle> Create(Circle circle)
    {
        await context.AddAsync(circle);
        return circle;
    }

    public async Task<IEnumerable<Circle>> Getall()
    {
        return await context.Circles
            .Include(c => c.Administrators)
            .Include(c => c.Members)
            .Include(c => c.CreatedBy)
            .Include(c => c.UpdatedBy)
            .ToListAsync();
    }

    public async Task<Circle?> GetById(int id)
    {
        return await context.Circles
            .Include(c => c.Administrators)
            .Include(c => c.Members)
            .Include(c => c.CreatedBy)
            .Include(c => c.UpdatedBy)
            .FirstOrDefaultAsync();
    }

    public async Task<Circle> Update(Circle circle)
    {
        context.Update(circle);
        return await Task.FromResult(circle); 
    }

    public async Task<Circle> Delete(Circle circle)
    {
        context.Remove(circle);
        return await Task.FromResult(circle);
    }

    public async Task SaveChanges()
    {
        await context.SaveChangesAsync();
    }


    // Special, more specific, queries and commands
}