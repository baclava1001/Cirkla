using Cirkla.Shared.DTOs.CircleJoinRequests;
using FluentValidation;

namespace Cirkla.Shared.Validators.CircleJoinRequests
{
    public class CircleJoinRequestUpdateValidator : AbstractValidator<CircleJoinRequestUpdateDTO>
    {
        public CircleJoinRequestUpdateValidator()
        {
            RuleFor(x => x)
                .NotNull()
                .WithMessage("Attempted updating a circle join request with null value");

            RuleFor(x => x.CircleId)
                .NotEmpty()
                .WithMessage("No circle ID was included in join request");

            RuleFor(x => x.CircleId)
                .GreaterThan(0)
                .WithMessage("No valid circle ID was included in join request");

            RuleFor(x => x.UpdatedByUserId)
                .NotEmpty()
                .WithMessage("No target user ID was included in join request");
        }
    }

}
