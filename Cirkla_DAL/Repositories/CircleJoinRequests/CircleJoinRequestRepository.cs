using Cirkla_DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace Cirkla_DAL.Repositories.CircleJoinRequests;

public class CircleJoinRequestRepository(AppDbContext context) : ICircleJoinRequestRepository
{
    public async Task<CircleJoinRequest?> Create(CircleJoinRequest circleRequest)
    {
         await context.CircleJoinRequests.AddAsync(circleRequest);
         return circleRequest;
    }

    public async Task<IEnumerable<CircleJoinRequest?>> GetAll()
    {
        return await context.CircleJoinRequests
            .Include(cr => cr.Circle)
            .ThenInclude(c => c.Administrators)
            .Include(cr => cr.Circle)
            .ThenInclude(c => c.Members)
            .Include(cr => cr.TargetUser)
            .ThenInclude(pm => pm.Items)
            .Include(cr => cr.FromUser)
            .Include(cr => cr.UpdatedByUser)
            .ToListAsync();
    }

    public async Task<IEnumerable<CircleJoinRequest?>> GetAllByCircleId(int circleId)
    {
        return await context.CircleJoinRequests
            .Where(cr => cr.Circle.Id == circleId)
            .Include(cr => cr.Circle)
            .ThenInclude(c => c.Administrators)
            .Include(cr => cr.Circle)
            .ThenInclude(c => c.Members)
            .Include(cr => cr.TargetUser)
            .ThenInclude(pm => pm.Items)
            .Include(cr => cr.FromUser)
            .Include(cr => cr.UpdatedByUser)
            .ToListAsync();
    }

    public async Task<IEnumerable<CircleJoinRequest?>> GetAllByTargetUserId(string userId)
    {
        return await context.CircleJoinRequests
            .Where(cr => cr.TargetUser.Id == userId)
            .Include(cr => cr.Circle)
            .ThenInclude(c => c.Administrators)
            .Include(cr => cr.Circle)
            .ThenInclude(c => c.Members)
            .Include(cr => cr.TargetUser)
            .ThenInclude(pm => pm.Items)
            .Include(cr => cr.FromUser)
            .Include(cr => cr.UpdatedByUser)
            .ToListAsync();
    }

    public async Task<IEnumerable<CircleJoinRequest?>> GetByTargetUserAndCircle(string targetUserId, int circleId)
    {
        return await context.CircleJoinRequests
            .Include(cr => cr.Circle)
            .Where(c => c.Circle.Id == circleId)
            .Include(cr => cr.Circle)
            .ThenInclude(c => c.Members)
            .Include(cr => cr.Circle)
            .ThenInclude(cr => cr.Administrators)
            .Include(cr => cr.FromUser)
            .Include(cr => cr.TargetUser)
            .Where(cr => cr.TargetUser.Id == targetUserId)
            .ToListAsync();
    }

    public async Task<CircleJoinRequest?> GetById(int id)
    {
        return await context.CircleJoinRequests
            .Include(cr => cr.Circle)
            .ThenInclude(c => c.Administrators)
            .Include(cr => cr.Circle)
            .ThenInclude(c => c.Members)
            .Include(cr => cr.TargetUser)
            .Include(cr => cr.FromUser)
            .Include(cr => cr.UpdatedByUser)
            .FirstOrDefaultAsync(cr => cr.Id == id);
    }

    public async Task<CircleJoinRequest?> Update(CircleJoinRequest circleRequest)
    {
        context.CircleJoinRequests.Attach(circleRequest);
        context.Entry(circleRequest).Property(cr => cr.Status).IsModified = true;
        context.Entry(circleRequest).Property(cr => cr.UpdatedAt).IsModified = true;
        context.Entry(circleRequest).Property(cr => cr.UpdatedByUserId).IsModified = true;
        return await Task.FromResult(circleRequest);
    }

    // TODO: No delete because requests should be kept for historical purposes. Old requests will be cleaned up in the background in later versions.
}