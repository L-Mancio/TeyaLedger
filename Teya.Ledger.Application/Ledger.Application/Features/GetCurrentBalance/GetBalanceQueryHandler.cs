
using Ledger.API.Features.GetCurrentBalance.ApiContract;
using Ledger.Application.Services.Interfaces;

namespace Ledger.Application.Features.GetCurrentBalance;

public class GetBalanceQueryHandler(ILedgerService ledgerService)
{
    public async Task<BalanceResponse> HandleAsync()
    {
        var balance = await ledgerService.GetCurrentBalanceAsync();
        
        var balanceResponse = BalanceResponse.FromDomain(balance);

        return balanceResponse;
    }
}
