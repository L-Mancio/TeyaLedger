using Ledger.Domain.Models.Enums;

namespace Ledger.Application.Features.SendTransaction.ApiContracts;

public record SendResponse(
    Guid Id, 
    double Amount, 
    TransactionType Type, 
    DateTime Timestamp, 
    string Description)
    {
        public static SendResponse FromDomain(Domain.Models.Transaction transaction) =>
            new(transaction.TransactionId, transaction.Amount, transaction.Type, transaction.Timestamp, transaction.Description);


    }
