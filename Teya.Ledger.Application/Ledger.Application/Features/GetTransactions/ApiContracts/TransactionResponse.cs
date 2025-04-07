using Ledger.Domain.Models;
using Ledger.Domain.Models.Enums;

namespace Ledger.Application.Features.GetTransactions.ApiContracts;

public record TransactionResponse(double Amount, TransactionType Type, DateTime Timestamp, string Description)
{
    public static TransactionResponse FromDomain(Transaction transaction) =>
        new(transaction.Amount, transaction.Type, transaction.Timestamp, transaction.Description);

}
