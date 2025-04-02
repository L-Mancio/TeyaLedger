using FluentValidation;
using Ledger.Application.Features.GetTransactions.ApiContracts;

namespace Ledger.Application.Features.GetTransactions.Validation;

public class TransactionsRequestValidator : AbstractValidator<TransactionsRequest>
{
    public TransactionsRequestValidator()
    {
        RuleFor(x => x.FromDateTime)
            .NotNull()
            .When(x => x.ToDateTime.HasValue)
            .WithMessage("FromDateTime must be defined if ToDateTime is defined");

        RuleFor(x => x.ToDateTime)
            .NotNull()
            .When(x => x.FromDateTime.HasValue)
            .WithMessage("ToDateTime must be defined if FromDateTime is defined");

        RuleFor(x => x.FromDateTime)
            .LessThanOrEqualTo(x => x.ToDateTime)
            .When(x => x.FromDateTime.HasValue && x.ToDateTime.HasValue)
            .WithMessage("FromDateTime must be less than or equal to ToDateTime");

        RuleFor(x => x.ToDateTime)
            .GreaterThanOrEqualTo(x => x.FromDateTime)
            .When(x => x.FromDateTime.HasValue && x.ToDateTime.HasValue)
            .WithMessage("ToDateTime must be greater than or equal to FromDateTime");
    }
} 

