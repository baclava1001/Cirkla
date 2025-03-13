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

    public async Task<IEnumerable<Circle>> GetAll()
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
            .Where(c => c.Id == id)
            .Include(c => c.Administrators)
            .Include(c => c.Members)
            .Include(c => c.CreatedBy)
            .Include(c => c.UpdatedBy)
            .FirstOrDefaultAsync();
    }

    public async Task<Circle> Update(Circle circle)
    {
        context.Circles.Attach(circle);
        context.Entry(circle).Property(c => c.Name).IsModified = true;
        context.Entry(circle).Property(c => c.Description).IsModified = true;
        context.Entry(circle).Property(c => c.IsPublic).IsModified = true;
        context.Entry(circle).Property(c => c.UpdatedAt).IsModified = true;
        context.Entry(circle).Property(c => c.UpdatedById).IsModified = true;
        return await Task.FromResult(circle);
    }

    public async Task<Circle> UpdateMembers(Circle circle)
    {
        context.Circles.Attach(circle);
        context.Entry(circle).Collection(c => c.Members).IsModified = true;
        return await Task.FromResult(circle);
    }

    public async Task<Circle> UpdateAdministrators(Circle circle)
    {
        context.Circles.Attach(circle);
        context.Entry(circle).Collection(c => c.Administrators).IsModified = true;
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
}