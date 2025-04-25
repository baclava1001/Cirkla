using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cirkla_DAL.Models.Enums;
using Cirkla.Shared.DTOs.Contracts;

namespace Cirkla.Shared.Validators.Contracts
{
    public class ContractUpdateValidator : AbstractValidator<ContractUpdateDTO>
    {
        public ContractUpdateValidator()
        {

            RuleFor(contract => contract)
                .NotNull()
                .WithMessage("Attempted creating a new borrowing contract with null value");

            RuleFor(contract => contract.StartTime)
                .GreaterThanOrEqualTo(contract => contract.Created)
                .WithMessage("Start date cannot be earlier than request creation date");

            RuleFor(contract => contract.EndTime)
                .GreaterThan(contract => contract.StartTime)
                .WithMessage("End date must be later than start date");

            RuleFor(contract => contract.FromStatus)
                .IsInEnum()
                .WithMessage("Invalid contract status");

            RuleFor(contract => contract.ToStatus)
                .IsInEnum()
                .WithMessage("Invalid contract status");

            RuleFor(contract => contract.ToStatus)
                .Must(status => status != ContractStatus.Pending)
                .WithMessage("Updated contracts can't have 'Pending' status");
        }
    }
}
