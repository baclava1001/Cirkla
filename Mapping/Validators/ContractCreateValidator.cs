using FluentValidation;
using Mapping.DTOs.Contracts;

namespace Mapping.Validators;

public class ContractCreateValidator : AbstractValidator<ContractCreateDTO>
{
    public ContractCreateValidator()
    {
        RuleFor(contract => contract)
            .NotNull()
            .WithMessage("Request not valid");

        RuleFor(contract => contract.Created)
            .GreaterThanOrEqualTo(DateTime.Now)
            .WithMessage("Contract creation date cannot be in the past");

        RuleFor(contract => contract.StartTime)
            .GreaterThanOrEqualTo(contract => contract.Created)
            .WithMessage("Start date cannot be earlier than request creation date");

        RuleFor(contract => contract.EndTime)
            .GreaterThan(contract => contract.StartTime)
            .WithMessage("End date cannot be later than start date");
    }
}

public class ContractUpdateValidator : AbstractValidator<ContractUpdateDTO>
{
    public ContractUpdateValidator()
    {
        //Include(new ContractCreateValidator()); // Reuse rules from other validator
        
        RuleFor(contract => contract.Id)
            .GreaterThan(0)
            .WithMessage("Invalid contract id");

        RuleFor(contract => contract.UpdatedByUserId)
            .NotEmpty()
            .WithMessage("UpdatedByUserId cannot be empty");
    }
}
