using FluentValidation;
using Mapping.DTOs.Circles;

namespace Mapping.Validators.Circles;

public class CircleCreateValidator : AbstractValidator<CircleCreateDTO>
{
    public CircleCreateValidator()
    {
        RuleFor(x => x)
            .NotNull()
            .WithMessage("CircleCreateDTO cannot be null");

        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Name is required")
            .MaximumLength(100)
            .WithMessage("Name must be less than 100 characters");

        RuleFor(x => x.Description)
            .MaximumLength(500)
            .WithMessage("Description must be less than 500 characters");
        
        RuleFor(x => x.Administrators)
            .NotNull()
            .WithMessage("Administrators are required for a circle");
        
        RuleFor(x => x.Members)
            .NotNull()
            .WithMessage("Members are required for a circle");

        RuleFor(x => x.CreatedAt)
            .NotEmpty()
            .WithMessage("CreatedAt is required")
            .LessThanOrEqualTo(DateTime.Now)
            .WithMessage("CreatedAt cannot be in the future");

        RuleFor(x => x.CreatedById)
            .NotEmpty()
            .WithMessage("CreatedById is required")
            .Length(36)
            .WithMessage("CreatedById must be a valid GUID");
    }
}