using System.Security.Cryptography.X509Certificates;
using Cirkla_DAL.Models.Enums;
using FluentValidation;
using Mapping.DTOs.CircleJoinRequests;

namespace Mapping.Validators.CircleJoinRequests;

public class CircleJoinRequestCreateValidator : AbstractValidator<CircleJoinRequestCreateDTO>
{
    public CircleJoinRequestCreateValidator()
    {
        RuleFor(x => x)
            .NotNull()
            .WithMessage("Attempted creating a new circle join request with null value");

        RuleFor(x => x.Type)
            .IsInEnum()
            .WithMessage("A join request needs to include a valid type");

        RuleFor(x => x.CircleId)
            .NotEmpty()
            .WithMessage("No circle ID was included in join request");

        RuleFor(x => x.CircleId)
            .GreaterThan(0)
            .WithMessage("No valid circle ID was included in join request");

        RuleFor(x => x.TargetUserId)
            .NotEmpty()
            .WithMessage("No target user ID was included in join request");

        RuleFor(x => x.FromUserId)
            .NotEmpty()
            .WithMessage("No inviting user ID was included in join request");

        RuleFor(x => x.Status)
            .IsInEnum()
            .WithMessage("No valid status was included in join request");

        RuleFor(x => x.Status)
            .Must(status => status == CircleRequestStatus.Pending)
            .WithMessage("New join requests must have status pending");

        RuleFor(x => x.RequestDate)
            .NotEmpty()
            .WithMessage("No request date was included in join request");

        RuleFor(x => x.RequestDate)
            .LessThanOrEqualTo(DateTime.Now)
            .WithMessage("Request date cannot be created in the future");

        RuleFor(x => x.ExpiresAt)
            .NotEmpty()
            .WithMessage("No expiration date was included in join request");

        RuleFor(x => x.ExpiresAt)
            .Must((dto, expires) => expires >= dto.RequestDate.AddDays(7))
            .WithMessage("Expiration date must be at least one week after the request date");
    }
}
