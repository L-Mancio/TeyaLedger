using Ledger.Application.Features.GetTransactions.ApiContracts;
using Ledger.Domain.Models;

namespace Ledger.Application.Services.Interfaces;

public interface ILedgerService
{
    Task<Balance> GetCurrentBalanceAsync();
    Task<Transaction> DepositAsync(double amount);
    Task<Transaction> WithdrawAsync(double amount);    
    Task<IReadOnlyList<Transaction>> GetTransactionsAsync(TransactionsRequest request);
}
