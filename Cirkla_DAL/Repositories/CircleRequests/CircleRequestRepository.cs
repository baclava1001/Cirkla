using Cirkla_DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace Cirkla_DAL.Repositories.CircleRequests;

public class CircleRequestRepository(AppDbContext context) : ICircleRequestRepository
{
    public async Task<CircleRequest> Create(CircleRequest circleRequest)
    {
         await context.CircleRequests.AddAsync(circleRequest);
         return circleRequest;
    }

    public async Task<IEnumerable<CircleRequest>> GetAll()
    {
        return await context.CircleRequests
            .Include(cr => cr.Circle)
            .ThenInclude(c => c.Administrators)
            .Include(cr => cr.Circle)
            .ThenInclude(c => c.Members)
            .Include(cr => cr.PendingMember)
            .Include(cr => cr.FromUser)
            .Include(cr => cr.UpdatedByUser)
            .ToListAsync();
    }

    public async Task<IEnumerable<CircleRequest>> GetAllByCircleId(int circleId)
    {
        return await context.CircleRequests
            .Include(cr => cr.Circle)
            .ThenInclude(c => c.Administrators)
            .Include(cr => cr.Circle)
            .ThenInclude(c => c.Members)
            .Include(cr => cr.PendingMember)
            .Include(cr => cr.FromUser)
            .Include(cr => cr.UpdatedByUser)
            .Where(cr => cr.Circle.Id == circleId)
            .ToListAsync();
    }

    public async Task<IEnumerable<CircleRequest>> GetAllByPendingMemberId(string userId)
    {
        return await context.CircleRequests
            .Include(cr => cr.Circle)
            .ThenInclude(c => c.Administrators)
            .Include(cr => cr.Circle)
            .ThenInclude(c => c.Members)
            .Include(cr => cr.PendingMember)
            .Include(cr => cr.FromUser)
            .Include(cr => cr.UpdatedByUser)
            .Where(cr => cr.PendingMember.Id == userId)
            .ToListAsync();
    }

    public async Task<CircleRequest> GetById(int id)
    {
        return await context.CircleRequests
            .Include(cr => cr.Circle)
            .ThenInclude(c => c.Administrators)
            .Include(cr => cr.Circle)
            .ThenInclude(c => c.Members)
            .Include(cr => cr.FromUser)
            .Include(cr => cr.UpdatedByUser)
            .FirstOrDefaultAsync(cr => cr.Id == id);
    }

    public async Task<CircleRequest> Update(CircleRequest circleRequest)
    {
        context.CircleRequests.Attach(circleRequest);
        context.Entry(circleRequest).Property(cr => cr.Status).IsModified = true;
        context.Entry(circleRequest).Property(cr => cr.UpdatedAt).IsModified = true;
        context.Entry(circleRequest).Property(cr => cr.UpdatedByUserId).IsModified = true;
        return await Task.FromResult(circleRequest);
    }

    // No delete because requests should be kept for historical purposes. Old requests will be cleaned up in the background in later versions.

    public async Task SaveChanges()
    {
        await context.SaveChangesAsync();
    }
}