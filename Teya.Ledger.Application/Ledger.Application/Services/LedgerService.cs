using System.Collections.Immutable;
using Ledger.Application.Features.GetTransactions.ApiContracts;
using Ledger.Application.Services.Interfaces;
using Ledger.Domain.Models;

namespace Ledger.Application.Services;

public class LedgerService : ILedgerService
{
    private Balance _balance = Balance.Create(0, DateTime.UtcNow);

    private List<Transaction> _transactions = [];

    public async Task<Balance> GetCurrentBalanceAsync() => await Task.FromResult(_balance);

    public async Task<Transaction> DepositAsync(double amount)
    {
        var transaction = Transaction.CreateDeposit(amount, "Deposit");
        
        //assume transaction is always successful, if it conforms to the business rules
        
        _balance.Deposit(amount);
        _transactions.Add(transaction);

        return await Task.FromResult(transaction);
    }

    public async Task<Transaction> WithdrawAsync(double amount)
    {
        var transaction = Transaction.CreateWithdrawal(amount, _balance.Amount, "Withdrawal");

        //assume transaction is always successful, if it conforms to the business rules

        _balance.Withdraw(amount);
        _transactions.Add(transaction);

        return await Task.FromResult(transaction);
    }

    public async Task<IReadOnlyList<Transaction>> GetTransactionsAsync(TransactionsRequest request)
    {
        return await Task.FromResult(
            (request.FromDateTime is not null && request.ToDateTime is not null) 
            ? _transactions
                .Where(t => t.Timestamp >= request.FromDateTime && 
                            t.Timestamp <= request.ToDateTime)
                .OrderByDescending(t => t.Timestamp).ToImmutableList()
            : _transactions.OrderByDescending(t => t.Timestamp).ToImmutableList()
        );
    }

}
