﻿using Cirkla_DAL.Models.Enums;
using FluentValidation;
using Mapping.DTOs.Contracts;

namespace Mapping.Validators.Contracts;

public class ContractCreateValidator : AbstractValidator<ContractCreateDTO>
{
    public ContractCreateValidator()
    {
        RuleFor(contract => contract)
            .NotNull()
            .WithMessage("Attempted creating a new borrowing contract with null value");

        RuleFor(contract => contract.BorrowerId)
            .Must((contract, borrowerId) => borrowerId != contract.OwnerId)
            .WithMessage("Cannot borrow your own item");

        RuleFor(contract => contract.Created)
            .Must(created => created >= DateTime.UtcNow.AddHours(-2))
            .WithMessage("Contract creation date cannot be in the past");

        RuleFor(contract => contract.StartTime)
            .GreaterThanOrEqualTo(contract => contract.Created)
            .WithMessage("Start date cannot be earlier than request creation date");

        RuleFor(contract => contract.EndTime)
            .GreaterThan(contract => contract.StartTime)
            .WithMessage("End date must be later than start date");

        RuleFor(contract => contract.CurrentStatus)
            .IsInEnum()
            .WithMessage("Invalid contract status");

        RuleFor(contract => contract.CurrentStatus)
            .Must(status => status == ContractStatus.Pending)
            .WithMessage("New contracts must have 'Pending' status");
    }
}