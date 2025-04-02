using FakeItEasy;
using FluentAssertions;
using Ledger.Application.Services;
using Ledger.Application.Services.Interfaces;
using Ledger.Domain.BusinessRules;
using Ledger.Domain.Models.Enums;

namespace Ledger.Tests.Application;

public class LedgerServiceTest
{
    private readonly ILedgerService _ledgerService;
    public LedgerServiceTest() => _ledgerService = new LedgerService();

    [Fact]
    public async Task DepositAsync_WithValidAmount_ShouldCreateTransaction()
    {
        // Arrange
        double depositAmount = 100.0;

        // Act
        var transaction = await _ledgerService.DepositAsync(depositAmount);

        // Assert
        transaction.Should().NotBeNull();
        transaction.Amount.Should().Be(depositAmount);
        transaction.Type.Should().Be(TransactionType.Deposit);
        transaction.Description.Should().Be("Deposit");
    }

    [Fact]
    public async Task DepositAsync_WithNegativeAmount_ShouldThrowArgumentException()
    {
        // Arrange
        double depositAmount = -100.0;

        // Act
        var exception = await Assert.ThrowsAsync<BuisnessRuleException>(() => _ledgerService.DepositAsync(depositAmount));

        // Assert
        exception.Should().BeOfType<BuisnessRuleException>();
    }


    //more tests ... 
}
