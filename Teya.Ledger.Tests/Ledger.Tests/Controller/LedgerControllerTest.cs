using System;
using FakeItEasy;
using FluentAssertions;
using Ledger.API.Controllers;
using Ledger.Application.Features.Deposit;
using Ledger.Application.Features.Deposit.ApiContract;
using Ledger.Application.Features.GetCurrentBalance;
using Ledger.Application.Features.GetTransactions;
using Ledger.Application.Features.Withdraw;
using Ledger.Application.Services.Interfaces;
using Ledger.Domain.Models;
using Ledger.Domain.Models.Enums;
using Microsoft.AspNetCore.Mvc;

namespace Ledger.Tests.Endpoint;

public class LedgerControllerTest
{
    private readonly ILedgerService _ledgerService;
    private readonly GetBalanceQueryHandler _getBalanceQueryHandler;
    private readonly DepositQueryHandler _depositQueryHandler;
    private readonly WithdrawQueryHandler _withdrawQueryHandler;
    private readonly TransactionsQueryHandler _transactionsQueryHandler;

    public LedgerControllerTest() 
    {
        _ledgerService = A.Fake<ILedgerService>();
        _getBalanceQueryHandler = new GetBalanceQueryHandler(_ledgerService);   
        _depositQueryHandler = new DepositQueryHandler(_ledgerService);
        _withdrawQueryHandler = new WithdrawQueryHandler(_ledgerService);
        _transactionsQueryHandler = new TransactionsQueryHandler(_ledgerService);
    }

    [Fact]
    public async Task DepositAsync_WithValidAmount_ShouldCreateTransaction()
    {
        // Arrange
        var controller = Controller();
        double depositAmount = 100.0;

        A.CallTo(() => _ledgerService.DepositAsync(depositAmount)).Returns(Task.FromResult(Transaction.CreateDeposit(depositAmount, "Deposit Test")));

        // Act
        var response = ((CreatedAtActionResult) await controller.DepositTransaction(new DepositRequest(depositAmount))).Value as DepositResponse;

        // Assert
        response!.Amount.Should().Be(depositAmount);
        response.Type.Should().Be(TransactionType.Deposit);
        response.Timestamp.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromMilliseconds(100));
        response.Description.Should().Be("Deposit Test");
    }

    private LedgerController Controller() => new(_getBalanceQueryHandler, _depositQueryHandler, _withdrawQueryHandler, _transactionsQueryHandler);

}
