using Ledger.Domain.Models;
using Ledger.Domain.Models.Enums;

namespace Ledger.Application.Features.Deposit.ApiContract;

public record DepositResponse(
    Guid Id, 
    double Amount, 
    TransactionType Type, 
    DateTime Timestamp, 
    string Description)
{
    public static DepositResponse FromDomain(Transaction transaction) =>
        new(transaction.TransactionId, transaction.Amount, transaction.Type, transaction.Timestamp, transaction.Description);
}
