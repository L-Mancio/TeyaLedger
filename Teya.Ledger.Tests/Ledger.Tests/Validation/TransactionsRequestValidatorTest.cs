using FluentAssertions;
using Ledger.Application.Features.GetTransactions.ApiContracts;
using Ledger.Application.Features.GetTransactions.Validation;

namespace Ledger.Tests.Validation;

public class TransactionsRequestValidatorTest
{
    private readonly TransactionsRequestValidator _validator = new();

    [Fact]
    public void Validate_WithValidRequest_ShouldReturnSuccess()
    {
        var request = new TransactionsRequest(DateTime.UtcNow, DateTime.UtcNow);
        var result = _validator.Validate(request);
        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void Validate_WithInvalidRequest_ShouldReturnFailure()
    {
        var request = new TransactionsRequest(DateTime.UtcNow, DateTime.UtcNow.AddDays(-1));
        var result = _validator.Validate(request);
        result.IsValid.Should().BeFalse();
    }

    [Fact]
    public void Validate_WithOneNullRequest_ShouldReturnFailure()
    {
        var request = new TransactionsRequest(DateTime.UtcNow, null);
        var result = _validator.Validate(request);
        result.IsValid.Should().BeFalse();
    }
    
}
