using Ledger.Application.Features.Deposit.ApiContract;
using Ledger.Application.Services.Interfaces;

namespace Ledger.Application.Features.Deposit;

public class DepositQueryHandler(ILedgerService ledgerService)
{
    public async Task<DepositResponse> HandleAsync(DepositRequest request)
    {
        var transaction = await ledgerService.DepositAsync(request.Amount);

        return DepositResponse.FromDomain(transaction);
    }
}
