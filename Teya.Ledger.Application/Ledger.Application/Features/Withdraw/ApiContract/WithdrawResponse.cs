using Ledger.Domain.Models;
using Ledger.Domain.Models.Enums;

namespace Ledger.Application.Features.Withdraw.ApiContract;

public record WithdrawResponse(
    Guid Id, 
    double Amount, 
    TransactionType Type, 
    DateTime Timestamp, 
    string Description)
{
    public static WithdrawResponse FromDomain(Transaction transaction) =>
        new(transaction.TransactionId, transaction.Amount, transaction.Type, transaction.Timestamp, transaction.Description);
}
