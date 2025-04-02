using Ledger.Application.Features.Withdraw.ApiContract;
using Ledger.Application.Services.Interfaces;

namespace Ledger.Application.Features.Withdraw;

public class WithdrawQueryHandler(ILedgerService ledgerService)
{
    public async Task<WithdrawResponse> HandleAsync(WithdrawRequest request)
    {
        var transaction = await ledgerService.WithdrawAsync(request.Amount);

        return WithdrawResponse.FromDomain(transaction);
    }
}
