using System.Collections.Immutable;
using Ledger.Application.Features.GetTransactions.ApiContracts;
using Ledger.Application.Features.SendTransaction.ApiContracts;
using Ledger.Application.Services.Interfaces;
using Ledger.Domain.Models;

namespace Ledger.Application.Services;

public class LedgerService : ILedgerService
{
    private Balance _balance = Balance.Create(0, DateTime.UtcNow);

    private List<Account> _accounts = [new Account(Guid.Parse("340C25D4-EB45-4538-B11E-D0A3EB954934"), 1000), new Account(Guid.Parse("F549ACE2-975F-4F5B-975B-45FD04C83301"), 10)];

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

    public async Task<Transaction> SendTransactionAsync(SendRequest request)
    {
        var fromAccount = _accounts.FirstOrDefault(x => x.AccountId == request.FromAccountId)!;
        var toAccount = _accounts.FirstOrDefault(x => x.AccountId == request.ToAccountId)!;

        return await Task.FromResult(Account.Send(fromAccount, toAccount, request.Amount, request.Description));
    }    

}
