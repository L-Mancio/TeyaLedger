using Ledger.Domain.Models;

namespace Ledger.API.Features.GetCurrentBalance.ApiContract;

public record BalanceResponse(double Amount, DateTime AtTimestamp)
{
    public static BalanceResponse FromDomain(Balance balance) => 
        new(balance.Amount, balance.AtTimestamp);
}
