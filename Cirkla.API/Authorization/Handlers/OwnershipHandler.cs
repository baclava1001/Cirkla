using Cirkla_API.Authorization.Requirements;
using Cirkla_API.Common.Constants;
using Cirkla_DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Item = Cirkla.ApiClient.Item;

namespace Cirkla_API.Authorization.Handlers;

public class OwnershipHandler : AuthorizationHandler<OwnershipRequirement, Item>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
                                                    OwnershipRequirement requirement, 
                                                    Item recource)
    {
        var userId = context.User.FindFirst(CustomClaimTypes.UserId)?.Value;
        if (recource.OwnerId == userId)
        {
            context.Succeed(requirement);
        }
        return Task.CompletedTask;
    }
}