using Ledger.Application.Features.GetTransactions.ApiContracts;
using Ledger.Application.Services.Interfaces;

namespace Ledger.Application.Features.GetTransactions;

public class TransactionsQueryHandler(ILedgerService ledgerService)
{
    public async Task<IReadOnlyList<TransactionResponse>> HandleAsync(TransactionsRequest request)
    {
        var transactions = await ledgerService.GetTransactionsAsync(request);

        return [.. transactions.Select(TransactionResponse.FromDomain)];
    }
}
